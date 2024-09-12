using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;
using MySqlServices = WebApp.Services.MySQL;
using NoSqlServices = WebApp.Services.NoSQL;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly DatabaseService _databaseService;
    private readonly MySqlServices.UserService _mySqlUserService;
    private readonly NoSqlServices.UserService _noSqlUserService;
    private readonly ILogger<HomeController> _logger;
   

    public HomeController(DatabaseService databaseService,
        MySqlServices.UserService mySqlUserService,
        NoSqlServices.UserService noSqlUserService,
        ILogger<HomeController> logger)
    {
        _databaseService = databaseService;
        _mySqlUserService = mySqlUserService;
        _noSqlUserService = noSqlUserService;
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.Message = TempData["Message"];
        ViewBag.MessageType = TempData["MessageType"];

        HomeViewModel homeViewModel = new HomeViewModel()
        {
            Users = new List<UserViewModel>(),
            SelectedUserId = GlobalValues.SelectedUser!=null? GlobalValues.SelectedUser.Id: 0
        };

        if (GlobalValues.CurrentDBMS == DBMS.MySQL)
        {
            homeViewModel.Users.AddRange(_mySqlUserService.GetAll());
        }
        else if (GlobalValues.CurrentDBMS == DBMS.MongoDB)
        {
            homeViewModel.Users.AddRange(_noSqlUserService.GetAll());
        }
        return View(homeViewModel);
    }

    [HttpPost]
    public IActionResult InitializeMySqlDb()
    {
        try
        {
            _databaseService.InitializeMySqlDb();
            GlobalValues.CurrentDBMS = DBMS.MySQL;
            TempData["Message"] = "MySQL database is successfully initialized!";
            TempData["MessageType"] = "success";
        }
        catch (Exception ex)
        {
            TempData["Message"] = $"Error while initializing MySQL database: {ex.Message}";
            TempData["MessageType"] = "error";
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult MigrateDataToMongoDb()
    {
        try
        {
            _databaseService.MigrateDataToMongoDb();
            GlobalValues.CurrentDBMS = DBMS.MongoDB;

            TempData["Message"] = "Data is successfully migrated from MySQL to MongoDB!";
            TempData["MessageType"] = "success";
        }
        catch (Exception ex)
        {
            TempData["Message"] = $"Error while migrating Data from MySQL to MongoDB: {ex.Message}";
            TempData["MessageType"] = "error";
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult SaveSelectedUser(HomeViewModel model)
    {
        try
        {
            var selectedUserId = model.SelectedUserId;
            if (GlobalValues.CurrentDBMS == DBMS.MySQL)
            {
                GlobalValues.SelectedUser = _mySqlUserService.GetById(selectedUserId);
             
            }
            else if (GlobalValues.CurrentDBMS == DBMS.MongoDB)
            {
                GlobalValues.SelectedUser = _noSqlUserService.GetById(selectedUserId);
            }

            TempData["Message"] = $"User is successfully selected!";
            TempData["MessageType"] = "success";
        }
        catch (Exception ex)
        {
            TempData["Message"] = $"Error saving selected user: {ex.Message}";
            TempData["MessageType"] = "error";
        }

        return RedirectToAction("Index");
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


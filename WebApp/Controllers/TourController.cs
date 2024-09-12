using System;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebApp.Models;

using NoSqlServices = WebApp.Services.NoSQL;
using MySqlServices = WebApp.Services.MySQL;

namespace WebApp.Controllers
{
	public class TourController: Controller
	{
        private readonly MySqlServices.TourService _mySqlTourService;
        private readonly NoSqlServices.TourService _noSqlTourService;
        private readonly ILogger<TourController> _logger;


        public TourController(MySqlServices.TourService mySqlTourService,
            NoSqlServices.TourService noSqlTourService,
            ILogger<TourController> logger)
        {
            _mySqlTourService = mySqlTourService;
            _noSqlTourService = noSqlTourService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<TourViewModel> tourViewModelList = new List<TourViewModel>();
            if (GlobalValues.CurrentDBMS == DBMS.MySQL)
            {
                tourViewModelList.AddRange(_mySqlTourService.GetAll());
            }
            else if (GlobalValues.CurrentDBMS == DBMS.MongoDB)
            {
                tourViewModelList.AddRange(_noSqlTourService.GetAll());
            }
            return View(tourViewModelList);

        }
        public IActionResult Details(int id)
        {
            TourViewModel tourViewModel = null;

            if (GlobalValues.CurrentDBMS == DBMS.MySQL)
            {
                tourViewModel = _mySqlTourService.GetById(id);

            }
            else if (GlobalValues.CurrentDBMS == DBMS.MongoDB)
            {
                tourViewModel = _noSqlTourService.GetById(id);
            }
            return View(tourViewModel);
        }
    }
}


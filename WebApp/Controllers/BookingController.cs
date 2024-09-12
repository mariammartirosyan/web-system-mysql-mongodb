using System;
using ApplicationCore.Entities.NoSQL;
using Microsoft.AspNetCore.Mvc;
using MySqlServices = WebApp.Services.MySQL;
using MySqlEntities = ApplicationCore.Entities.MySQL;

using NoSqlServices = WebApp.Services.NoSQL;
using NoSqlEntities = ApplicationCore.Entities.NoSQL;
using WebApp.Models;
using ApplicationCore.Entities.MySQL;

namespace WebApp.Controllers
{
	public class BookingController : Controller
    {
        private readonly MySqlServices.BookingService _mySqlBookingService;
        private readonly MySqlServices.TourService _mySqlTourService;
        private readonly NoSqlServices.BookingService _noSqlTourBookingService;
        private readonly NoSqlServices.TourService _noSqlTourService;

        public BookingController(MySqlServices.BookingService mySqlBookingService,
            MySqlServices.TourService mySqlTourService,
            NoSqlServices.BookingService noSqlBookingService,
            NoSqlServices.TourService noSqlTourService)
        {
            _mySqlBookingService = mySqlBookingService;
            _mySqlTourService = mySqlTourService;
            _noSqlTourBookingService = noSqlBookingService;
            _noSqlTourService = noSqlTourService;
        }

		[HttpPost]
		public IActionResult ProcessBooking(int tourId)
		{
            try
            {
                if (GlobalValues.SelectedUser != null)
                {
                   
                    if (GlobalValues.CurrentDBMS == DBMS.MySQL)
                    {
                        var tour = _mySqlTourService.GetById(tourId);
                        if (tour != null)
                        {
                            var userBookings = _mySqlBookingService.GetAllByUserId(GlobalValues.SelectedUser.Id);
                            if (!userBookings.Any(x=>x.TourId==tourId && x.BookingDate.Date == DateTime.UtcNow.Date))
                            {
                                _mySqlBookingService.Insert(new BookingViewModel()
                                {
                                    BookingDate = DateTime.UtcNow,
                                    Price = tour.Price,
                                    TourId = tourId,
                                    UserId = GlobalValues.SelectedUser.Id
                                });
                            }
                            else
                            {
                                return StatusCode(400, "A booking of this tour is already made today.");
                            }

                        }
                        else
                        {
                            return StatusCode(400, "Tour was not found");
                        }
                    }
                    else if (GlobalValues.CurrentDBMS == DBMS.MongoDB)
                    {
                        var tour = _noSqlTourService.GetById(tourId);
                        if (tour != null)
                        {
                            var userBookings = _noSqlTourBookingService.GetAllByUserId(GlobalValues.SelectedUser.Id);

                            if (!userBookings.Any(x => x.TourId == tourId && x.BookingDate.Date == DateTime.UtcNow.Date))
                            {
                                _noSqlTourBookingService.Insert(new UserBooking()
                                {
                                    BookingDate = DateTime.UtcNow,
                                    Price = tour.Price,
                                    TourId = tourId
                                }, GlobalValues.SelectedUser.Id);
                            }
                            else
                            {
                                return StatusCode(400, "A booking of this tour is already made today.");
                            }
                        }
                        else
                        {
                            return StatusCode(400, "Tour was not found");
                        }
                    }
                    

                }
                return Ok();
            }
			catch(Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.ToString());
            }
        }


        [HttpGet]
        public IActionResult Index()
        {
            var bookings = new List<BookingViewModel>();

            if (GlobalValues.SelectedUser != null)
            {
                if (GlobalValues.CurrentDBMS == DBMS.MySQL)
                {
                    bookings.AddRange(_mySqlBookingService.GetAll().Where(x => x.UserId == GlobalValues.SelectedUser.Id));
                }
                else if (GlobalValues.CurrentDBMS == DBMS.MongoDB)
                {
                    bookings.AddRange(_noSqlTourBookingService.GetAllByUserId(GlobalValues.SelectedUser.Id));
                }
            }
            
            return View(bookings);
        }
	}
}


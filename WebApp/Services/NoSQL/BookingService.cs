using System;
using ApplicationCore.Entities.NoSQL;
using Infrastructure.Repositories.NoSQL;
using WebApp.Models;

namespace WebApp.Services.NoSQL
{
	public class BookingService
	{
        private readonly BookingRepository _repository;

        public BookingService(BookingRepository repository)
		{
            _repository = repository;
        }
        public void Insert(UserBooking booking, int userId)
        {
            _repository.Insert(booking, userId);
        }
        public List<BookingViewModel> GetAllByUserId(int userId)
        {
            var bookings = _repository.GetUserBookings(userId);
            var bookingsViewModelList = new List<BookingViewModel>();

            foreach(var booking in bookings)
            {
                bookingsViewModelList.Add(new BookingViewModel()
                {
                    UserId = userId,
                    TourId = booking.TourId,
                    Price = booking.Price,
                    BookingDate = booking.BookingDate
                });
            }
            return bookingsViewModelList;
        }
    }
}


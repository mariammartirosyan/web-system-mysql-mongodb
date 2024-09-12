using System;
using Infrastructure.Repositories.MySQL;
using WebApp.Models;

namespace WebApp.Services.MySQL
{
	public class BookingService
	{
        private readonly BookingRepository _bookingRepository;
        private readonly TourRepository _tourRepository;
        private readonly UserRepository _userRepository;

        public BookingService(BookingRepository bookingRepository, TourRepository tourRepository, UserRepository userRepository)
		{
            _bookingRepository = bookingRepository;
            _tourRepository = tourRepository;
            _userRepository = userRepository;
        }

        public void Insert(BookingViewModel bookingViewModel)
        {
            if (bookingViewModel != null)
            {
                _bookingRepository.Insert(new ApplicationCore.Entities.MySQL.Booking()
                {
                    UserId = bookingViewModel.UserId,
                    TourId = bookingViewModel.TourId,
                    BookingDate = bookingViewModel.BookingDate,
                    Price = bookingViewModel.Price,
                    Tour = _tourRepository.GetById(bookingViewModel.TourId),
                    User = _userRepository.GetById(bookingViewModel.UserId)
                });
            }
        }

        public List<BookingViewModel> GetAll()
        {
            var bookings = _bookingRepository.GetAll();
            var bookingsViewModelList = new List<BookingViewModel>();

            foreach(var booking in bookings)
            {
                bookingsViewModelList.Add(new BookingViewModel()
                {
                    UserId = booking.UserId,
                    TourId = booking.TourId,
                    BookingDate = booking.BookingDate,
                    Price = booking.Price
                });
            }

            return bookingsViewModelList;
        }

        public List<BookingViewModel> GetAllByUserId(int userId)
        {
            var bookings = _bookingRepository.GetAllByUserId(userId);
            var bookingsViewModelList = new List<BookingViewModel>();

            foreach (var booking in bookings)
            {
                bookingsViewModelList.Add(new BookingViewModel()
                {
                    UserId = booking.UserId,
                    TourId = booking.TourId,
                    BookingDate = booking.BookingDate,
                    Price = booking.Price
                });
            }

            return bookingsViewModelList;
        }
    }
}


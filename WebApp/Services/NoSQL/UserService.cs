using System;
using Infrastructure.Repositories.NoSQL;
using WebApp.Models;

namespace WebApp.Services.NoSQL
{
	public class UserService
	{
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public UserViewModel GetById(int id)
        {
            var user = _userRepository.GetById(id);
            UserViewModel model = null;
            if (user != null)
            {
                model = new UserViewModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password
                };
                foreach (var booking in user.Bookings)
                {
                    model.Booking.Add(new BookingViewModel()
                    {
                        TourId = booking.TourId,
                        UserId = user.Id,
                        BookingDate = booking.BookingDate,
                        Price = booking.Price
                    });
                }
            }
            return model;
        }

        public List<UserViewModel> GetAll()
        {
            var users = _userRepository.GetAll();
            var userViewModelList = new List<UserViewModel>();
            foreach (var user in users)
            {
                userViewModelList.Add(new UserViewModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password
                });
            }
            return userViewModelList;
        }
    }
}


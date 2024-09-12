using System;
namespace WebApp.Models
{
	public class BookingViewModel
    {
        public int TourId { get; set; }
        public int UserId { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal Price { get; set; }
    }
}


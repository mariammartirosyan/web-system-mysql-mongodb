using System;
namespace WebApp.Models
{
	public class AccommodationViewModel
	{
        public int Id { get; set; }
        public int TourId { get; set; }
        public string Location { get; set; }
        public DateTime CheckInDateTime { get; set; }
        public DateTime CheckOutDateTime { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
    }
}


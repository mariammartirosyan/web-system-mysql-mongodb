using System;
namespace ApplicationCore.Entities.MySQL
{
	public class Booking
	{
        public int TourId { get; set; }
        public int UserId { get; set; }
		public DateTime BookingDate { get; set; }
        public decimal Price { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual User User { get; set; }
    }
}


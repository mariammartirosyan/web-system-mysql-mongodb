using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities.MySQL
{
    public class Accommodation
    {
		public int Id { get; set; }
        public int TourId { get; set; }
        public string Location { get; set; }
        public DateTime CheckInDateTime { get; set; }
        public DateTime CheckOutDateTime { get; set; }
		public string Type { get; set; }
		public decimal Price { get; set; }
		public virtual Tour Tour { get; set; }
	}
}


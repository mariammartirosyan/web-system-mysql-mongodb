using System;
namespace ApplicationCore.Entities.MySQL
{
	public class Transportation
	{
		public int Id { get; set; }
        public int TourId { get; set; }
        public string DepartureLocation { get; set; }
		public DateTime DepartureDateTime { get; set; }
        public string ArrivalLocation { get; set; }
        public DateTime ArrivalDateTime { get; set; }
		public string Type { get; set; }
		public decimal Price { get; set; }
        public virtual Tour Tour { get; set; }

    }
}


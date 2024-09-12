using System;
namespace ApplicationCore.Entities
{
	public class ReportRow
	{
		public int TourId { get; set; }
        public int BookingsNumber { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string AttractionTypeName { get; set; }
        public string AttractionName { get; set; }
        public string AttractionLocation { get; set; }
    }
}


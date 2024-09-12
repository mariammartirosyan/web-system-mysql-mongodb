using System;
namespace ApplicationCore.Entities.MySQL
{
	public class Tour
	{
		public int Id { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set; }
        //public List<TourExtension> IncludedTours { get;} = new List<TourExtension>();
        public virtual List<Tour> IncludedTour { get; } = new List<Tour>();
        public virtual List<Tour> SupersetTour { get; } = new List<Tour>();
        public virtual List<Booking> Booking { get; } = new List<Booking>();
        public virtual List<Transportation> Transportation { get; } = new List<Transportation>();
        public virtual List<Accommodation> Accommodation { get; } = new List<Accommodation>();
        public virtual List<TouristAttraction> TouristAttraction { get; } = new List<TouristAttraction>();

        ////public List<TourTouristAttraction> TourTouristAttractions { get; set; }
    }
}


using System;
namespace WebApp.Models
{
	public class TourViewModel
	{
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual List<TourViewModel> IncludedTour { get; } = new List<TourViewModel>();
        public virtual List<BookingViewModel> Booking { get; } = new List<BookingViewModel>();
        public virtual List<TransportationViewModel> Transportation { get; } = new List<TransportationViewModel>();
        public virtual List<AccommodationViewModel> Accommodation { get; } = new List<AccommodationViewModel>();
        public virtual List<TouristAttractionViewModel> TouristAttraction { get; } = new List<TouristAttractionViewModel>();
    }
}


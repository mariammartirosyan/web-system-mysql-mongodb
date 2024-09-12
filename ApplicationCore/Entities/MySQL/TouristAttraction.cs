using System;
namespace ApplicationCore.Entities.MySQL
{
	public class TouristAttraction
	{
		public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public virtual List<AttractionType> AttractionType { get; } = new List<AttractionType>();
        public virtual List<Tour> Tour { get; } = new List<Tour>();
        //public List<TouristAttractionAttractionType> TouristAttractionAttractionTypes { get; set; }
        // public List<TourTouristAttraction> TourTouristAttractions { get; set; }
    }
}


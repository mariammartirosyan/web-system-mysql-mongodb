using System;
namespace ApplicationCore.Entities.MySQL
{
	public class AttractionType
	{
		public int Id { get; set; }
		public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<TouristAttraction> TouristAttraction { get; } = new List<TouristAttraction>();
    }
}


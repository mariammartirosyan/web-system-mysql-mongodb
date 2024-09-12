using System;
namespace WebApp.Models
{
	public class TouristAttractionViewModel
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public virtual List<AttractionTypeViewModel> AttractionType { get; } = new List<AttractionTypeViewModel>();
    }
}


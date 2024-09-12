using System;
using ApplicationCore.Entities.MySQL;
using CsvHelper.Configuration;

namespace Infrastructure.Helpers.CsvMappings
{
	public class AttractionTypeTouristAttractionMap: ClassMap<AttractionTypeTouristAttraction>
    {
		public AttractionTypeTouristAttractionMap()
		{
            Map(p => p.TouristAttractionId).Index(0);
            Map(p => p.AttractionTypeId).Index(1);
        }
	}
}


using System;
using ApplicationCore.Entities.MySQL;
using CsvHelper.Configuration;

namespace Infrastructure.Helpers.CsvMappings
{
	public class TouristAttractionMap: ClassMap<TouristAttraction>
    {
		public TouristAttractionMap()
		{
            Map(p => p.Id).Index(0);
            Map(p => p.Name).Index(1);
            Map(p => p.Description).Index(2);
            Map(p => p.Location).Index(3);
        }
	}
}


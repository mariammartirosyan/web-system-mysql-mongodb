using System;
using System.Diagnostics;
using ApplicationCore.Entities.MySQL;
using CsvHelper.Configuration;

namespace Infrastructure.Helpers.CsvMappings
{
	public class AccommodationMap : ClassMap<Accommodation>
    {
		public AccommodationMap()
		{
            Map(p => p.Location).Index(0);
            Map(p => p.Type).Index(1);
            Map(p => p.Price).Index(2);
        }
	}
}


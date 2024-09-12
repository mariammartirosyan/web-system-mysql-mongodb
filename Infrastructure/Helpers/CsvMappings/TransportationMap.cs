using System;
using System.Diagnostics;
using ApplicationCore.Entities.MySQL;
using CsvHelper.Configuration;

namespace Infrastructure.Helpers.CsvMappings
{
	public class TransportationMap : ClassMap<Transportation>
    {
		public TransportationMap()
		{
            Map(p => p.DepartureLocation).Index(0);
            Map(p => p.ArrivalLocation).Index(1);
            Map(p => p.Type).Index(2);
            Map(p => p.Price).Index(3);
        }
	}
}


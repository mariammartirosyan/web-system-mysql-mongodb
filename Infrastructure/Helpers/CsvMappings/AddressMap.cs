using System;
using ApplicationCore.Entities.MySQL;
using CsvHelper.Configuration;

namespace Infrastructure.Helpers.CsvMappings
{
	public class AddressMap: ClassMap<Address>
	{
		public AddressMap()
		{
            Map(p => p.Id).Index(0);
            Map(p => p.Country).Index(1);
            Map(p => p.City).Index(2);
            Map(p => p.Street).Index(3);
            Map(p => p.ZipCode).Index(4);
        }
    }
}


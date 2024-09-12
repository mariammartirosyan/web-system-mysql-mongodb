using System;
using ApplicationCore.Entities.MySQL;
using CsvHelper.Configuration;

namespace Infrastructure.Helpers.CsvMappings
{
	public class AttractionTypeMap : ClassMap<AttractionType>
    {
		public AttractionTypeMap()
		{
            Map(p => p.Id).Index(0);
            Map(p => p.Name).Index(1);
            Map(p => p.Description).Index(2);
        }
	}
}


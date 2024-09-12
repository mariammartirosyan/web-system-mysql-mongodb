using System;
using ApplicationCore.Entities.MySQL;
using CsvHelper.Configuration;

namespace Infrastructure.Helpers.CsvMappings
{
	public class RoleMap: ClassMap<Role>
    {
		public RoleMap()
		{
            Map(p => p.Id).Index(0);
            Map(p => p.Name).Index(1);
        }
	}
}


using System;
using ApplicationCore.Entities.MySQL;
using CsvHelper.Configuration;

namespace Infrastructure.Helpers.CsvMappings
{
	public class RoleUserMap: ClassMap<RoleUser>
	{
		public RoleUserMap()
		{
            Map(p => p.RoleId).Index(0);
            Map(p => p.UserId).Index(1);
        }
	}
}


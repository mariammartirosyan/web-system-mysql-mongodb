using System;
using ApplicationCore.Entities.MySQL;
using CsvHelper.Configuration;

namespace Infrastructure.Helpers.CsvMappings
{
	public class UserMap: ClassMap<User>
    {
		public UserMap()
		{
            Map(p => p.Id).Index(0);
            Map(p => p.FirstName).Index(1);
            Map(p => p.LastName).Index(2);
            Map(p => p.Email).Index(3);
            Map(p => p.Password).Index(4);
            Map(p => p.AddressId).Index(5);
        }
	}
}


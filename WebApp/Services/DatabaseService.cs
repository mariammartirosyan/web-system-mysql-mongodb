using System;
using Infrastructure;
using MongoDB.Driver;

namespace WebApp.Services
{
	public class DatabaseService
	{
        private readonly ApplicationDbContext _context;
        private readonly MySqlToMongoDbMigration _mySqlToMongo;

        public DatabaseService(ApplicationDbContext context, MySqlToMongoDbMigration mySqlToMongo)
		{
			_context = context;
            _mySqlToMongo = mySqlToMongo;
		}

		public void InitializeMySqlDb()
        {
            _context.Database.EnsureCreated();
            _context.DeleteData();
            _context.InsertData();
        }
       
		public void MigrateDataToMongoDb()
		{
			_mySqlToMongo.DeleteData();
            _mySqlToMongo.MigrateData();
		}
    }
}


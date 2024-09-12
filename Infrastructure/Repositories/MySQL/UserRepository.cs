using System;
using ApplicationCore.Entities.MySQL;

namespace Infrastructure.Repositories.MySQL
{
	public class UserRepository
	{
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public User GetById(int id)
        {
            return _context.Set<User>().Find(id);
        }
        public List<User> GetAll()
        {
            return _context.Set<User>().ToList();
        }
    }
}


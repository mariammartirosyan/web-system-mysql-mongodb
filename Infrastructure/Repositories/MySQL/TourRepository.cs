using System;
using ApplicationCore.Entities.MySQL;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.MySQL
{
	public class TourRepository
	{
        private readonly ApplicationDbContext _context;

        public TourRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Insert(Tour tour)
        {
            _context.Set<Tour>().Add(tour);
            _context.SaveChanges();
        }
        public Tour GetById(int id)
        {
            return _context.Set<Tour>().Find(id);
        }
        public void Update(Tour tour)
        {
            _context.Set<Tour>().Update(tour);
            _context.SaveChanges();
        }
        public void DeleteById(int id)
        {
            var tour = _context.Set<Tour>().Find(id);
            if (tour != null)
            {
                _context.Set<Tour>().Remove(tour);
                _context.SaveChanges();
            }
        }
        public List<Tour> GetAll()
        {
            return _context.Set<Tour>().ToList();
        }
    }
}


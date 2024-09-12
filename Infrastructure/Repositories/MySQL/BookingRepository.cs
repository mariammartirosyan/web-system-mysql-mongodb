using System;
using ApplicationCore.Entities.MySQL;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.MySQL
{
    public class BookingRepository 
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Insert(Booking booking)
        {
            _context.Set<Booking>().Add(booking);
            _context.SaveChanges();
        }
        public Booking GetById(int id)
        {
            return _context.Set<Booking>().Find(id);
        }
        public void Update(Booking booking)
        {
            _context.Set<Booking>().Update(booking);
            _context.SaveChanges();
        }
        public void DeleteById(int id)
        {
            var booking = _context.Set<Booking>().Find(id);
            if (booking != null)
            {
                _context.Set<Booking>().Remove(booking);
                _context.SaveChanges();
            }
        }
        public List<Booking> GetAll()
        {
            return _context.Set<Booking>().ToList();
        }
        public List<Booking> GetAllByUserId(int userId)
        {
            return _context.Set<Booking>().Where(x=>x.UserId==userId).ToList();
        }
    }
}


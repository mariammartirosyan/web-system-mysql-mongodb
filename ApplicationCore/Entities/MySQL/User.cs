using System;
namespace ApplicationCore.Entities.MySQL
{
	public class User
	{
		public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<Role> Role { get; } = new List<Role>();
        public virtual List<Booking> Booking { get; } = new List<Booking>(); //this was newly added
    }
}


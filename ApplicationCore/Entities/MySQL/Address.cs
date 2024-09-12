using System;
namespace ApplicationCore.Entities.MySQL
{
	public class Address
	{
		public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int ZipCode { get; set; }
        public virtual ICollection<User> User { get; } = new List<User>();
    }
}


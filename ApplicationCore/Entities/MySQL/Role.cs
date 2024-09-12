using System;
namespace ApplicationCore.Entities.MySQL
{
	public class Role
	{
		public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<User> User { get; } = new List<User>();
    }
}


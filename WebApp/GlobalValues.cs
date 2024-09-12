using System;
using System.Drawing;
using WebApp.Models;

namespace WebApp
{
	public static class GlobalValues
	{
		public static DBMS? CurrentDBMS { get; set; } = null;
        public static UserViewModel SelectedUser { get; set; }
    }
	public enum DBMS
	{
		MySQL,
		MongoDB
	}
}


using System;
namespace WebApp.Models
{
	public class HomeViewModel
	{
        public List<UserViewModel> Users { get; set; }
        public int SelectedUserId { get; set; }
    }
}


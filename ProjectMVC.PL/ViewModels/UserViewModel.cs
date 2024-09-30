﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace ProjectMVC.PL.ViewModels
{
	public class UserViewModel
	{
        public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public IEnumerable<string> Roles { get; set; }

		public UserViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

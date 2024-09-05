﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.DAL.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Code Is Required!")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Code Is Required!")]
        public string Name { get; set; } 

        public DateTime DateOfCreation { get; set; }
    }
}

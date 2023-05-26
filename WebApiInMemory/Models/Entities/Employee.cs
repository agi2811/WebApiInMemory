﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiInMemory.Models.Entities
{
    public class Employee
    {
        [Key]
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public DateTime? BIrthDate { get; set; }
    }
}

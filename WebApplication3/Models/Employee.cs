using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    [Table("Employee")]
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public virtual SalesInfo SalesInfo { get; set; }
    }
}

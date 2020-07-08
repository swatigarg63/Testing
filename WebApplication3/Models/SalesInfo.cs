using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.Models
{
    [Table("SalesInfo")]
    public class SalesInfo
    {
        [Key]
        public int Id { get; set; } //as InvoiceNumber
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public DateTime SalesDate { get; set; }
        public int Discount { get; set; }
        public int VATApplied { get; set; }
        public int InvoiceTotal { get; set; }

        [ForeignKey("employeeId")]
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

    }
}

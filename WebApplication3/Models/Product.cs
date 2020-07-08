using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Model
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public int Available_Quantity { get; set; }
        public string CategoryName { get; set; }
        
        //We can create Category table separately and then uses its foreign key in it
    }
}

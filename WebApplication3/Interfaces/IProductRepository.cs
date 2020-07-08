using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.Interfaces
{
    public interface IProductRepository
    {
        Product Add(Product product);

        IEnumerable<Product> GetProducts();

        Product Update(Product productChanges);

        bool Delete(int ProductId);
    }
}

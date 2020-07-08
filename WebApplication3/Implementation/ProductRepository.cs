using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Interfaces;
using WebApplication3.Model;

namespace WebApplication3.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext context;
        public ProductRepository(ApplicationDBContext context)
        {
            this.context = context;
        }

        public Product Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return product;
        }

        public IEnumerable<Product> GetProducts()
        {
            return context.Products;
        }

        public Product Update(Product productChanges)
        {
            var product = context.Products.Attach(productChanges);
            product.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return productChanges;
        }

        public bool Delete(int ProductId)
        {
            Product product = context.Products.Find(ProductId);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

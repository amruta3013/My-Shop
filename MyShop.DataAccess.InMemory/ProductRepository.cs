using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache Cache = MemoryCache.Default;
        List<Product> Products;

        public ProductRepository()
        {
            Products = Cache["Products"] as List<Product>;
            if (Products == null)
            {

                Products = new List<Product>();
            }
        }

        public void commit()
        {
            Cache["Products"] = Products;
        }

        public void Insert(Product p)
        {
            Products.Add(p);
        }

        public void Update(Product Product)
        {
            Product productToUpdate = Products.Find(p => p.ID == Product.ID);

            if (productToUpdate != null)
            {
                productToUpdate = Product;
            }
            else
            {
                throw new Exception("Product No Found");
            }

        }

        public Product Find(string Id)
        {
            Product product = Products.Find(p => p.ID == Id);

            if (product != null)
            {
               return product;
            }
            else
            {
                throw new Exception("Product No Found");
            }

        }

        public IQueryable<Product> Collection()
        {
            return Products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product productToDelete = Products.Find(p => p.ID ==Id);

            if (productToDelete != null)
            {
                Products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product No Found");
            }
        }
    }
}

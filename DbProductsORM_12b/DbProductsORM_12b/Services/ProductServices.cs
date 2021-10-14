using DbProductsORM_12b.Data;
using DbProductsORM_12b.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbProductsORM_12b.Services
{
    public class ProductServices
    {
        private ProductContext productContext;

        public List<Product> GetAll()
        {
            using (productContext = new ProductContext())
            {
                return productContext.Products.ToList();
            }
        }
        public Product Get(int id)
        {
            using (productContext = new ProductContext())
            {
                var product = productContext.Products.Find(id);
                return product;
                
            }
        }
        public void Add(Product product)
        {
            using (productContext = new ProductContext())
            {
                productContext.Products.Add(product);
                productContext.SaveChanges();
            }
        }
        public void Update(Product product)
        {
            using (productContext = new ProductContext())
            {
                var oldProduct = productContext.Products.Find(product.Id);

                if (oldProduct != null)
                {
                    oldProduct.Name = product.Name;
                    oldProduct.Price = product.Price;
                    oldProduct.Stock = product.Stock;
                    //productContext.Entry(oldProduct).CurrentValues.SetValues(product);

                    productContext.SaveChanges();
                }
            }
        }
        public void Delete(int id)
        {
            using(productContext = new ProductContext())
            {
                var product = productContext.Products.Find(id);
                if (product != null)
                {
                    productContext.Products.Remove(product);
                    productContext.SaveChanges();
                }
            }
        }
    }
}

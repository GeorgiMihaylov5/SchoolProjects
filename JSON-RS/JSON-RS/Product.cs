using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_RS
{
    public class Product
    {
        private int id;
        private string name;
        private decimal price;
        private int stock;
        private DateTime expiry;

        public Product(int id, string name, decimal price, int stock, DateTime expiry)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Stock = stock;
            this.Expiry = expiry;
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public decimal Price
        {
            get { return this.price; }
            set { this.price = value; }
        }
        public int Stock
        {
            get { return this.stock; }
            set { this.stock = value; }
        }
        public DateTime Expiry
        {
            get { return this.expiry; }
            set { this.expiry = value; }
        }
    }

}



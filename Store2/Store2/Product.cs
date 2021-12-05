using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store2
{
    public class Product
    {
        private string productName;
        private int quantity;
        private double price;

        public Product(string productName, int quantity, double price)
        {
            this.ProductName = productName;
            this.Quantity = quantity;
            this.Price = price;
        }

        public string ProductName
        {
            get { return productName; }
            set
            {
                if (value.Length < 3)
                {
                    throw new ArgumentException("The product name is too short!");
                }
                this.productName = value;
            }
        }
        public int Quantity 
        {
            get { return this.quantity; }
            set 
            {
                if (value < 1)
                {
                    throw new ArgumentException("The quantity must be a positive number!");
                }
                this.quantity = value;
            }
        }
        public double Price 
        {
            get { return this.price; } 
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("The price must be a positive number!");
                }
                this.price = value;
            } 
        }

        public override string ToString()
        {
            return $"{this.ProductName} {this.Quantity} cost {this.Price} lv.";
        }
        public void Discount()
        {
            if (this.Quantity > 10)
            {
                this.Price -= this.Price * 10 / 100;
            }
        }
    }
}

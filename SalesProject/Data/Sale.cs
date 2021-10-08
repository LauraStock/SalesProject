﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProject.Data
{
    class Sale
    {
        //properties
        public int salesID { get;}
        public string productName { get; }
        public int quantity { get; }
        public DateTime saleDate { get; }
        public double price { get; }
        /*
        public override double price 
        {
            get { return String.Format("{0:C}", price); }
            set { _price = value; }
        
        } //need to set get to currency format
        */

        //constructors
        public Sale(int salesID, string productName, int quantity, double price)
        {
            this.salesID = salesID;
            this.productName = productName;
            this.quantity = quantity;
            this.price = price;
            this.saleDate = DateTime.Now;
        }

        //methods
        public override string ToString()
        {
            string formattedPrice = String.Format("{0:C2}", price);
            return $"ID = {salesID}, Product = {productName}, Quantity = {quantity}, Price = {formattedPrice} - transaction made on {saleDate}";
        }
    }
}

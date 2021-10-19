using SalesProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProject.Services
{
    class SalesService
    {
        private readonly SalesRepository repo;
        public SalesService(SalesRepository repo)
        {
            this.repo = repo; 
        }

        public void Create(string productName, string quantityString, string priceString)
        {
            // need to check and change quantity and price to numbers
            int quantity = checkIsInt(quantityString);
            double price = checkIsDouble(priceString);
            Console.WriteLine("We are in create in the service");
            Sale item = new Sale(productName, quantity, price);
            repo.Create(item);
            Console.WriteLine("Creation has run");
        }

        public void ReadInDate(int function, int[] dates)
        {   
            
            // option should be 0-read all, 1-total, 2-min, 3-max, 4-average
            // if month is passed, can also select for month
            // check if year is 4 characters and within speciic dates 
            IList<Sale> saleList = repo.Read();
            // does the database contain anything for this time period?
            foreach (Sale thing in saleList)
            {
                Console.WriteLine(thing);
            }
        }

        public void ReadBetweenDates(int function, int year1, int year2, int month1 = 0, int month2 = 0, int day1 = 0, int day2 = 0)
        { 
            // if any of the values are 0 - only use the ones before that, at minimum will have the years
            // check which date is the lower one is the lower one
        }

        internal int checkIsInt(string input)
        {
            if (int.TryParse(input, out int value))
            {
                return value;
            }
            else
            {
                Console.WriteLine("We need to throw an exception here");
                return 0;
            }
        }

        internal double checkIsDouble(string input)
        {
            if (double.TryParse(input, out double value))
            {
                return value;
            }
            else
            {
                Console.WriteLine("We need to throw an exception here");
                return 0;
            }
        }

        internal DateTime checkIsDateTime(string input)
        {
            if (DateTime.TryParse(input, out DateTime value))
            {
                return value;
            }
            else
            {
                Console.WriteLine("We need to throw an exception here");
                return DateTime.Now;
            }

        }
        
        
}
}

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
        private static int counter;
        public SalesService(SalesRepository repo)
        {
            this.repo = repo;
            counter = 1;      
        }

        public void Create(string productName, string quantityString, string priceString)
        {
            // need to check and change quantity and price to numbers
            int quantity = checkIsInt(quantityString);
            double price = checkIsDouble(priceString);
            Sale item = new Sale(counter, productName, quantity, price);
            repo.Create(item);
            Console.WriteLine("Creation has run");
            counter++;
        }

        public void ReadInDate(int function, int year, int month = 0, int day = 0)
        {   
            // option should be 0-read all, 1-total, 2-min, 3-max, 4-average
            // if month is passed, can also slect for month
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

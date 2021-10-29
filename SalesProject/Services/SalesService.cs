using SalesProject.Data;
using SalesProject.Exceptions;
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
            try
            {
                int quantity = checkIsInt(quantityString);
                double price = checkIsDouble(priceString);
                Sale item = new Sale(productName, quantity, price);
                repo.Create(item);
                Console.WriteLine("Item has been added to the database");
            } catch (InvalidCastException)
            {
                Console.WriteLine("The value you entered is not valid.");
            }
            
        }

        public void Read(int selectOption, int dateOption, IList<String> input)
        {
            // if there's one date -> make the second one +1 year or month
            // if there's two dates -> make the second one +1 year or month
            // cahnge dateOption depending on the number of variables needing to be prepared for the SQL query

            // send dateOption (1 for year, 2 for month and three for day - number of var that needs preparing) and list of two dates
            // switch on the selectOption depending on output type
            //Console.WriteLine("We are in service read");
            //Console.WriteLine($"Select is ${selectOption}\n date option is ${dateOption}");
            IList<DateTime> dates = new List<DateTime>();
            try
            {
                for (int i = 0; i < input.Count; i++)
                {

                    DateTime date = DateTime.Parse(input[i]);
                    dates.Add(date);
                }
                dates = FormatDates(dateOption, dates);

                if (dateOption < 7)
                {
                    repo.Read(selectOption, dateOption, dates);
                }
                else
                {
                    throw (new OptionUnavailableException("This date option has not been added yet"));
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("The date you entered could not be recognised.");
            }
            
        }

        internal IList<DateTime> FormatDates(int dateOption, IList<DateTime> dates)
        {
            // need to make the between date options inclusive as between function on sql is val1<= date <val2 for months and days
            if (dateOption == 5)
            {
                //Console.WriteLine($"Dates are {dates[0]} and {dates[1]}");
                dates[1] = dates[1].AddMonths(1);
                //Console.WriteLine($"New Dates are {dates[0]} and {dates[1]}");
            }
            else if (dateOption == 6)
            {
                //Console.WriteLine($"Dates are {dates[0]} and {dates[1]}");
                dates[1] = dates[1].AddDays(1);
                //Console.WriteLine($"New Dates are {dates[0]} and {dates[1]}");
            }
            return dates;
        }

        internal int checkIsInt(string input)
        {
            if (int.TryParse(input, out int value))
            {
                return value;
            }
            else
            {
                throw new InvalidCastException();
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
                throw new InvalidCastException(); 
                return 0;
            }
        }
    }
}

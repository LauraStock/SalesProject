using SalesProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesProject.Exceptions;

namespace SalesProject.Controller
{
    class SalesController
    {
        private readonly SalesService service;

        public SalesController(SalesService service)
        {
            this.service = service;
        }

        public int NavigateMainMenu()
        {
            // take input from user
            // parse input to select option
            string[] menuOptions = {"data", "report", "q"};
            int option = getUserOption(menuOptions);
            return option;
            // switch on option
            //create or read
            //display sub menus

        }

        public void Create()
        {
            Console.Clear();
            Console.Write("Please enter product name: \n> ");
            string productName = Console.ReadLine();
            Console.Write("Quantity: \n> "); // could put an option to start again or go back?
            string quantity = Console.ReadLine();
            Console.Write("Price: \n> ");
            string price = Console.ReadLine();
            Console.WriteLine($"The sale to add is: \n{productName} \nQuantity {quantity} \nPrice {price} \n \n Continue? (Y/N)");
            string go = Console.ReadLine();
            service.Create(productName,quantity,price);
        }

        public void Read()
        {
            string[] readOptions = { "y", "m", "d", "between", "back" };
            int dateOption = getUserOption(readOptions);

            switch (dateOption)
            {
                case 4:
                    //get both dates
                    break;
                case 3:
                    int day = GetInt("day");
                    goto case 2;
                case 2:
                    int month = GetMonth();
                    goto case 1;
                case 1:
                    int year = GetInt("year");
                    break;
                default:
                     // could get rid of ^break statement and put the next menu in a function
                     // means you can go straight to ReadInDate or ReadBetweendates function in switch statement
                     // or put the two decisions the other way round?
                    break;
            }

            Console.WriteLine("What would you like to view?");
            Console.WriteLine("1. All sales");
            Console.WriteLine("2. Total sales");
            Console.WriteLine("3. Minimum price");
            Console.WriteLine("4. Maximum price");
            Console.WriteLine("5. Average price");
            Console.WriteLine("6. Back");

            string[] funOptions = { "all", "total", "min", "max", "av", "b" };
            int functionOption = getUserOption(funOptions);
            // option for function to be carried out should be 0-read all, 1-total, 2-min, 3-max, 4-average
              

        }

        internal int GetInt(string timeFrame)
        {
            Console.Clear();
            Console.WriteLine($"What {timeFrame} would you like to view?");
            Console.Write("> ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int year))
            {
                return year;
            } else 
            {
                Console.WriteLine("We need to throw an exception here");
                return 0;
            }
        }

        internal int GetMonth()
        {
            Console.Clear();
            Console.WriteLine("What month would you like to view?");
            string input = Console.ReadLine();
            string[] monthOptions = { "jan", "feb", "mar", "apr", "may", "jun", "aug", "sep", "oct", "nov", "dec" };
            int month = getUserOption(monthOptions);
            return month;
        }

        internal int getUserOption(string[] options)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            input = input.ToLower();
            for (int i = 1; i <= options.Length; i++)
            {
                if (input.Contains(i.ToString()) || input.Contains(options[i - 1]))
                { return i; }
                // need to throw an exception here
            }
            return 0;
        }

    }
}

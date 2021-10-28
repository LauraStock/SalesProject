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
            int option = 0;
            try
            {
                option = getUserOption(menuOptions);
            }
            catch (InvalidUserInputException e)
            {
                Console.WriteLine(e);
            }
            return option;
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

        public void ReadMenu1()
        {
            try
            {
                string[] funOptions = { "all", "total", "min", "max", "av", "count", "b" };
                int functionOption = getUserOption(funOptions);
                // option for function to be carried out should be 1-read all, 2-total, 3-min, 4-max, 5-average, 6-count, 7-back
                if (functionOption < 7)
                {
                    ReadMenu2(functionOption);
                }
            }
            catch (InvalidUserInputException e)
            {
                Console.WriteLine(e);
            }
                         
        }

        public void ReadMenu2(int functionOption)
        {
            Console.WriteLine("For what period?");
            Console.Write("1. A Year \n2. A Month \n3. A Day \n4. Between two years \n5. Between two months \n6. Between two dates \n7. Back");
            string[] readOptions = { "y", "m", "da", "between two y","between two m","between two d", "back" };
            int dateOption = getUserOption(readOptions);
            int loop = 1;
            IList <string> dates = new List<string>();

            if (dateOption > 3 && dateOption != 7)
            {
                loop = 2; // number of dates that need to be collected
            }

            for (int i = 0; i < loop; i++)
            {   if (dateOption == 7)
                { return; }
                string input = "";
                switch (dateOption % 3)
                {
                    case 0:
                        Console.WriteLine("Enter the date to view (dd/mm/yyyy)");
                        input = Console.ReadLine();
                        //dates.Add(GetInt("day"));
                        break;
                    case 2:
                        Console.WriteLine("Enter the month to view (mm/yyyy)");
                        input = Console.ReadLine();
                        break;
                    case 1:
                        Console.WriteLine("Enter the year to view (yyyy)");
                        string filler = (loop == 1) ? "01/01/" : "31/12/";
                        input = filler + Console.ReadLine();
                        break;
                    default:
                        break;
                }
                dates.Add(input);
            }
            try
            {
                service.Read(functionOption, dateOption, dates);
            }
            catch (OptionUnavailableException)
            { 
                
            }
            



        }

        internal int GetInt(string timeFrame)
        {
            Console.Clear();
            Console.WriteLine($"What {timeFrame} would you like to view? (enter number or 0 if not applicable)");
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
            Console.Write("What month would you like to view? (if not applicable, enter 0)");
            string[] monthOptions = { "jan", "feb", "mar", "apr", "may", "jun","jul", "aug", "sep", "oct", "nov", "dec" };
            int month = getUserOption(monthOptions);
            return month;
        }

        internal int getUserOption(string[] options)
        {
            Console.Write("\n> ");
            string input = Console.ReadLine();
            input = input.ToLower();
            for (int i = 1; i <= options.Length; i++)
            {
                if (input.Contains(i.ToString()) || input.Contains(options[i - 1]))
                { return i; }
                    
                
            }
            throw (new InvalidUserInputException("The value you entered could not be recognised as an option"));
            return 0;
        }

    }
}

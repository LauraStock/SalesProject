﻿using SalesProject.Services;
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

        public void ReadMenu1()
        {
            string[] funOptions = { "all", "total", "min", "max", "av", "b" };
            int functionOption = getUserOption(funOptions);
            // option for function to be carried out should be 0-read all, 1-total, 2-min, 3-max, 4-average
            if (functionOption < 6)
            {
                ReadMenu2(functionOption);
            }              
        }

        public void ReadMenu2(int functionOption)
        {
            Console.WriteLine("For what period?");
            Console.Write("1. A Year \n2. A Month \n3. A Day \n4. Between two years \n5. Between two months \n6. Between two dates \n7. Back");
            string[] readOptions = { "y", "m", "d", "between two y","between two m","between two d", "back" };
            int dateOption = getUserOption(readOptions);
            int loop = 1;

            if (dateOption > 3 && dateOption != 7)
            {
                loop = 2; // number of dates that need to be collected
            }

            int[] dates = { };

            for (int i = 0; i < loop; i++)
            {   if (dateOption == 7)
                { break; }
                Console.WriteLine($"date option % 3 is {dateOption % 3}");
                switch (dateOption % 3)
                {
                    case 0:
                        dates.Append(GetInt("day"));
                        goto case 2;
                    case 2:
                        dates.Append(GetMonth());
                        goto case 1;
                    case 1:
                        dates.Append(GetInt("year"));
                        break;
                    default:
                        // could get rid of ^break statement and put the next menu in a function
                        // means you can go straight to ReadInDate or ReadBetweendates function in switch statement
                        // or put the two decisions the other way round?
                        break;
                }
            }

            if (loop == 2)
            {
                service.ReadBetweenDates(functionOption, dates);
            }
            else
            {
                service.ReadInDate(functionOption, dates);
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
            string[] monthOptions = { "jan", "feb", "mar", "apr", "may", "jun", "aug", "sep", "oct", "nov", "dec" };
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
                // need to throw an exception here
            }
            return 0;
        }

    }
}

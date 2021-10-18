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

        public void Read()
        {
            string[] readOptions = { "sales by year", "sales by month", "total sales by year", "total sales by month", "back" };
            int option = getUserOption(readOptions);
            int year = GetYear();

            switch (option)
            {
                
                    
            }

        }

        internal int GetYear()
        {
            Console.Clear();
            Console.WriteLine("What year would you like to view?");
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

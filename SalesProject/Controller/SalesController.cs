using SalesProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        internal int getUserOption(string[] options)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            input = input.ToLower();
            for (int i = 1; i <= options.Length; i++)
            {

                if (input.Contains(i.ToString()) || input.Contains(options[i - 1]))
                { return i; }
            }
            return 0;
        }

    }
}

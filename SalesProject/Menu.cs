using SalesProject.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProject
{
    class Menu
    {
        private readonly SalesController controller;
        public Menu(SalesController controller)
        {
            this.controller = controller;        
        }
        internal void MenuLoop()
        {
            bool inLoop = true;

            while (inLoop)
            {
                Console.Clear();
                Console.WriteLine("-----SALES-----");
                Console.WriteLine("1. Data Entry");
                Console.WriteLine("2. Reports");
                Console.WriteLine("3. Quit");

                int option = controller.NavigateMainMenu();

                switch (option)
                {
                    case 1:
                        controller.Create(); 
                        break;
                    case 2:
                        ReadMenu();
                        break;
                    case 3:
                        inLoop = false;
                        break;
                }
            }
            
        }

        internal void ReadMenu()
        {
            Console.Clear();
            Console.WriteLine("Would you like to see reports for");
            Console.WriteLine("1. A Year");
            Console.WriteLine("2. A Month");
            Console.WriteLine("3. A Day");
            Console.WriteLine("4. Between two dates");
            Console.WriteLine("5. Back");
            controller.Read();
        }


        

        
    }
}

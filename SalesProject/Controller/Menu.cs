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
            Console.WriteLine("What would you like to view?");
            Console.WriteLine("1. All sales");
            Console.WriteLine("2. Total sales");
            Console.WriteLine("3. Minimum price");
            Console.WriteLine("4. Maximum price");
            Console.WriteLine("5. Average price");
            Console.WriteLine("6. Back");
            controller.ReadMenu1();
        }


        

        
    }
}

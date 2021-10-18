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
                Console.WriteLine("-----SALES-----");
                Console.WriteLine("1. Data Entry");
                Console.WriteLine("2. Reports");
                Console.WriteLine("3. Quit");
                Console.Write("> ");

                int option = controller.NavigateMainMenu();

                switch (option)
                {
                    case 1:
                        CreateMenu();
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

        private void ReadMenu()
        {
            throw new NotImplementedException();
        }

        internal void CreateMenu()
        { 
        }

        
    }
}

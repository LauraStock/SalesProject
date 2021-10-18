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

        public void Create()
        {
            Sale item = new Sale(1, "Peanuts", 2, 4.00);

            repo.Create(item);
            Console.WriteLine("Creation has run");
        }

        public void Read()
        {
            IList<Sale> saleList = repo.Read();

            foreach (Sale thing in saleList)
            {
                Console.WriteLine(thing);
            }
        }
        
        
}
}

﻿using SalesProject.Data;
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
            int quantity = checkIsInt(quantityString);
            double price = checkIsDouble(priceString);
            Console.WriteLine("We are in create in the service");
            Sale item = new Sale(productName, quantity, price);
            repo.Create(item);
            Console.WriteLine("Creation has run");
        }

        public void Read(int selectOption, int dateOption, IList<DateTime> date)
        {
            //date = ValidateDates(dateOption, date);

            // if there's one date -> make the second one +1 year or month
            // if there's two dates -> make the second one +1 year or month
            // cahnge dateOption depending on the number of variables needing to be prepared for the SQL query

            // send dateOption (1 for year, 2 for month and three for day - number of var that needs preparing) and list of two dates
            // switch on the selectOption depending on output type
            Console.WriteLine("We are in service read");
            Console.WriteLine($"Select is ${selectOption}\n date option is ${dateOption}");

            if (dateOption < 4) {
                repo.Read(selectOption,dateOption,date);
            }


        }

        internal IList<string> ConditionVariables(int dateOption, IList<DateTime> dates)
        {
            IList<string> conditionVariables = new List<string>();

            if (dates.Count == 1)
            {
                switch (dateOption)
                {
                    case 1:
                        dates.Add(dates[0].AddYears(1));
                        break;
                    case 2:
                        dates.Add(dates[0].AddMonths(1));
                        break;
                    case 3:
                        dates.Add(dates[0]);
                        break;
                }
            } else
            {
                switch (dateOption)
                {
                    case 1:
                        dates[1] = dates[1].AddYears(1);
                        break;
                    case 2:
                        dates[1] = dates[1].AddMonths(1);
                        break;
                    case 3:
                        break;
                }


            }
            Console.WriteLine($"date count is {dates.Count}");

            // loop through each date
            for (int i = 0; i < 2; i++)
            {
                // loop through year, month, day
                for (int j = 1; j <= 3; j++)
                {
                    Console.WriteLine($"i is {i}, j is {j}");
                    //Console.WriteLine("Ready to write date variables");
                    if (j <= dateOption)
                    {
                        switch (j)
                        {
                            case 1:
                                conditionVariables.Add(dates[i].Year.ToString());
                                break;
                            case 2:
                                conditionVariables.Add(dates[i].Month.ToString());
                                break;
                            case 3:
                                conditionVariables.Add(dates[i].Day.ToString());
                                break;
                        }
                        
                    }
                    else
                    {
                        conditionVariables.Add("00");
                    }
                    // if j <= dateOption add the dates[i] variable, otherwise add 0
                    
                }
                //Console.WriteLine(dates[i]);

            }
            return conditionVariables;
        }

            internal IList<int> ValidateDates(int numDates, IList<DateTime> date)
        {
            if (numDates == 2)
            {
                int numValues = date.Count / 2;
                DateTime now = DateTime.Now;
                
            }
            // if between two dates (numDates == 2)
            // split list into two?
            //check number of elements in each half
            // validate years - pad to 4 digits and check between values
            // check which is lower
            // validate months
            // check which is first
            // validate days
            // check which is first
            // fill in any missing gaps

            // if any of the values are 0 - only use the ones before that, at minimum will have the years
            // check which date is the lower one is the lower one
            return (IList<int>)date;
        }

        

        internal int checkIsInt(string input)
        {
            if (int.TryParse(input, out int value))
            {
                return value;
            }
            else
            {
                Console.WriteLine("We need to throw an exception here");
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
                Console.WriteLine("We need to throw an exception here");
                return 0;
            }
        }
               
        
}
}

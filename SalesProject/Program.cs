using System;
using System.Collections.Generic;
using System.Globalization;
using MySql.Data.MySqlClient;
using SalesProject.Data;

namespace SalesProject
{
    class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection connection = null;

            using (connection = MySqlUtils.GetConnection())
            {
                Sale item = new Sale(1,"Peanuts",2, 4.00);

                SalesRepository repo = new SalesRepository(connection);
                repo.Create(item);
                Console.WriteLine("Creation has run");
                IList<Sale> saleList = repo.Read();

                foreach (Sale thing in saleList) {
                    Console.WriteLine(thing);
                }
                
                //Console.WriteLine(repo.Exists());
                /*
                //opening the db connection and creating the sales db if it does not exist.
                connection.Open();
                string path = "./" + @"\Data\dbSetup.sql";
                MySqlUtils.RunSchemaFile(path, connection);
                */
                //connection.Ping();
                //bool connectionOpen = connection.Ping();
                //Console.WriteLine($"Connection status: {connection.State} \nPing successfull: {connectionOpen} \nDB Version: {connection.ServerVersion}");



            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using MySql.Data.MySqlClient;
using SalesProject.Controller;
using SalesProject.Data;
using SalesProject.Services;

namespace SalesProject
{
    class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection connection = null;

            using (connection = MySqlUtils.GetConnection())
            {
                SalesRepository repo = new SalesRepository(connection);
                SalesService service = new SalesService(repo);
                SalesController controller = new SalesController(service);

                Menu menu = new Menu(controller);
                Console.WriteLine(menu);
                menu.MenuLoop();

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

using System;
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
                SalesRepository repo = new SalesRepository(connection);
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

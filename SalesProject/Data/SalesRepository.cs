using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProject.Data
{
    class SalesRepository
    {
        public MySqlConnection connection { get; }

        public SalesRepository(MySqlConnection connection)
        {
            this.connection = connection;
            SetupDB();

        }

        internal void SetupDB() //runs the dbSetup.sql file to create sales db if it doesn't already exist.
        {
            connection.Open();
            string path = "./" + @"\Data\dbSetup.sql";
            MySqlUtils.RunSchemaFile(path, connection);
            connection.Close();
        }

        internal void Create()
        { 
        
        }

        internal bool Exists()
        {
            
            string MySqlString = "SELECT COUNT(*) FROM sales WHERE SalesID=@SalesID";
            int salesID = 1;

            /*
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = MySqlString;
            command.Parameters.AddWithValue("@SalesID", salesID);

            connection.Open();
            command.Prepare();
            */
            MySqlCommand command = PrepCommandInt(MySqlString, salesID);
            int result = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            return result > 0;
        }
        
        internal MySqlCommand PrepCommandInt(string sqlCommand, int val)
        {
            //possibly put this in as an extension to the MySqlCommand class?
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sqlCommand;
            command.Parameters.AddWithValue("@SalesID", val);

            connection.Open();
            command.Prepare();
            Console.WriteLine("In prep command function");
            return command;
        }
        
    }
}

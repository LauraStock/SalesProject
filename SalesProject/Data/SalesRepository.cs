using MySql.Data.MySqlClient;
using System;
using System.Collections;
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

        internal void Create(Sale item)
        { 

            string MySqlString = $"INSERT INTO sales(ProductName, Quantity, Price) VALUES(@productName,quantity,@price)";
            MySqlCommand command = connection.CreateCommand();

            //could use prepare statement as PrepCommand(string SqlCommand, string placeHolder1, var val1, string placeHolder2, var val2...)
            command.CommandText = MySqlString;
            Console.WriteLine($"nom: {item.productName}");
            command.Parameters.AddWithValue("@productName", $"{item.productName}");
            command.Parameters.AddWithValue("@quantity",item.quantity);
            command.Parameters.AddWithValue("@price",item.price);
            

            connection.Open();
            command.Prepare();
            command.ExecuteNonQuery();
            connection.Close();

        }

        internal IList<Sale> Read()
        {
            IList salesList = new List<Sale>();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM sales";

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int salesID = reader.GetFieldValue<int>(0);
                string productName = reader.GetFieldValue<string>(1);
                int quantity = reader.GetFieldValue<int>(2);
                double price = reader.GetFieldValue<double>(3);
                DateTime saleDate = reader.GetFieldValue<DateTime>(4);

                Sale sale = new Sale(salesID, productName, quantity, price, saleDate);
                salesList.Add(sale);
            }
            connection.Close();

            return (IList<Sale>) salesList;

        }

        internal bool Exists()
        {
            
            string MySqlString = "SELECT COUNT(*) FROM sales WHERE SalesID=@SalesID";
            int salesID = 1;
            string placeHolder = "@SalesID";

            /*
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = MySqlString;
            command.Parameters.AddWithValue("@SalesID", salesID);

            connection.Open();
            command.Prepare();
            */

            MySqlCommand command = PrepCommandInt(MySqlString, placeHolder, salesID);
            int result = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            return result > 0;
        }
        
        //can override this method for different number of placeholders and types (use var type)?
        internal MySqlCommand PrepCommandInt(string sqlCommand,string placeHolder1, int val1)
        {           
            //how to get number of arguments passed to loop through
            //int numArgs 
            //possibly put this in as an extension to the MySqlCommand class?
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sqlCommand;
            command.Parameters.AddWithValue(placeHolder1, val1);

            connection.Open();
            command.Prepare();
            Console.WriteLine("In prep command function");
            return command;
        }       
        
    }
}

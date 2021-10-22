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

            string MySqlString = $"INSERT INTO sales(ProductName, Quantity, Price) VALUES(@productName,@quantity,@price)";
            MySqlCommand command = connection.CreateCommand();

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

        internal string ConditionQuery(int dateOption)
        {
            //condition statement will always be in the same format

            //WHERE SaleDate BETWEEN '@year1-@month1-@day1' AND '@year2-@month2-@day2';

            string conditionString = "WHERE SaleDate BETWEEN '@year1-@month1-@day1' AND '@year2-@month2-@day2';";
            // format create string " where SaleDate between '@year1-@month1-@day1' and '@year2-@month2-@day2'
            //create placeholder list @year1 etc strings
            // if one date
            // count number of values
            // build string ' where year(SaleDate) = @year1' etc
            // create placeholder string list
            return conditionString;
        }

        
            
            // also need a set condition variables depending on dateOption.
            // Lists would be: format -> year1,month1,day1,year2,month2,day2
            // 1 -> date[0].Year, '0', '0', date[1].Year, '0','0'
            // 2-> date[0].Year, date[0].Month, '0', date[1].Year,date[1].Month,'0'
            // 3 -> date[0].Year,date[0].Month, date[0].Day, date[1].Year, date[1].Month, date[1].Day


        /*
        internal IList<Sale> ReadOutList(int dateOption, IList<DateTime> date)
        {
            string selectQuery = "SELECT * FROM sales "; // output type IList
            string condQuery = ConditionQuery(dateOption);
            string query = selectQuery + condQuery;
            Console.WriteLine(query);
        }

        internal MySqlCommand PrepCommandDate(string sqlCommand, int dateOption, IList<DateTime> date)
        {
            string[] placeholders = {"@year1", "@month1", "@date1","@year2","@month2","@date2"};
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sqlCommand;

            if (date.Count == 2)
            { 
                for 
            }


            command.Parameters.AddWithValue(placeholders, val1);

            connection.Open();
            command.Prepare();
            Console.WriteLine("In prep command function");
            return command;
        }

        // Need 4 different read functions that output different types
        // double, int, list and sale
        // Each one then needs:
        // select string - may need to differentiate around different ones
        // call condition string function which outputs string 
        // put the two parts of the sql string together
        // pass string and list of dates to prep command function which prepares this
        // reader function -> two types: multi column, single value (execute scalar?)
        // prints output to console and returns success bool


        internal void Read(int function, IList<int> date)
        {
            IList salesList = new List<Sale>();

            MySqlCommand command = connection.CreateCommand();
            string selectCommand = "";
            

            switch (function)
            {
                case 1:
                    selectCommand = "SELECT * FROM sales"; // output type IList
                    break;
                case 2:
                    selectCommand = "SELECT sum(price) from sales"; // output type double
                    break;
                case 3:
                    selectCommand = "SELECT *, MIN(price) FROM sales"; // output type Sale
                    break;
                case 4:
                    selectCommand = "SELECT *, MAX(price) FROM sales"; // Output type Sale
                    break;
                case 5:
                    selectCommand = "SELECT AVG(price) FROM sales"; // output type double
                    break;
                case 6:
                    selectCommand = "SELECT COUNT(price) FROM sales"; // output type int
                    break;
                default:
                    break;
            }
            string[] dateString = {"YEAR(SaleDate) = @year"," AND MONTH(SaleDate) = @month"," AND DAY(SaleDate) = @day"};
            string conditionCommand = "";
            for (int i = 0; i < date.Count; i++)
            {
                conditionCommand = conditionCommand + dateString[i];
                
            }
            command.CommandText = selectCommand + " " + conditionCommand + ";";
            Console.WriteLine(selectCommand + " " + conditionCommand + ";");

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
        */
    }
}

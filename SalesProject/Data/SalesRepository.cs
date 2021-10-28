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

        // Need 4 different read functions that output different types
        // double, int, list and sale
        // Each one then needs:
        // select string - may need to differentiate around different ones
        // call condition string function which outputs string 
        // put the two parts of the sql string together
        // pass string and list of dates to prep command function which prepares this
        // reader function -> two types: multi column, single value (execute scalar?)
        // prints output to console and returns success bool
        internal void Read(int selectOption, int dateOption, IList<DateTime> date)
        {
            //generate sql query string
            string sqlQuery = SelectQuery(selectOption);
            sqlQuery = sqlQuery + ConditionQuery(dateOption);
            Console.WriteLine(sqlQuery);

            //prepare command 
            connection.Open();
            MySqlCommand command = PrepReadCommand(sqlQuery, dateOption, date);

            switch (selectOption) // 1-view all, 2-total, 3-min, 4-max, 5-average,6-count
            {
                case 1:
                    ReadOutList(command);
                    break;
                case 2:
                    ReadOutDouble(command);
                    break;
                case 3:
                    ReadOutSale(command);
                    break;
                case 4:
                    goto case 3;
                case 5:
                    goto case 2;
                case 6:
                    ReadOutInt(command);
                    break;
            }
            connection.Close();
        }
        internal void ReadOutList(MySqlCommand command)
        {
            IList< Sale > saleList = new List<Sale>();
            MySqlDataReader reader = command.ExecuteReader();
            Console.WriteLine(reader.HasRows);

            while (reader.Read())
            {
                int salesID = reader.GetFieldValue<int>(0);
                Console.WriteLine(salesID);
                
                string productName = reader.GetFieldValue<string>(1);
                int quantity = reader.GetFieldValue<int>(2);
                double price = reader.GetFieldValue<double>(3);
                DateTime saleDate = reader.GetFieldValue<DateTime>(4);

                Sale sale = new Sale(salesID, productName, quantity, price, saleDate);
                saleList.Add(sale);
                
            }
            reader.Close();
            for (int i = 0; i < saleList.Count; i++)
            {
                Console.WriteLine(saleList[i]);
            }
        }

        internal void ReadOutDouble(MySqlCommand command)
        {
            double result = Convert.ToDouble(command.ExecuteScalar());
            Console.WriteLine($"The result is {String.Format("{0:C2}", result)}");
        }

        internal void ReadOutSale(MySqlCommand command)
        { 
            MySqlDataReader reader = command.ExecuteReader();
            Console.WriteLine(reader.HasRows);

            while (reader.Read())
            {
                int salesID = reader.GetFieldValue<int>(0);
                //Console.WriteLine(salesID);

                string productName = reader.GetFieldValue<string>(1);
                int quantity = reader.GetFieldValue<int>(2);
                double price = reader.GetFieldValue<double>(3);
                DateTime saleDate = reader.GetFieldValue<DateTime>(4);
                Sale sale = new Sale(salesID, productName, quantity, price, saleDate);
                Console.WriteLine(sale);
            }            
            reader.Close();
        }

        internal void ReadOutInt(MySqlCommand command)
        {
            int result = Convert.ToInt32(command.ExecuteScalar());
            Console.WriteLine($"The number of sales is {result}");
        }
        
        internal string SelectQuery(int selectOption)
        {
            string selectCommand = "";
            switch (selectOption)
            {
                case 1:
                    selectCommand = "SELECT * FROM sales"; // output type IList
                    break;
                case 2:
                    selectCommand = "SELECT sum(price*quantity) from sales"; // output type double
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
            return selectCommand;
        }

        internal string ConditionQuery(int dateOption)
        {
            string sqlCondition = " WHERE";
            switch (dateOption)
            {
                case 3:
                    sqlCondition = sqlCondition + " DAY(SaleDate) = @day1 AND";
                    goto case 2;
                case 2:
                    sqlCondition = sqlCondition + " MONTH(SaleDate) = @month1 AND";
                    goto case 1;
                case 1:
                    sqlCondition = sqlCondition + " YEAR(SaleDate) = @year1";
                    break;
                case 4:
                    sqlCondition = sqlCondition + "YEAR(SaleDate) BETWEEN @year1 AND @year2";
                    break;
                    // How to do between full dates?
            }
            return sqlCondition;
        }
        internal MySqlCommand PrepReadCommand(string sqlQuery, int dateOption, IList<DateTime> date)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sqlQuery;

            switch (dateOption)
            {
                case 3:
                    command.Parameters.AddWithValue("@day1", date[0].Day);
                    goto case 2;
                case 2:
                    command.Parameters.AddWithValue("@month1", date[0].Month);
                    goto case 1;
                case 1:
                    command.Parameters.AddWithValue("@year1", date[0].Year);
                    break;
                case 4:
                    command.Parameters.AddWithValue("@year2", date[1].Year);
                    goto case 1;
            }
            command.Prepare();
            return command;
        }
        /*        
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
        */
    }
}

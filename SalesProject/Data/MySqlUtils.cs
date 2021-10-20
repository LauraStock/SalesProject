using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProject.Data
{
    class MySqlUtils
    {
        public static MySqlConnectionStringBuilder connectionString { get; set; } = new MySqlConnectionStringBuilder
        {
            Server = "127.0.0.1", // server hosting the mysql server
            UserID = "root", // user id for mysql
            Password = "root", // password for mysql
            Port = 3306, // port to connect on, 3306 is default for MySQL
            Database = "test", // name of db to connect to in rdbms
            AllowBatch = true, // allows batches of commands to be sent, defaults to true,
            AllowLoadLocalInfileInPath = "./", // only files in specified dir can be uploaded, AllowLoadLocalInfile will override if set to true
            AllowLoadLocalInfile = false, // allows file uploads from anywhere if set to true
            ConnectionTimeout = 30 // default is 15, amount of time until app throws a timeout error
        };

        /*
        internal static void RunSchema(string v, object connection)
        {
            throw new NotImplementedException();
        }
        */
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString.ConnectionString);
        }

        public static void RunSchemaFile(string path, MySqlConnection connection)
        {
            string schema = File.ReadAllText(path);
            MySqlCommand mySqlCommand = connection.CreateCommand();
            mySqlCommand.CommandText = schema;

            //.ExecuteNonQuery(); is used when we are not reading from the database but manipulating it.
            mySqlCommand.ExecuteNonQuery();

        }
    }
}

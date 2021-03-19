using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagramme
{
    public class UserRepository
    {
        // A connectionstring tells an app which DB it can use to store / fetch data
        private const string connectionString = @"
                Data Source=.\SQLEXPRESS;Initial Catalog=Instagramme;Integrated Security=True";

        public void AddUser(string name)
        {
            // !!!BAD PRACTICE!!! 
            //string query = $"INSERT INTO USERS(UserName) VALUES({name})";

            // Good practice. Use SQL Parameters to sanitize input.
            string query = $"INSERT INTO USERS(UserName) VALUES(@placeholder)";
                       

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Sanitize user input by parsing it through a SQL Parameter(s)
                command.Parameters.AddWithValue("@placeholder", name);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        
        public void GetAllUsers()
        {
            string query = "SELECT * FROM [Instagramme].[dbo].[Users]";

            GetData(query);
        }

        public void GetXAmountOfUsers(string amountOfUsers)
        {
            // Good practice. Use SQL Parameters to sanitize input, even in Select statements
            string query = $"SELECT * FROM [Instagramme].[dbo].[Users] WHERE UserId <= @maxNumber";
            
            // A connection has to be open between app and DB to allow for communication
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // A command will execute a query on an open connection
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@maxNumber", amountOfUsers);

                // We have to manually open a connection
                // Open late, close early -> An open connection is precious.
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                ReadAllRows(reader);
            }
        }

        private void GetData(string query)
        {
            // A connection has to be open between app and DB to allow for communication
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // A command will execute a query on an open connection
                SqlCommand command = new SqlCommand(query, connection);
                
                // We have to manually open a connection
                // Open late, close early -> An open connection is precious.
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                ReadAllRows(reader);
            }
        }

        private void ReadAllRows(SqlDataReader reader)
        {
            while (reader.Read())
            {
                string row = $"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetDateTime(2)}";
                Console.WriteLine(row);
            }
        }
    }
}

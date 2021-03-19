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

        public void GetAllUsers()
        {
            string query = "SELECT * FROM [Instagramme].[dbo].[Users]";

            GetData(query);
        }

        public void GetXAmountOfUsers(int amountOfUsers)
        {
            string query = $"SELECT * FROM [Instagramme].[dbo].[Users] WHERE UserId <= {amountOfUsers}";
            GetData(query);
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

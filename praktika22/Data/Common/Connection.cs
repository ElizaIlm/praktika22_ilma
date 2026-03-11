using MySql.Data.MySqlClient;
using System;

namespace praktika22.Data.Common
{
    public class Connection
    {
        readonly static string ConnectionData = "server=127.0.0.1;port=3306;database=Shop;uid=root";

		public static MySqlConnection MySqlOpen()
        {
            try
            {
                MySqlConnection NewMySqlConnection = new MySqlConnection(ConnectionData);
                NewMySqlConnection.Open();
                return NewMySqlConnection;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка подключения к БД: " + ex.Message);
            }
        }

        public static MySqlDataReader MySqlQuery(string Query, MySqlConnection Connection)
        {
            try
            {
                MySqlCommand NewMySqlCommand = new MySqlCommand(Query, Connection);
                return NewMySqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка выполнения запроса: " + ex.Message);
            }
        }

        public static void MySqlClose(MySqlConnection connection)
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
using MySql.Data.MySqlClient;

namespace praktika22.Data.Common
{
	public class Connection
	{
		readonly static string ConnectionData = "server=127.0.0.1;port=3306;uid=root;";
		public static MySqlConnection MySqlOpen()
		{
			MySqlConnection NewMySqlConnection = new MySqlConnection(ConnectionData);
			NewMySqlConnection.Open();

			return NewMySqlConnection;
		}
		public static MySqlDataReader MySqlQuery(string Query, MySqlConnection Connection)
		{
			MySqlCommand NewMySqlCommand = new MySqlCommand(Query, Connection);
			return NewMySqlCommand.ExecuteReader();
		}
		public static void MySqlClose(MySqlConnection connection)
		{
			connection.Close();
		}
	}
}

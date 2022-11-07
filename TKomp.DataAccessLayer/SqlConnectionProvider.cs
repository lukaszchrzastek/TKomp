using System;
using System.Data.SqlClient;
using TKomp.Domain;

namespace TKomp.DataAccessLayer
{
    public class SqlConnectionProvider : ISqlConnectionProvider
    {
        private string _connectionString;
        private string _username;
        private string _password;

        public SqlConnectionProvider(string connectionString) {
            _connectionString = connectionString;
        }
        
        public string GetConnectionString()
        {
            return BuildSqlConnectionString(_username, _password);
        }

        public void SetCredentials(string username, string password)
        {
            _username = username;
            _password = password;
        }

        private string BuildSqlConnectionString(string username, string password) {            
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(_connectionString);
            sqlConnectionStringBuilder.UserID = username;
            sqlConnectionStringBuilder.Password = password;
            return sqlConnectionStringBuilder.ConnectionString;
        }

        public bool Test(string username, string password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
                return false;
            using (SqlConnection connection = new SqlConnection(BuildSqlConnectionString(username, password)))
            {
                try
                {
                    connection.Open();
                    return connection.State == System.Data.ConnectionState.Open;
                }
                catch (Exception) {
                    return false;
                }
            }
        }
    }
}
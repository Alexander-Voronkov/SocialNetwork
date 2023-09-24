using Microsoft.Data.SqlClient;

namespace UIApp.Utils
{
    public static partial class Utils
    {
        public static async Task InitHangfireDb(SqlConnectionStringBuilder builder)
        {
            builder.InitialCatalog = "";
            using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'DB_HANGFIRE_JOBS')  BEGIN  CREATE DATABASE DB_HANGFIRE_JOBS;  END";
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}

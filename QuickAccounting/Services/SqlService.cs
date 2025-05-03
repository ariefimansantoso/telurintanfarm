using Microsoft.Data.SqlClient;
using System.Data;

namespace QuickAccounting.Services
{
    public class SqlService
    {
        private readonly string _connectionString;

        public SqlService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<DataTable> ExecuteSqlAsync(string sql)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new SqlCommand(sql, connection);
            using var reader = await command.ExecuteReaderAsync();

            var dt = new DataTable();
            dt.Load(reader);
            return dt;
        }
    }
}

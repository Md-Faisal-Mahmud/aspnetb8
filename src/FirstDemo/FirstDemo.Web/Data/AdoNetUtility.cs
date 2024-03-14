using System.Data.Common;
using System.Data.SqlClient;

namespace FirstDemo.Web.Data
{
    public class AdoNetUtility
    {
        private readonly DbConnection _connection;
        public AdoNetUtility(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }
        public int WriteOperation(string sql, IList<DbParameter> parameters)
        {
            using var command = CreateCommand(sql, parameters);

            int effected = command.ExecuteNonQuery();

            return effected;
        }

        public IList<Dictionary<string, object>> ReadOperation(string sql, 
            IList<DbParameter> parameters, bool isStoredProcedure)
        {
            using var command = CreateCommand(sql, parameters);

            if(isStoredProcedure)
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
            }

            var reader = command.ExecuteReader();

            var rows = new List<Dictionary<string, object>>();

            while(reader.Read())
            {
                var row = new Dictionary<string, object>();

                for(int i = 0; i < reader.FieldCount; i++)
                {
                    string columnName = reader.GetName(i);
                    object columnValue = reader.GetValue(i);

                    row.Add(columnName, columnValue);
                }

                rows.Add(row);
            }

            return rows;
        }

        private DbCommand CreateCommand(string sql, IList<DbParameter> parameters)
        {
            DbCommand command = new SqlCommand(sql, _connection as SqlConnection);

            if (_connection.State != System.Data.ConnectionState.Open)
            {
                _connection.Open();
            }

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            return command;
        }
    }
}

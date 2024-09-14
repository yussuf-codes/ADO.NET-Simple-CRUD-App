using System.Data;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Persistence.Utilities.SQLProvider;

public class SQLServerProvider : ISQLProvider
{
    private readonly string _connectionString;

    public SQLServerProvider(string connectionString)
    {
        _connectionString = connectionString;
    }

    public object ExecuteQuery(string query, List<StoredProcedureArg>? args = null)
    {
        SqlCommand command = new(query) { CommandType = CommandType.StoredProcedure };
        if (args != null)
        {
            for (int i = 0; i < args.Count; i++)
                command.Parameters.AddWithValue(args[i].Name, args[i].Value);
        }
        using SqlConnection sqlConnection = new SqlConnection(_connectionString);
        command.Connection = sqlConnection;
        sqlConnection.Open();
        using SqlDataAdapter sqlDataAdapter = new(command);
        DataTable dataTable = new DataTable();
        sqlDataAdapter.Fill(dataTable);
        sqlConnection.Close();
        return dataTable;
    }

    public int ExecuteNonQuery(string query, List<StoredProcedureArg>? args = null)
    {
        SqlCommand command = new(query) { CommandType = CommandType.StoredProcedure };
        if (args != null)
        {
            for (int i = 0; i < args.Count; i++)
                command.Parameters.AddWithValue(args[i].Name, args[i].Value);
        }
        using SqlConnection sqlConnection = new SqlConnection(_connectionString);
        command.Connection = sqlConnection;
        sqlConnection.Open();
        int affectedRows = command.ExecuteNonQuery();
        sqlConnection.Close();
        return affectedRows;
    }

    public object ExecuteScalar(string query, List<StoredProcedureArg>? args = null)
    {
        SqlCommand command = new(query) { CommandType = CommandType.StoredProcedure };
        if (args != null)
        {
            for (int i = 0; i < args.Count; i++)
                command.Parameters.AddWithValue(args[i].Name, args[i].Value);
        }
        using SqlConnection sqlConnection = new SqlConnection(_connectionString);
        command.Connection = sqlConnection;
        sqlConnection.Open();
        object obj = command.ExecuteScalar();
        sqlConnection.Close();
        return obj;
    }
}

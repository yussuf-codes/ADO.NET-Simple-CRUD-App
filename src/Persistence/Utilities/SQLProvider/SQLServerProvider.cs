using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Persistence.Utilities.SQLProvider;

public class SQLServerProvider : ISQLProvider
{
    private readonly string _connectionString;

    public SQLServerProvider()
    {
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly());

        IConfiguration configuration = configurationBuilder.Build();

        string? connectionString = configuration["ConnectionStrings:DefaultConnection"];

        if (connectionString is null)
            throw new NullReferenceException("Connection string is null");

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

    public int ExecuteCommand(string query, List<StoredProcedureArg>? args = null)
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

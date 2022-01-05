using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.Bookings.Repository;

public class SqlServerConnectionFactory : IDbConnectionFactory
{
    private readonly IConfiguration _connectionString;
    private readonly string dbConnection;
    public SqlServerConnectionFactory(IConfiguration connectionString)
    {
        _connectionString = connectionString;
       dbConnection = _connectionString.GetConnectionString("Default");       
    }

    public IDbConnection CreateConnection() => new SqlConnection(dbConnection);
}


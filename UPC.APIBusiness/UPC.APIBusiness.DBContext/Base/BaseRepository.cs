using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace DBContext
{
    public class BaseRepository
    {
        public static IConfigurationRoot Configuration { get; set; }

        public SqlConnection GetSqlConnection(bool open = true)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            string cs = Configuration["Logging:AppSettings:SqlConnectionString"];

            var csb = new SqlConnectionStringBuilder(cs) { };

            var conn = new SqlConnection(csb.ConnectionString);
            if(conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            //if (open) conn.Open();
            return conn;
        }

    }
}

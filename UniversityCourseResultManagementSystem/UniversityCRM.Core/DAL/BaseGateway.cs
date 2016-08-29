using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace UniversityCourseResultManagementSystem.UniversityCRM.Core.DAL
{
    public class BaseGateway
    {
        public string ConnectionString { get; set; }
        public SqlConnection SqlConnection { get; set; }
        public SqlCommand SqlCommand { get; set; }


        public BaseGateway()
        {
            this.ConnectionString = WebConfigurationManager.ConnectionStrings["UniversityCRMConnectionString"].ConnectionString;
            this.SqlConnection = new SqlConnection(ConnectionString);
            this.SqlCommand = new SqlCommand();
            this.SqlCommand.Connection = SqlConnection;
        }
    }
}
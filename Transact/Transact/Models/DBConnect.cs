using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Transact.Models
{
    public static class DBConnect
    {
        public static string getConnection()
        {
             //return "data source=localhost:1521/XE;PERSIST SECURITY INFO=True;user id=sarim;password=ourapplication;Connection Timeout=3600;";
             return ConfigurationManager.ConnectionStrings["MSOL"].ConnectionString;

            // return "DATA SOURCE=localhost:1521/XE;PERSIST SECURITY INFO=True;USER ID=sarim;password=ourapplication";
            //return ConfigurationManager.AppSettings["connectionString"].ToString();

        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DataAccess
{
    public class DBComponent
    {
        public static IDBManager IDBMgr()
        {
            IDBManager iDBManager;
            string connectionString = string.Empty;
            {
                connectionString = ConfigurationManager.ConnectionStrings["OraConnection"].ToString();
            }
            iDBManager = new OracleDBManager(connectionString);

            return iDBManager;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public abstract class IDBManager
    {
        /// <summary>
        /// Exectue a direct query on database
        /// </summary>
        /// <param name="Query">Query to be executed.</param>
        /// <param name="Params">Parameters for the Query. NULL if no Parameters required.</param>
        /// <returns></returns>
        public abstract DataSet ExecuteDirectQuery(string Query, GeneralParams[] Params);

        /// <summary>
        /// Executes query for which no datatable is returned. Can be used for Update queries etc.
        /// </summary>
        /// <param name="SPName">Stored Procedure Name</param>
        /// <param name="Params">Parameters expected by the stored procedure SPName</param>
        /// <returns>int, the rows affected by the SP</returns>
        public abstract int ExecuteNonQuery(string SPName, GeneralParams[] Params);
        /// <summary>
        /// Executes a Stored Procedure
        /// </summary>
        /// <param name="SPName">Stored Procedure Name</param>
        /// <param name="Params">Parameters for the Stored Procedure. NULL if no Parameters required.</param>
        /// <returns></returns>
        public abstract DataSet ExecuteSP(string SPName, GeneralParams[] Params);
        public abstract List<GeneralParams> ExecuteSPParamQuery(string SpName, GeneralParams[] Params);

        public class LogLevels
        {
            public static string TraceDetail = "TraceDetail";
            public static string TraceError = "TraceInfo";
        };
    }
}

using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OracleDBManager : IDBManager
    {
        private string ClassName = "DBComponent.OracleDBManager";

        private string ConnectionString;

        public OracleDBManager(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        //Singletone
        public static OracleConnection DbConnection;

        private OracleConnection OpenConnection()
        {

            string sFunctionName = ".OpenConnection()";
            try
            {
                if (DbConnection == null)
                {
                    DbConnection = new OracleConnection(ConnectionString);
                }

                return DbConnection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        override public DataSet ExecuteDirectQuery(string Query, GeneralParams[] Params)
        {
            string sFunctionName = "ExecuteDirectQuery";

            try
            {
                DbConnection = OpenConnection();
                OracleCommand Command = CreateCommand(DbConnection, Query, Params, CommandType.Text);

                OracleDataAdapter Adapter = new OracleDataAdapter(Command);
                DataSet ds = new DataSet();

                Adapter.Fill(ds);
                SetParameters(ref Params, Command);
                CloseConnection(DbConnection);
                return ds;
            }
            catch (Exception ex)
            {
                CloseConnection(DbConnection);
                throw ex;
            }
            finally
            {
                CloseConnection(DbConnection);
            }
        }

        /// <summary>
        /// Executes query for which no datatable is returned. Can be used for Update queries etc.
        /// </summary>
        /// <param name="ConnectionString">Connection String for the database to be connected.</param>
        /// <param name="SPName">Stored Procedure Name</param>
        /// <param name="Params">Parameters expected by the stored procedure SPName</param>
        /// <returns>int, the rows affected by the SP</returns>
        override public int ExecuteNonQuery(string SPName, GeneralParams[] Params)
        {
            string sFunctionName = "ExecuteNonQuery";
            OracleConnection oracleConnection = new OracleConnection(ConnectionString);

            try
            {

                oracleConnection.Open();

                OracleCommand Command = CreateCommand(oracleConnection, SPName, Params, CommandType.StoredProcedure);


                int Rows_Affected = Command.ExecuteNonQuery();
                SetParameters(ref Params, Command);

                oracleConnection.Close();
                oracleConnection.Dispose();

                return Rows_Affected;
            }
            catch (Exception ex)
            {

                oracleConnection.Close();
                oracleConnection.Dispose();

                throw ex;
            }
            finally
            {
                oracleConnection.Close();
                oracleConnection.Dispose();
            }
        }

        override public DataSet ExecuteSP(string SPName, GeneralParams[] Params)
        {
            string sFunctionName = " + ExecuteSP";
            OracleConnection Connection = null;

            try
            {
                Connection = OpenConnection();
                OracleCommand Command = CreateCommand(Connection, SPName, Params, CommandType.StoredProcedure);
                OracleDataAdapter Adapter = new OracleDataAdapter(Command);
                DataSet ds = new DataSet();

                Adapter.Fill(ds);
                SetParameters(ref Params, Command);
                CloseConnection(Connection);
                return ds;
            }
            catch (Exception ex)
            {
                CloseConnection(Connection);
                throw ex;
            }
            finally
            {
                CloseConnection(Connection);
            }
        }

        override public List<GeneralParams> ExecuteSPParamQuery(string SpName, GeneralParams[] Params)
        {
            string sFunctionName = "ExecuteSPParamQuery";
            List<GeneralParams> Paramlist = new List<GeneralParams>();


            try
            {
                DbConnection = OpenConnection();
                OracleCommand Command = new OracleCommand(SpName, DbConnection);
                Command.CommandType = CommandType.StoredProcedure;

                OracleCommandBuilder.DeriveParameters(Command);
                foreach (OracleParameter param in Command.Parameters)
                {
                    Paramlist.Add(new GeneralParams(param.ParameterName, param.Size, GetGenralDBType(param.OracleDbType), param.Value, param.Direction));
                }
                return Paramlist;
            }
            catch (Exception ex)
            {
                CloseConnection(DbConnection);
                throw ex;
            }
            finally
            {
                CloseConnection(DbConnection);
            }
        }

        private void CloseConnection(OracleConnection Connection)
        {
            string sFunctionName = ".CloseConnection()";
            try
            {
                if ((DbConnection != null) && (DbConnection.State != ConnectionState.Closed))
                {
                    DbConnection.Close();
                    DbConnection.Dispose();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private OracleCommand CreateCommand(OracleConnection Conn, string SPName, GeneralParams[] Parameters, CommandType CmdType)
        {
            string sFunctionName = ".CreateCommand()";
            try
            {
                OracleCommand Cmd = new OracleCommand(SPName, Conn);

                Cmd.CommandType = CmdType;

                if (Parameters != null)
                {
                    OracleParameter[] OraParams = GetParameters(Parameters);

                    foreach (OracleParameter Param in OraParams)
                    {
                        Cmd.Parameters.Add(Param);
                    }
                }

                return Cmd;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GeneralParams.GeneralDBTypes GetGenralDBType(OracleDbType Type)
        {
            switch (Type)
            {
                case OracleDbType.Varchar2:
                    return GeneralParams.GeneralDBTypes.VarChar;

                case OracleDbType.Int32:
                    return GeneralParams.GeneralDBTypes.Int;

                case OracleDbType.Decimal:
                    return GeneralParams.GeneralDBTypes.Decimal;

                case OracleDbType.Date:
                    return GeneralParams.GeneralDBTypes.DateTime;

                case OracleDbType.RefCursor:
                    return GeneralParams.GeneralDBTypes.Cursor;

                case OracleDbType.Clob:
                    return GeneralParams.GeneralDBTypes.Text;
                    //case OracleDbType.Double:
                    //    return GeneralParams.GeneralDBTypes.Number;
            }

            return GeneralParams.GeneralDBTypes.VarChar;
        }

        private OracleDbType GetOraDBType(GeneralParams.GeneralDBTypes Type)
        {
            switch (Type)
            {
                case GeneralParams.GeneralDBTypes.VarChar:
                    return OracleDbType.Varchar2;

                case GeneralParams.GeneralDBTypes.Int:
                    return OracleDbType.Int32;

                case GeneralParams.GeneralDBTypes.Decimal:
                    return OracleDbType.Decimal;

                case GeneralParams.GeneralDBTypes.DateTime:
                    return OracleDbType.Date;

                case GeneralParams.GeneralDBTypes.Cursor:
                    return OracleDbType.RefCursor;

                case GeneralParams.GeneralDBTypes.Text:
                    return OracleDbType.Clob;
                    //case GeneralParams.GeneralDBTypes.Number:
                    //    return OracleDbType.Double;
            }

            return OracleDbType.Varchar2;
        }

        private OracleParameter[] GetParameters(GeneralParams[] Params)
        {
            string sFunctionName = ".GetParameters()";
            try
            {
                OracleParameter[] OraParams = new OracleParameter[Params.Length];

                for (int i = 0; i < Params.Length; i++)
                {
                    OraParams[i] = new OracleParameter(Params[i].ParamName, GetOraDBType(Params[i].ParamDBType), Params[i].Size, Params[i].ParamDirection, false, ((System.Byte)(Params[i].Precision)), ((System.Byte)(Params[i].Scale)), "", System.Data.DataRowVersion.Current, Params[i].InputValue);
                }
                return OraParams;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void SetParameters(ref GeneralParams[] Params, OracleCommand Command)
        {
            string sFunctionName = ".SetParameters()";
            try
            {
                if (Params != null)
                {
                    foreach (GeneralParams Param in Params)
                    {
                        if ((Param.ParamDirection == ParameterDirection.Output) || (Param.ParamDirection == ParameterDirection.ReturnValue))
                        {
                            /*
                             * Follwing code is modified to handle "CLOB OUT" data type
                             * CLOB is return as Object if no value is fetch then OracleNullValueException is throw
                             */
                            if (Param.ParamDBType == GeneralParams.GeneralDBTypes.Text)
                            {
                                try
                                {
                                    Param.OutputValue = ((Oracle.DataAccess.Types.OracleClob)Command.Parameters[Param.ParamName].Value).Value.ToString();
                                }
                                catch (Oracle.DataAccess.Types.OracleNullValueException)
                                {
                                    Param.OutputValue = string.Empty;
                                }
                            }
                            else
                            {
                                Param.OutputValue = Command.Parameters[Param.ParamName].Value.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

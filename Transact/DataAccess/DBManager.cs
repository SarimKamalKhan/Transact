using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;


namespace DataAccess
{
    public class DBManager
    {
        private string ClassName = "DBManager";
        private System.Byte MaxPrecision = 38;
        private System.Byte CommonDecimal = 2;

        /*
        public DataSet GetCitiesByCountryCode(string countryCode, out string spResponse)
        {
            string functionName = ".GetCitiesByCode";
            string source = ClassName + functionName;
            spResponse = string.Empty;
            DataSet ds_Responses = new DataSet();
            try
            {

                string SPName = "PKG_CITY.GetByCountryCode";

                GeneralParams[] Params = new GeneralParams[3];

                Params[0] = new GeneralParams("inCode", 100, GeneralParams.GeneralDBTypes.VarChar, countryCode, ParameterDirection.Input);
                Params[1] = new GeneralParams("outCursor", 0, GeneralParams.GeneralDBTypes.Cursor, null, ParameterDirection.Output);
                Params[2] = new GeneralParams("outResponseCode", 3, GeneralParams.GeneralDBTypes.VarChar, null, ParameterDirection.Output);

                ds_Responses = DBComponent.IDBMgr().ExecuteSP(SPName, Params);
                spResponse = Params[2].OutputValue;
            }
            catch (System.Exception ex)
            {
                spResponse = CommonHelper.Contants.ResponseCodes.Failed;
            }

            return ds_Responses;
        }
        */
    

       
      
       
      
        public DataSet UserLogin(LoginDTO loginDTO, out string spResponse)
        {
            string functionName = ".ReserveFlight";
            string source = ClassName + functionName;
            spResponse = string.Empty;
            DataSet ds_Responses = new DataSet();
            try
            {

                //1.Insert customer Reserve Flight details
                //2.If successfully  then update available seats on reserve flight table and commit
                //3.otherwise rollback

                string SPName = "PKG_LOGIN.GetUserLogin";
                

                GeneralParams[] Params = new GeneralParams[4];

                Params[0] = new GeneralParams("inReservationCode", 100, GeneralParams.GeneralDBTypes.VarChar, loginDTO.username, ParameterDirection.Input);

                Params[1] = new GeneralParams("inReservationNumber", 100, GeneralParams.GeneralDBTypes.VarChar, loginDTO.password, ParameterDirection.Input);
                Params[2] = new GeneralParams("outCursor", 0, GeneralParams.GeneralDBTypes.Cursor, null, ParameterDirection.Output);

                Params[3] = new GeneralParams("outResponseCode", 3, GeneralParams.GeneralDBTypes.VarChar, null, ParameterDirection.Output);

                ds_Responses = DBComponent.IDBMgr().ExecuteSP(SPName, Params);
                spResponse = Params[3].OutputValue;
            }
            catch (System.Exception ex)
            {
                spResponse = CommonHelper.Contants.ResponseCodes.Failed;
            }

            return ds_Responses;
        }
    }
}

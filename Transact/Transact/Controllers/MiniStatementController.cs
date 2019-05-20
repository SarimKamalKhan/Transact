using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transact.Models;

namespace Transact.Controllers
{
    public class MiniStatementController : Controller
    {
        Fund_Transfer db = new Fund_Transfer();

        // GET: MiniStatement
        public ActionResult Index()
        {

            try
            {
                if (Session["UserID"] != null)
                {

                    DataSet ds = new DataSet();
                    string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost )(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=sarim;Password=ourapplication;";
                    OracleConnection connection = new OracleConnection(connectionString);

                    //To check if the user is already exist
                    string check_queryString =
        "select * from fund_transfer where account = '" + Int32.Parse(Session["AccNum"].ToString()) + "' order by TRANSACTION_ID desc";
                    OracleCommand MiniStat_command = connection.CreateCommand();
                    MiniStat_command.CommandText = check_queryString;

                    try
                    {
                        connection.Open();
                        //OracleDataReader dr = MiniStat_command.ExecuteReader();



                        using (OracleDataAdapter sda = new OracleDataAdapter(MiniStat_command))
                        {
                            sda.Fill(ds);
                        }

                        connection.Close();
                        connection.Dispose();


                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        ViewBag.Message = ex.Message;
                    }




                    return View(ds);
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }

            }
            catch (Exception e)
            {

                return Content(e.ToString());
            }
        }
    }
}
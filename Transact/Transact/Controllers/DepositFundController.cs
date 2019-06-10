using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transact.Models;

namespace Transact.Controllers
{
    public class DepositFundController : Controller
    {
        // GET: DepositFundController
        public ActionResult Deposit()
        {

            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }



        [HttpPost]
        public ActionResult Deposit(Fund_Transfer fund_transfers)
        {
            try
            {

                if (Session["UserID"] != null)
                {


                    if (ModelState.IsValid)
                    {

                        using (OurDBContext db = new OurDBContext())
                        {

                            string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost )(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=transact;Password=transactpswd;";
                            OracleConnection connection = new OracleConnection(connectionString);


                            //Credit Query
                            string credt_queryString =
       "insert into fund_transfer (FROM_ACCOUNT,TO_ACCOUNT,TRANSACTION_TYPE,TRANSACTION_DATE,ACCOUNT,AMOUNT) " +
       "values('" + Int32.Parse(Session["AccNum"].ToString()) + "','" + Int32.Parse(Session["AccNum"].ToString()) + "','CRDT','" + DateTime.Now.ToString("dd-MMM-yyyy") + "','" + Int32.Parse(Session["AccNum"].ToString()) + "','" + fund_transfers.AMOUNT + "')";
                            OracleCommand Credit_Query = connection.CreateCommand();
                            Credit_Query.CommandText = credt_queryString;


                            int balance = (int)Session["AccBalance"] + fund_transfers.AMOUNT;
                            Session["AccBalance"] = balance;

                            //Update Balance Query
                            string balance_queryString =
                                "UPDATE accounts SET current_balance = " + (int)Session["AccBalance"] + " where username = '" + Session["Username"].ToString() + "'";
                            OracleCommand balance_Query = connection.CreateCommand();
                            balance_Query.CommandText = balance_queryString;

                            try
                            {
                                connection.Open();


                                balance_Query.ExecuteNonQuery();
                                Credit_Query.ExecuteNonQuery();
                                ViewBag.text = "Cash Deposit is Successful";


                                connection.Close();
                                connection.Dispose();


                            }

                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                ViewBag.text = "Cash Deposit Failed: " + ex.Message;
                            }

                            ModelState.Clear();



                        }


                    }
                    return View();
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
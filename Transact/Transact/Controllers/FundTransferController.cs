using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transact.Models;

namespace Transact.Controllers
{
    public class FundTransferController : Controller
    {
        // GET: FundTransfer
        public ActionResult Fund_Transfer()
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
        public ActionResult Fund_Transfer(Fund_Transfer fund_transfers)
        {

            if (Session["UserID"] != null)
            {


                if (ModelState.IsValid)
                {

                    using (OurDBContext db = new OurDBContext())
                    {

                        bool Fund_Sufficient;

                        //Checking if the Current balance is sufficient for Fund Transfer
                        //if (Curr_bal[0] >= Int32.Parse(Session["AccBalance"].ToString()))
                        if (Int32.Parse(Session["AccBalance"].ToString()) >= fund_transfers.AMOUNT)
                        {

                            Fund_Sufficient = true;
                        }
                        else
                        {

                            ViewBag.Check_balance = "You have Insuficient Balance, Please try again with the sufficient amount";
                            Fund_Sufficient = false;
                        }




                        if (Fund_Sufficient)
                        {

                            string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost )(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=sarim;Password=ourapplication;";
                            OracleConnection connection = new OracleConnection(connectionString);


                            //Check to Acc
                            string to_acc =
       "select * from registration where accountnum = '" + fund_transfers.TO_ACCOUNT + "'";
                            OracleCommand To_acc_query = connection.CreateCommand();
                            To_acc_query.CommandText = to_acc;

                            //Debt Query
                            string check_queryString =
       "insert into fund_transfer (FROM_ACCOUNT,TO_ACCOUNT,TRANSACTION_TYPE,TRANSACTION_DATE,ACCOUNT,AMOUNT) " +
       "values('" + Int32.Parse(Session["AccNum"].ToString()) + "','" + fund_transfers.TO_ACCOUNT + "','DEBT','" + DateTime.Now.ToString("dd-MMM-yyyy") + "','" + Int32.Parse(Session["AccNum"].ToString()) + "','" + fund_transfers.AMOUNT + "')";
                            OracleCommand Debit_Query = connection.CreateCommand();
                            Debit_Query.CommandText = check_queryString;

                            

                            //Credit Query
                            string credt_queryString =
       "insert into fund_transfer (FROM_ACCOUNT,TO_ACCOUNT,TRANSACTION_TYPE,TRANSACTION_DATE,ACCOUNT,AMOUNT) " +
       "values('" + Int32.Parse(Session["AccNum"].ToString()) + "','" + fund_transfers.TO_ACCOUNT + "','CRDT','" + DateTime.Now.ToString("dd-MMM-yyyy") + "','" + fund_transfers.TO_ACCOUNT + "','" + fund_transfers.AMOUNT + "')";
                            OracleCommand Credit_Query = connection.CreateCommand();
                            Credit_Query.CommandText = credt_queryString;



                            int balance = (int)Session["AccBalance"] - fund_transfers.AMOUNT;
                            Session["AccBalance"] = balance;


                            //Update Balance Query
                            string balance_queryString =
                                "UPDATE accounts SET current_balance = " + (int)Session["AccBalance"] + " where username = '" + Session["Username"].ToString() +  "'";
                              OracleCommand balance_Query = connection.CreateCommand();
                            balance_Query.CommandText = balance_queryString;


                            //UPDATE accounts SET current_balance = (select current_balance - 10 from accounts where username = 'atta@gmai.com') 

                            List<int> Curr_bal = new List<int> { };

                            try
                            {
                                connection.Open();

                                OracleDataReader dr = To_acc_query.ExecuteReader();
                                bool check_acc = dr.HasRows;



                                if (check_acc)
                                {
                                    Debit_Query.ExecuteNonQuery();
                                    Credit_Query.ExecuteNonQuery();
                                    balance_Query.ExecuteNonQuery();




                                    ViewBag.Check_balance = "Fund Transfer is Successful";
                                }
                                else
                                {
                                    ViewBag.Check_balance = "The Provided TO_ACCOUNT doesnot exist";

                                }



                                //while (chk_bal.Read())
                                //{
                                //    Curr_bal.Add(chk_bal.GetInt32(0));
                                //}



                                connection.Close();
                                connection.Dispose();




                            }

                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                ViewBag.Check_balance = "Fund Transfer Failed: " + ex.Message;
                            }

                            ModelState.Clear();

                        }

                    }

                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }
    }
}
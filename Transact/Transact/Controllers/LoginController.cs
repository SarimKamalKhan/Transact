using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonHelper.Contants;
using DataAccess;
using DataTransferObjects;
using Oracle.ManagedDataAccess.Client;
using Transact.Models;

namespace Transact.Controllers
{
    public class LoginController : Controller
    {
        //Login

        public ActionResult Login()
        {
            if (Session["UserID"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoggedIn");


            }

        }


        [HttpPost]
        public ActionResult Login(Registration registers)
        {
            string response = string.Empty;

            if (Session["UserID"] == null)

            {



                LoginDTO LoginDTORequest = new LoginDTO();
                LoginDTORequest.username = registers.USERNAME;
                LoginDTORequest.password = registers.PASSWORD;


                LoginDTO loginUser = LoginDTORequest;

                DBManager dbMgr = new DBManager();

                DataSet ds = dbMgr.UserLogin(loginUser, out response);



                if (response == ResponseCodes.Success && ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Session["Username"] = registers.USERNAME;
                  
                    Session["Password"] = registers.PASSWORD;


                    // Session["AccBalance"] = 
                    // Session["AccNum"] = 

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        Session["AccBalance"] = dr["BALANCE"];
                        Session["AccNum"] = dr["ACCOUNTNUM"];
                        Session["UserID"] = dr["USERNAME"];
                    }


                    return RedirectToAction("LoggedIn");



                }
                else
                {

                    ModelState.AddModelError("", "Your UserName or Password is invalid");
                    return View();
                }

            }

            else
            {
                #region OldCode
                /*
                // return RedirectToAction("LoggedIn");


                //Database connection string
                string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost )(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=transact;Password=transactpswd;";
                OracleConnection connection = new OracleConnection(connectionString);


                //Query to get User Details
                string queryString = "Select name from registration where username = '" + registers.USERNAME +
                "' and password = '" + registers.PASSWORD + "'";
                OracleCommand command = connection.CreateCommand();
                command.CommandText = queryString;



                //Query to get Account Balance
                string BalanceString = "Select CURRENT_BALANCE from accounts where username = '" + registers.USERNAME +
                "'";
                OracleCommand Balancecommand = connection.CreateCommand();
                Balancecommand.CommandText = BalanceString;



                //Query to get Account Balance
                string AccNumQuery = "Select ACCOUNTNUM from registration where username = '" + registers.USERNAME +
                "' and password = '" + registers.PASSWORD + "'";
                OracleCommand AccNumcommand = connection.CreateCommand();
                AccNumcommand.CommandText = AccNumQuery;

                List<int> accBalance = new List<int> { };
                List<int> accnumber = new List<int> { };


                try
                {
                    connection.Open();

                    OracleDataReader dr = command.ExecuteReader();
                    bool check_user = dr.HasRows;


                    //To read data(Name of User) from object returned data from DB
                    while (dr.Read())
                    {
                        registers.NAME = dr.GetString(0);
                    }


                    OracleDataReader AccBalance = Balancecommand.ExecuteReader();


                    while (AccBalance.Read())
                    {
                        accBalance.Add(AccBalance.GetInt32(0));
                    }


                    OracleDataReader AccNum = AccNumcommand.ExecuteReader();

                    while (AccNum.Read())
                    {
                        accnumber.Add(AccNum.GetInt32(0));
                    }


                    connection.Close();
                    connection.Dispose();

                    if (check_user)
                    {
                        Session["Username"] = registers.USERNAME;
                        Session["UserID"] = registers.NAME;
                        Session["Password"] = registers.PASSWORD;
                        Session["AccBalance"] = accBalance[0];

                        //  Session["AccBalance"] = accBalance[0].ToString();

                        //Session["AccBalanceTest"] = accBalance[0];

                        Session["AccNum"] = accnumber[0].ToString();

                        //ViewBag.Message = AccBalance.ToString();

                        return RedirectToAction("LoggedIn");

                    }
                    else
                    {
                                            ModelState.AddModelError("", "Your UserName or Password is invalid");
                        return View();
                    }
                                    }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ViewBag.Message = "Login Failed: " + ex.Message;
                }


            }


            //else
            //{
            //    return RedirectToAction("LoggedIn");


            //}

            */
                #endregion
                return View();
            }
        

        }


       
       

        public ActionResult LoggedIn()
        {



            if (Session["UserID"] != null)
            {

               
                return View();
            }
            else
            {

                return RedirectToAction("Login");
            }

        }

    }

}
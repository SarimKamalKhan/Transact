using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Net.Mail;
using Transact.Models;

namespace Transact.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            using (OurDBContext db = new OurDBContext())
            {

                return View(db.registration.ToList());
            }
        }

        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Register(Registration registers)

        {
            if (ModelState.IsValid)
            {

                using (OurDBContext db = new OurDBContext())
                {
                    string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost )(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=transact;Password=transactpswd;";
                    OracleConnection connection = new OracleConnection(connectionString);

                    //To check if the user is already exist
                    string check_queryString =
"select * from registration where username = '" + registers.USERNAME + "'";
                    OracleCommand command = connection.CreateCommand();
                    command.CommandText = check_queryString;


                    //To insert new user in DB
                    string queryString =
"insert into registration (USERNAME,NAME,PASSWORD,GENDER,ACCOUNTNUM) values('" + registers.USERNAME + "','" + registers.NAME + "','" + registers.PASSWORD + "','" + registers.GENDER + "','" + registers.ACCOUNTNUM + "')";
                    OracleCommand insertion_command = connection.CreateCommand();
                    insertion_command.CommandText = queryString;



                    //To insert initial balance in user account
                    string query_acc_balance =
    "insert into accounts (USERNAME,REGISTRATION_ID,CURRENT_BALANCE,ACCOUNTNUM) values('" + registers.USERNAME + "',(select ID from REGISTRATION where USERNAME ='" + registers.USERNAME + "'),'10000','"+ registers.ACCOUNTNUM + "')";
                    OracleCommand acc_command = connection.CreateCommand();
                    acc_command.CommandText = query_acc_balance;


                    //To check if email is in correct format
                    bool check_email;
                    try
                    {
                        MailAddress m = new MailAddress(registers.USERNAME);
                        check_email = true;

                    }
                    catch (FormatException a)
                    {
                        Console.WriteLine(ViewBag.Message = "Registration Failed: As provided USERNAME must be in email format ");
                        check_email = false;
                    }



                    if (check_email)
                    {
                        try
                        {
                            connection.Open();
                            OracleDataReader dr = command.ExecuteReader();
                            bool check_user = dr.HasRows;

                            if (!check_user)
                            {
                                insertion_command.ExecuteNonQuery();
                                acc_command.ExecuteReader();
                                connection.Close();
                                connection.Dispose();
                                ViewBag.Message = registers.NAME + " " + ", you are successfully registered with UserID (" + registers.USERNAME + ")..!";
                            }


                            else
                            {
                                connection.Close();
                                connection.Dispose();
                                ViewBag.Message = registers.USERNAME + " " + "is already registered, kindly proceed with the Login";

                            }
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            ViewBag.Message = "Registration Failed: " + ex.Message;
                        }


                    }
                }
                ModelState.Clear();



            }
            
            return View();

        }




    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Transact.Models
{
    public class Accounts
    {
        [Key]
        public string REGISTRATION_ID { get; set; }


        [Required(ErrorMessage = "Username is Required")]
        public string USERNAME { get; set; }


        [Required(ErrorMessage = "Name is Required")]
        public int CURRENT_BALANCE { get; set; }



    }
}
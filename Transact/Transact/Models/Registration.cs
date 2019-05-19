using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Transact.Models
{
    public class Registration
    {
        [Key]
        public string REGISTRATION_ID { get; set; }

     
        [Required(ErrorMessage = "Account is Required")]
        public int ACCOUNTNUM { get; set; }

        [Required(ErrorMessage = "Username is Required")]
        public string USERNAME { get; set; }


        [Required(ErrorMessage ="Name is Required")]
        public string NAME { get; set; }


        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string PASSWORD { get; set; }


        [Required(ErrorMessage = "Gender is Required")]

        public string GENDER { get; set; }

    

      
    }
}
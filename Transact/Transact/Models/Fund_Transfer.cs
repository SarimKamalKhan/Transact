using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Transact.Models
{
    public class Fund_Transfer
    {

         [Key]
        public string TRANSACTION_ID { get; set; }


        [Required(ErrorMessage = "FROM_ACCOUNT is Required")]
        public int FROM_ACCOUNT { get; set; }

        [Required(ErrorMessage = "TO_ACCOUNT is Required")]
        public int TO_ACCOUNT { get; set; }


        [Required(ErrorMessage = "AMOUNT is Required")]
        public int AMOUNT { get; set; }


        public DateTime TRANSACTION_DATE { get; set; }

        public string TRANSACTION_TYPE { get; set; }

        public int ACCOUNT { get; set; }

    }
} 
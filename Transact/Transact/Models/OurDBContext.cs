using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Transact.Models;

namespace Transact.Models
{
    public class OurDBContext : DbContext
    {
       public DbSet<Registration> registration { get; set; }


        public System.Data.Entity.DbSet<Transact.Models.Fund_Transfer> Fund_Transfer { get; set; }
    }
}
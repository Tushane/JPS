using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JPS.Models;

namespace JPS.Data
{
    public class BillContext : DbContext
    {
        public BillContext (DbContextOptions<BillContext> options)
            : base(options)
        {
        }

        public DbSet<BillDatabaseModel> Bill_Information { get; set; }

        public DbSet<PREMISE_DETAILS> PREMISE_DETAILS { get; set; }
    }
}

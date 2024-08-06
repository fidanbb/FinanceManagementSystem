using FinanceManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinanceManagementSystem.Persistence.Context
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=DESKTOP-FV06R42;initial Catalog=FinanceManagementDB; " +
            //"integrated Security=true; TrustServerCertificate=true;");

            optionsBuilder.UseSqlServer("Data Source = SQL8010.site4now.net; Initial Catalog = db_aab54c_fidanfinancemsdb; User Id = db_aab54c_fidanfinancemsdb_admin; Password = Fidan1234_");
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FinancialAccount> FinancialAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}

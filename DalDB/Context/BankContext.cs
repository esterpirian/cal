using Dal.Models;
using System.Linq;
using System.Data.Entity;
namespace Dal.Context
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
   
    public class BankContext :  IdentityDbContext<ApplicationUser>
    {


        public BankContext(DbContextOptions options)
           : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Banks>();
           builder.Entity<Customer>();

            builder.Entity<BankAccounts>().HasKey(i => new { i.UserId, i.BranchCode });
         builder.Entity<BankBranches>();
                base.OnModelCreating(builder);
        }
        public DbSet<Banks> Banks { get; set; }
        public DbSet<BankBranches> BankBranches { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<BankAccounts> BankAccounts { get; set; }

    }
}

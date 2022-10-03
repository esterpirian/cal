namespace MyMicroservice.Context
{
    using Microsoft.EntityFrameworkCore;
    using MyMicroservice.Models;
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Banks> Banks { get; set; }
        public DbSet<BankBranches> BankBranches { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<BankAccounts> BankAccounts { get; set; }
    }
}

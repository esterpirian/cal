using Microsoft.AspNetCore.Identity;
using Dal.Models;
namespace Dal.Context
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Banks Banks { get; set; } // example, not necessary
        public virtual Customer Customer { get; set; } // example, not necessary
        public virtual BankAccounts BankAccounts { get; set; } // example, not necessary
        public virtual BankBranches BankBranches { get; set; } // example, not necessary
    }
}
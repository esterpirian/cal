namespace Dal.Models
{
    using Dal.Context;
    using FluentValidation;
    using FluentValidation.Validators;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;

    [Table("Banks")]
    public class Banks
    {
       

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankCode { get; set; }

        public string? BankName { get; set; }
    }
    [Table("BankBranches")]
    public class BankBranches
    {
        

      

        public int BankCode { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankBranch { get; set; }
        public string? BranchName { get; set; }
        public string? cityName { get; set; }
    }
    [Table("Customer")]
    public class Customer
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? IdNum { get; set; }
        public string? CustomerGuid { get; set; }
        public string? EmailAdd { get; set; }
     
    }
    public class CustomerValidator : AbstractValidator<Customer>
    {
        
       
        public CustomerValidator(BankContext context)
        {
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.FirstName).Length(1, 10).Matches(@"^[א-ת]$");
            RuleFor(x => x.FirstName).Length(1, 10).Matches(@"^[ א-ת]$");
            RuleFor(x => x.IdNum).Length(9,9).Matches(@"^[0-9]$");
            RuleFor(x => x.CustomerGuid).Length(36, 36).Matches(@"^[a-zA-Z-']$");
            RuleFor(x => x.EmailAdd).EmailAddress();
            
        }
    }
    public class BankValidator : AbstractValidator<Banks>
    {


        public BankValidator(BankContext context)
        {
            RuleFor(x => x.BankCode).NotNull();
            RuleFor(x => x.BankName).Length(1, 10).Matches(@"^[א-ת ]$");
           

        }
    }
    public class BranchValidator : AbstractValidator<BankBranches>
    {


        public BranchValidator(BankContext context)
        {
            RuleFor(x => x.BankCode).NotNull();
            RuleFor(x => x.BankBranch).NotNull().ScalePrecision(3, 3);
            RuleFor(x => x.BranchName).NotNull();
            RuleFor(x => x.cityName).NotNull().Length(1, 50).Matches(@"^[א-ת]$");
            


        }
    }
    [Table("BankAccounts")]
    public class BankAccounts
    {
          
              
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BranchCode { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string BankAccountNumber { get; set; }
     

    }
    public class BankAccountsValidator : AbstractValidator<BankAccounts>
    {
        private static BankContext _db;
        public BankAccountsValidator(BankContext context)
        {
            _db=context;
                      

            
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x=>x).NotNull().Must(x=>IsBankAlreadyThere(x.UserId,x.BranchCode)).WithMessage("bank rule alredy exist for this user");
            RuleFor(x => x.BankAccountNumber).MinimumLength(16);//.Matches(@"^[0-9]$");
           

        }
      
        private static bool IsBankAlreadyThere( int branch,int UserId)
        {

            var result1 =
                                            (from BankAccounts in _db.BankAccounts

                                             join BanksBranch in _db.BankBranches on BankAccounts.BranchCode equals BanksBranch.BankBranch into t1
                                             from BanksBranch in t1.DefaultIfEmpty()
                                             join Banks in _db.Banks on BanksBranch.BankCode equals Banks.BankCode into t3
                                             from Banks in t3.DefaultIfEmpty()
                                             where BankAccounts.UserId == UserId
                                             group new { BankAccounts, BanksBranch, Banks } by new { BankAccounts.UserId } into t2
                                             select new
                                             {
                                                 Account=t2.Count(),
                                                 Banks = t2.Where(w=>w.BanksBranch.BankBranch==branch).GroupBy(g => g.BanksBranch.BankCode).Count(),
                                                 count = t2.Where(w => w.BanksBranch.BankBranch == branch).GroupBy(g => g.BanksBranch.BankBranch).Count()
                                             }).ToList();
            if (result1[0].Account >= 3) return false;
            if (result1[0].Banks >= 1) return false;
            if (result1[0].count >= 1) return false;
            
            return true;
        }
    }
    
    public class BankList
    {
        
        public int BankCode { get; set; }
        public int BankBranch { get; set; }
        public string? BranchName { get; set; }
        public string? cityName { get; set; }
        public string? BankName { get; set; }
    }
    public enum CustomerOption
    {
        WithBanksAccount,
        CustomerData,
        CustomerWithCount

    }
    public class CustomerWithCount
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Count { get; set; }
    }
    public class CustomerBankData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  IdNum { get; set; }
        public List<BankData> BankAccounts { get; set; }
    }
    public class BankData
    {
        public string BankName { get; set; }
        public int BankCode { get; set; }
        public string BranchName { get; set; }
        public int BranchCode { get; set; }
        public string AccountNamber { get; set; }
    }
}

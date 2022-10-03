namespace MyMicroservice.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankCode { get; set; }
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
    [Table("BankAccounts")]
    public class BankAccounts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public int BranchCode { get; set; }
        public string? BankAccountNumber { get; set; }
     

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

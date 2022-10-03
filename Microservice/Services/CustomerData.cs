using MyMicroservice.Models;
using MyMicroservice.Context;
using DalDB;
using Dal.Models;

namespace MyMicroservice.Services
{
    public interface ICustomerData
    {
        List<Dal.Models.CustomerWithCount> CustomerWithCount();
        List<Dal.Models.CustomerBankData> getCustomerData(string idNum);
        bool insertCustomer(Dal.Models.Customer customer);
        bool insertBankAccount(Dal.Models.BankAccounts account);
        bool insertBank(Dal.Models.Banks bank);
        bool insertBranch(Dal.Models.BankBranches branch);
    }
    public class CustomerData : ICustomerData
    {
       // private BankContext _bankContext;
        private readonly Dal.Bl.CustomerData customerData;
        public CustomerData( Dal.Bl.CustomerData customerData)//BankContext BankContext,
        {
           // _bankContext = BankContext;
            this.customerData = customerData;
        }
        public List<Dal.Models.CustomerWithCount> CustomerWithCount()
        {
            return this.customerData.CustomerWithCount();
 //           try
 //           {
                
 //               var result = (from Customer in _bankContext.Customer
 //                             join BankAccounts in _bankContext.BankAccounts on Customer.UserId equals BankAccounts.UserId into t1
 //                             from BankAccounts in t1.DefaultIfEmpty()

 //                             join BankBranches in _bankContext.BankBranches on BankAccounts.BranchCode equals BankBranches.BankBranch into t2
 //                             from BankBranches in t2.DefaultIfEmpty()

 //                             group Customer by new { Customer.FirstName, Customer.LastName, BankBranches.BankBranch } into CustomerGrouped
 //                             select new CustomerWithCount
 //                             {
 //                                 FirstName = CustomerGrouped.Key.FirstName,
 //                                 LastName = CustomerGrouped.Key.LastName,
 //                                 Count = CustomerGrouped.Count(x => x.CustomerGuid != null)
 //                             }

 //).ToList();

 //               return result;
 //           }
 //           catch(Exception e)
 //           {

 //           }
 //           return null;
        }
        //public List<CustomerBankData> getCustomerData1(string idNum)
        //{



        //    var result1 =
        //                        (from Customer in _bankContext.Customer
        
        //                        where Customer.IdNum==idNum
        //                         group  Customer by new { Customer.FirstName, Customer.LastName, Customer.UserId,Customer.IdNum } into CustomerGrouped
        //                                              select new CustomerBankData
        //                                              {

        //                                                  FirstName = CustomerGrouped.Key.FirstName,
        //                                                  LastName = CustomerGrouped.Key.LastName,
        //                                                  IdNum=idNum,
        //                                                  BankAccounts = (from BankAccounts in _bankContext.BankAccounts
        //                                                                  join BankBranches in _bankContext.BankBranches on BankAccounts.BranchCode equals BankBranches.BankBranch into t2
        //                                                                  from BankBranches in t2.DefaultIfEmpty()
        //                                                                  join Banks in _bankContext.Banks on BankBranches.BankCode equals Banks.BankCode into t3
        //                                                                  from Banks in t3.DefaultIfEmpty()
        //                                                                  where BankAccounts.UserId == CustomerGrouped.Key.UserId
        //                                                                  select new BankData
        //                                                                  {
        //                                                                      BankName = Banks.BankName,
        //                                                                      BankCode = Banks.BankCode,
        //                                                                      BranchCode = BankBranches.BankBranch,
        //                                                                      BranchName = BankBranches.BranchName,
        //                                                                      AccountNamber = BankAccounts.BankAccountNumber
        //                                                                  }).ToList()
       



        //                                              }

        //                                                                        ).ToList();
        //    return result1;
        //}
        public List<Dal.Models.CustomerBankData> getCustomerData(string idNum)
        {



            //var result1 =
            //                    (from Customer in _bankContext.Customer
            //                     join BankAccounts in _bankContext.BankAccounts on Customer.UserId equals BankAccounts.UserId into t1
            //                     from BankAccounts in t1.DefaultIfEmpty()

            //                     join BankBranches in _bankContext.BankBranches on BankAccounts.BranchCode equals BankBranches.BankBranch into t2
            //                     from BankBranches in t2.DefaultIfEmpty()

            //                     join Banks in _bankContext.Banks on BankBranches.BankCode equals Banks.BankCode into t3
            //                     from Banks in t3.DefaultIfEmpty()
            //                     where Customer.IdNum == idNum
            //                     group new { Customer, BankAccounts, BankBranches, Banks } by new { Customer.FirstName, Customer.LastName,Customer.IdNum,Customer.UserId } into CustomerGrouped
            //                     select new CustomerBankData
            //                     {

            //                         FirstName = CustomerGrouped.Key.FirstName,
            //                         LastName = CustomerGrouped.Key.LastName,
            //                         IdNum = CustomerGrouped.Key.IdNum,
            //                         BankAccounts = CustomerGrouped.Select(x => new BankData { BankCode= x.Banks.BankCode,BankName= x.Banks.BankName, BranchCode=x.BankBranches.BankBranch,BranchName= x.BankBranches.BranchName,AccountNamber= x.BankAccounts.BankAccountNumber }).ToList()
            //                         }).ToList();


            //return result1;
            return this.customerData.getCustomerData(idNum);
        }
        public bool insertCustomer(Dal.Models.Customer customer)
        {
            try
            {
                
                this.customerData.insertCustomer(customer);
                   return true;
                
            }
            catch (Exception e)
            {

            }
           
            return false;
        }
        public bool insertBankAccount(Dal.Models.BankAccounts account)
        {
            try { 
            return this.customerData.insertBankAccount(account);
            }
            catch (Exception e)
            {

            }

            return false;
        }
        public bool insertBank(Dal.Models.Banks bank)
        {
            try
            {

            
            return this.customerData.insertBank(bank);
            }
            catch (Exception e)
            {

            }

            return false;
        }
        public bool insertBranch(Dal.Models.BankBranches branch)
        {
            try { 
            return this.customerData.insertBranch(branch);
            }
            catch (Exception e)
            {

            }

            return false;
        }
    }
}

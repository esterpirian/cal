using Dal.Models;
using Dal.Context;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Graph;
using FluentValidation;

namespace Dal.Bl
{
    
    public class CustomerData 
    {
        
        private  readonly BankContext _bankContext;
        private IValidator<Customer> _validator;
        IValidator<BankAccounts> _bankValidator;
        IValidator<Banks> _banksValidator;
        IValidator<BankBranches> _branchValidator;
        public CustomerData(BankContext context, IValidator<Customer> validator, IValidator<BankAccounts> bankValidator, IValidator<Banks> banksValidator, IValidator<BankBranches> branchValidator)
        {
            _bankContext = context;
            _validator = validator; 
            _bankValidator = bankValidator;
            _banksValidator = banksValidator;
            _branchValidator=branchValidator;
        }
       
        public  List<Dal.Models.CustomerWithCount> CustomerWithCount()
        {
            try
            {
              
                    var result = (from Customer in _bankContext.Customer
                                  join BankAccounts in _bankContext.BankAccounts on Customer.UserId equals BankAccounts.UserId into t1
                                  from BankAccounts in t1.DefaultIfEmpty()

                                  join BankBranches in _bankContext.BankBranches on BankAccounts.BranchCode equals BankBranches.BankBranch into t2
                                  from BankBranches in t2.DefaultIfEmpty()

                                  group Customer by new { Customer.FirstName, Customer.LastName, BankBranches.BankBranch } into CustomerGrouped
                                  select new CustomerWithCount
                                  {
                                      FirstName = CustomerGrouped.Key.FirstName,
                                      LastName = CustomerGrouped.Key.LastName,
                                      Count = CustomerGrouped.Count(x => x.CustomerGuid != null)
                                  }

).ToList();

                    return result;
               
               
            }
            catch(Exception e)
            {

            }
            return null;
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
        public  List<CustomerBankData> getCustomerData(string idNum)
        {

            
            var result1 =
                                (from Customer in _bankContext.Customer
                                 join BankAccounts in _bankContext.BankAccounts on Customer.UserId equals BankAccounts.UserId into t1
                                 from BankAccounts in t1.DefaultIfEmpty()

                                 join BankBranches in _bankContext.BankBranches on BankAccounts.BranchCode equals BankBranches.BankBranch into t2
                                 from BankBranches in t2.DefaultIfEmpty()

                                 join Banks in _bankContext.Banks on BankBranches.BankCode equals Banks.BankCode into t3
                                 from Banks in t3.DefaultIfEmpty()
                                 where Customer.IdNum == idNum
                                 group new { Customer, BankAccounts, BankBranches, Banks } by new { Customer.FirstName, Customer.LastName, Customer.IdNum, Customer.UserId} into CustomerGrouped
                                 select new CustomerBankData
                                 {

                                     FirstName = CustomerGrouped.Key.FirstName,
                                     LastName = CustomerGrouped.Key.LastName,
                                     IdNum = CustomerGrouped.Key.IdNum,
                                     BankAccounts = CustomerGrouped.Select(x => new BankData { BankCode = x.Banks.BankCode, BankName = x.Banks.BankName, BranchCode = x.BankBranches.BankBranch, BranchName = x.BankBranches.BranchName, AccountNamber = x.BankAccounts.BankAccountNumber }).ToList()
                                 }).ToList();


                return result1;
            
        }
        public  bool insertCustomer(Customer customer)
        {
            var result =  _validator.Validate(customer);
            var result1 = _bankValidator.Validate(new BankAccounts { UserId=1,BranchCode=112,BankAccountNumber="123"});
            try
            {
                    if (result.IsValid==true)
                    {
                       


                        _bankContext.Customer.Add(customer);
                        _bankContext.SaveChanges();
                        return true;
                    }
                }
                catch (Exception e)
                {

                }
                       
            return false;
        }
        public bool insertBankAccount(BankAccounts account)
        {
            var result = _bankValidator.Validate(account);
            try
            {
                if (result.IsValid == true)
                {
                   
                    _bankContext.BankAccounts.Add(account);
                    _bankContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {

            }

            return false;
        }
        public bool insertBank(Banks bank)
        {
            var result = _banksValidator.Validate(bank);
            try
            {
                if (result.IsValid == true)
                {



                    _bankContext.Banks.Add(bank);
                    _bankContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {

            }

            return false;
        }
        public bool insertBranch(BankBranches branch)
        {
            var result = _branchValidator.Validate(branch);
            try
            {
                if (result.IsValid == true)
                {



                    _bankContext.BankBranches.Add(branch);
                    _bankContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {

            }

            return false;
        }
    }
}

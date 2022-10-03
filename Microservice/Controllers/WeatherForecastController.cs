using Microsoft.AspNetCore.Mvc;
using MyMicroservice.Context;
using MyMicroservice.Models;
using MyMicroservice.Services;
namespace MyMicroservice.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
  
    private readonly ICustomerData _customerData;


    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,  ICustomerData customerData)//,
    {
        _logger = logger;
      
        _customerData = customerData;
    }

    [HttpGet]
    //[Produces(typeof(List<CustomerWithCount>))]
    
    [Route("GetCustomer/{id}/{type}")]
   public ActionResult GetCustomer( string id, CustomerOption type)
      //  public List<CustomerWithCount> GetCustomer(string id, CustomerOption type)
    {
        
        switch (type)
        {
            case CustomerOption.WithBanksAccount:
                // code block
                return new ObjectResult(_customerData.getCustomerData(id));
               
            case CustomerOption.CustomerWithCount:
                // code block
                return new ObjectResult(_customerData.CustomerWithCount());
                
            default:
                // code block
                return new ObjectResult(_customerData.CustomerWithCount());
               // break;
        }
       
       

    }
    [HttpPost]
    [Route("AddCustomer")]
    public bool AddCustomer([FromBody]Dal.Models.Customer customer)
    {
        return _customerData.insertCustomer(customer);
    }
    [HttpPost]
    [Route("AddCustomer")]
    public bool AddBankAccount([FromBody] Dal.Models.BankAccounts account)
    {
        return _customerData.insertBankAccount(account);
    }
    [HttpPost]
    [Route("AddCustomer")]
    public bool AddBank([FromBody] Dal.Models.Banks bank)
    {
        return _customerData.insertBank(bank);
    }
    [HttpPost]
    [Route("AddCustomer")]
    public bool AddBranch([FromBody] Dal.Models.BankBranches branch)
    {
        return _customerData.insertBranch(branch);
    }
}

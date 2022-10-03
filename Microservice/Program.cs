using FluentAssertions.Common;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyMicroservice.Context;
using MyMicroservice.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Dal.Context.BankContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BnksConStr")));
builder.Services.AddIdentity<Dal.Context.ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<Dal.Context.BankContext>();
builder.Services.AddScoped<IValidator<Dal.Models.Customer>, Dal.Models.CustomerValidator>();
builder.Services.AddScoped<IValidator<Dal.Models.BankAccounts>, Dal.Models.BankAccountsValidator>();
builder.Services.AddScoped<IValidator<Dal.Models.BankBranches>, Dal.Models.BranchValidator>();
builder.Services.AddScoped<IValidator<Dal.Models.Banks>, Dal.Models.BankValidator>();

builder.Services.AddScoped<Dal.Bl.CustomerData>();
builder.Services.AddScoped<ICustomerData, CustomerData>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=WeatherForecast}/{GetCustomer}/{id}");
app.MapControllerRoute(name: "WeatherForecast",
                pattern: "WeatherForecast/{*GetCustomer}",
                defaults: new { controller = "WeatherForecast", action = "GetCustomer" });

app.Run();

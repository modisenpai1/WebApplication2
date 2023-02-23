using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;
using WebApplication2.Data;
using WebApplication2.Domain.Models;
using WebApplication2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(//"server=PHOTOSHOP-PC\\SQLEXPRESS;initial catalog=webapp2;user id=test;password=test123;trustservercertificate=true;"
                                     //"server=desktop-itl4gju\\sqlexpress;initial catalog=eventapi;user id=myapi;password=thispassword;trustservercertificate=true;"
                                     //for modi
                 "Server=localhost;Database=EventsAppDb;User Id=sa;Password=admin;Trusted_Connection=true;Encrypt=False;"
                    ,
                    sqlServerOptions => sqlServerOptions.CommandTimeout(420)), ServiceLifetime.Transient);
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddControllers().AddNewtonsoftJson(s =>
{
    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});
builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IAppRepo<>), typeof(AppRepo<>));
builder.Services.AddScoped<ICountryServices, CountryServices>();
builder.Services.AddScoped<ICityServices, CityServices>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
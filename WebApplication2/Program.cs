using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.Text;
using WebApplication2.Data;
using WebApplication2.Domain.Models;
using WebApplication2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    //T-Work
                    //"server=PHOTOSHOP-PC\\SQLEXPRESS;initial catalog=webapp2;user id=test;password=test123;trustservercertificate=true;"
                    //T-Home
                    "Server=DESKTOP-ITL4GJU\\SQLEXPRESS;Initial Catalog=newApiDB;User ID=admin;Password=admin;trustservercertificate=true;"
                    //modi
                    //"Server=localhost;Database=EventsAppDb;User Id=sa;Password=admin;Trusted_Connection=true;Encrypt=False;"
                    ,
                    sqlServerOptions => sqlServerOptions.CommandTimeout(420)), ServiceLifetime.Transient);


builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddControllers().AddNewtonsoftJson(s =>
{
    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
#pragma warning disable CS8604 // Possible null reference argument.
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("Jwt").GetSection("Issuer").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration.GetSection("Jwt")
            .GetSection("Key").Value))
    };
#pragma warning restore CS8604 // Possible null reference argument.
});



builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IAppRepo<>), typeof(AppRepo<>));
builder.Services.AddScoped<ICountryServices, CountryServices>();
builder.Services.AddScoped<ICityServices, CityServices>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserOrgService, UserOrgService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IOrgenizationServices,OrgenizationServices>();
builder.Services.AddScoped<IOrgenizationServices,OrgenizationServices>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Jwt Authorizatoin header user the Bearer scheme.
        Enter 'Bearer' [space] and then your token in the text input below.
        Example: 'Bearer 12345abcdef'",
        Name = "Authorizatoin",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
                Scheme="0auth2",
                Name="Bearer",
                In=ParameterLocation.Header
            },
            new List<string>()
        }
     
    });
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
   
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();

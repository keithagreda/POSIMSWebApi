using DataAccess.EFCore;
using DataAccess.EFCore.UnitOfWorks;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using POSIMSWebApi;
using POSIMSWebApi.Interceptors;
using POSIMSWebApi.Application.Services;
using POSIMSWebApi.Application.Interfaces;
using Serilog;
using System;
using POSIMSWebApi.Middleware;
using POSIMSWebApi.Infrastructure;
using Microsoft.AspNetCore.Builder;
using POSIMSWebApi.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using POSIMSWebApi.Authentication.Interface;
using POSIMSWebApi.Authentication.Services;
using System.Security.Claims;
using POSIMSWebApi.Authentication.AuthorizationHelper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "POSIMSWebAPI";
    config.Description = "API documentation for POSIMSWebAPI using NSwag.";
    config.Version = "v1";

});

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddDbContext<AuthContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddDbContext<SerilogContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("SqlLite")));

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStockDetailService, StocksDetailService>();
builder.Services.AddScoped<IUserAuthServices, UserAuthServices>();
builder.Services.AddScoped<IStockReceivingService, StocksReceivingService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IStorageLocationService, StorageLocationService>();
builder.Services.AddScoped<ISalesService, SalesService>();
builder.Services.AddScoped<SoftDeleteInterceptor>();
builder.Services.AddScoped<AuditInterceptor>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().WithMethods("GET", "POST", "PUT", "DELETE"));
});

builder.Services.AddIdentity<ApplicationIdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
    options.SignIn.RequireConfirmedAccount = true;
})
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider("userIdentity", typeof(DataProtectorTokenProvider<ApplicationIdentityUser>));

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(o =>
{
    o.SaveToken = false;
    o.RequireHttpsMetadata = false;
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]!))
    };
});

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy(UserRole.Admin, policy =>
//        policy.RequireAssertion(context =>
//            AuthorizationHelper.HasPermission(context.User, UserRole.Admin)));
//    options.AddPolicy(UserRole.Inventory, policy =>
//        policy.RequireAssertion(context =>
//            AuthorizationHelper.HasPermission(context.User, UserRole.Inventory)));
//    options.AddPolicy(UserRole.Cashier, policy =>
//        policy.RequireClaim("Permission", UserRole.Cashier));
//    options.AddPolicy(UserRole.Admin, policy =>
//    policy.RequireRole("Admin", ""));
//});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    ////app.UseSwaggerUI();
    //app.UseDeveloperExceptionPage();
    //app.UseForwardedHeaders();
    app.UseOpenApi();    // Serve OpenAPI/Swagger documents
    app.UseSwaggerUi(); // Serve Swagger UI
    app.UseReDoc();      // Optional: Serve ReDoc UI
    //app.UseReDoc();      // Optional: Serve ReDoc UI

}


app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();


app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseExceptionHandler();

app.MapControllers();

app.Run();

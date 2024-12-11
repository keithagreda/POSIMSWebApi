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
    config.Title = "BrigadaCanteen API";
    config.Description = "API documentation for BrigadaCanteen using NSwag.";
    config.Version = "v1";

});

builder.Services.AddDbContext<ApplicationContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddDbContext<SerilogContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SqlLite")));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStockDetailService, StocksDetailService>();
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
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseExceptionHandler();

app.MapControllers();

app.Run();

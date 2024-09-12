using Infrastructure;
using Infrastructure.Reports;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using WebApp.Services;

using NoSqlRepos = Infrastructure.Repositories.NoSQL;
using MySqlRepos = Infrastructure.Repositories.MySQL;

using NoSqlReports = Infrastructure.Reports.NoSQL;
using MySqlReports = Infrastructure.Reports.MySQL;

using NoSqlServices = WebApp.Services.NoSQL;
using MySqlServices = WebApp.Services.MySQL;
using Infrastructure.Repositories.NoSQL;
using MongoDB.Driver.Core.Configuration;

var builder = WebApplication.CreateBuilder(args);

var dbConnectionString = Environment.GetEnvironmentVariable("MySqlConnectionString");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(dbConnectionString, new MySqlServerVersion(ServerVersion.AutoDetect(dbConnectionString))));

//source: MongoDB Documentation for .NET
using var loggerFactory = LoggerFactory.Create(b =>
{
    b.AddSimpleConsole();
    b.SetMinimumLevel(LogLevel.Debug);
});
var settings = MongoClientSettings.FromConnectionString(Environment.GetEnvironmentVariable("MongoDbConnectionString"));
settings.LoggingSettings = new LoggingSettings(loggerFactory);

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    return new MongoClient(settings);
});
//


//builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
//{
//    return new MongoClient(Environment.GetEnvironmentVariable("MongoDbConnectionString"));
//});

builder.Services.AddSingleton(serviceProvider =>
{
    var client = serviceProvider.GetRequiredService<IMongoClient>();
    return client.GetDatabase(Environment.GetEnvironmentVariable("DbName"));
});

builder.Services.AddScoped<MySqlRepos.BookingRepository>();
builder.Services.AddScoped<NoSqlRepos.BookingRepository>();

builder.Services.AddScoped<MySqlRepos.TourRepository>();
builder.Services.AddScoped<NoSqlRepos.TourRepository>();

builder.Services.AddScoped<MySqlRepos.UserRepository>();
builder.Services.AddScoped<NoSqlRepos.UserRepository>();

builder.Services.AddScoped<MySqlReports.ReportGenerator>();
builder.Services.AddScoped<NoSqlReports.ReportGenerator>();

builder.Services.AddScoped<NoSqlRepos.TouristAttractionRepository>();

builder.Services.AddScoped<MySqlToMongoDbMigration>();

builder.Services.AddScoped<DatabaseService>();

builder.Services.AddScoped<MySqlServices.BookingService>();
builder.Services.AddScoped<NoSqlServices.BookingService>();

builder.Services.AddScoped<MySqlServices.TourService>();
builder.Services.AddScoped<NoSqlServices.TourService>();

builder.Services.AddScoped<MySqlServices.UserService>();
builder.Services.AddScoped<NoSqlServices.UserService>();

builder.Services.AddScoped<NoSqlReports.ReportGenerator>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


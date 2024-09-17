using System.Net.Mime;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Task1.Backend.Interfaces;
using Task1.Backend.Repositories;
using Task1.Data.Models;
using Microsoft.AspNetCore.Identity;
using Task1.Data;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load(); 
var connectionString = DotNetEnv.Env.GetString("MYSQL_CONNECTION_STRING");
builder.Configuration["ConnectionStrings:Default"] = connectionString;


builder.Services.AddDbContext<AccountContext>(options =>
	options.UseMySql(builder.Configuration.GetConnectionString("Default"), ServerVersion.AutoDetect(connectionString)));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseMySql(builder.Configuration.GetConnectionString("Default"), ServerVersion.AutoDetect(connectionString)));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AccountContext>();

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();

builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



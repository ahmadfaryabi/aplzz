using Ahmadside.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<UserDbContext>(options => {
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:UserDbContextConnection"]);
});

builder.Services.AddSession(options => {
    options.Cookie.Name = ".Applz.Session";
    options.Cookie.IsEssential = true;
});

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Login/Index";
});

// logger
var loggerConf = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File($"Logs/app_{DateTime.Now:yyyyMMdd_HHmmss}.log");

var logger = loggerConf.CreateLogger();
builder.Logging.AddSerilog(logger);

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
     app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

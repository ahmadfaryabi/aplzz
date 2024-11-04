using Microsoft.EntityFrameworkCore;
using Aplzz.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DbContexts>(options => {
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:DatabaseConnection"]);
});

builder.Services.AddSession(options => {
    options.Cookie.Name = ".Applz.Session";
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseAuthorization();
app.UseAuthentication();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Post}/{action=Index}/{id?}");

app.Run();

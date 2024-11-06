using Microsoft.EntityFrameworkCore;
using Aplzz.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

<<<<<<< HEAD
// Add DbContext for Posts
builder.Services.AddDbContext<PostDbContext>(options => {
    options.UseSqlite(builder.Configuration["ConnectionStrings:PostDbContextConnection"]);
});

// Add DbContext for Account Profiles
builder.Services.AddDbContext<AccountDbContext>(options => {
    options.UseSqlite(builder.Configuration["ConnectionStrings:AccountDbContextConnection"]);
=======
builder.Services.AddDbContext<DbContexts>(options => {
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:DatabaseConnection"]);
});

builder.Services.AddSession(options => {
    options.Cookie.Name = ".Applz.Session";
    options.Cookie.IsEssential = true;
>>>>>>> d99f336fee5261038d837b2c088847523dcbe697
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Add error handling for production
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAuthorization();
app.UseAuthentication();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Post}/{action=Index}/{id?}");

app.Run();
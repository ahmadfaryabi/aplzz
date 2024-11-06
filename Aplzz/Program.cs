using Microsoft.EntityFrameworkCore;
using Aplzz.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext for Posts
builder.Services.AddDbContext<PostDbContext>(options => {
    options.UseSqlite(builder.Configuration["ConnectionStrings:PostDbContextConnection"]);
});

// Add DbContext for Account Profiles
builder.Services.AddDbContext<AccountDbContext>(options => {
    options.UseSqlite(builder.Configuration["ConnectionStrings:AccountDbContextConnection"]);
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Post}/{action=Index}/{id?}");

app.Run();
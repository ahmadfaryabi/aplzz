using Microsoft.EntityFrameworkCore;
using Aplzz.DAL;
using Aplzz.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IPostRepository, PostRepository>();



builder.Services.AddSession(options => {
    options.Cookie.Name = ".Applz.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// Add DbContext for Posts

builder.Services.AddDbContext<PostDbContext>(options => {
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:DatabaseConnection"]);
});

// Add DbContext for Account Profiles
builder.Services.AddDbContext<AccountDbContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:AccountDbContextConnection"]);
});

// Add Distributed Memory Cache for session support
builder.Services.AddDistributedMemoryCache();



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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Add session middleware to the request pipeline
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Post}/{action=Index}/{id?}");

app.Run();

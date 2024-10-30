using Microsoft.EntityFrameworkCore;
using Aplzz.DAL;
using Aplzz.Models;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<PostDbContext>(options => {
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:DatabaseConnection"]);
});

<<<<<<< HEAD
builder.Services.AddScoped<IPostRepository, PostRepository>();

builder.Services.AddSession(options => {
    options.Cookie.Name = ".Applz.Session";
// Add DbContext for Posts
});

// Add DbContext for Account Profiles
builder.Services.AddDbContext<AccountDbContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:AccountDbContextConnection"]);
});

// Add Distributed Memory Cache for session support
builder.Services.AddDistributedMemoryCache();

// Add session services and configure session options
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

=======
var loggerConfiguration = new LoggerConfiguration()
    .MinimumLevel.Information() // levels: Trace< Information < Warning < Erorr < Fatal
    .WriteTo.File($"Logs/app_{DateTime.Now:yyyyMMdd_HHmmss}.log");

loggerConfiguration.Filter.ByExcluding(e => e.Properties.TryGetValue("SourceContext", out var value) &&
                            e.Level == LogEventLevel.Information &&
                            e.MessageTemplate.Text.Contains("Executed DbCommand"));
>>>>>>> c954901 (Errohandling og logging)

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

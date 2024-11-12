using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
using Aplzz.DAL;
=======
<<<<<<< HEAD
>>>>>>> 5b23c9a (Lagt til DAL, Fikset Like og Kommentar funksjon)
=======
<<<<<<< HEAD
>>>>>>> c63ba55 (endring)
=======
<<<<<<< HEAD
>>>>>>> ab71774 (fikset sql lite feil. :))
using Aplzz.Models;
=======
using Aplzz.DAL;
<<<<<<< HEAD
>>>>>>> d6afb7a (Lagt til DAL, Fikset Like og Kommentar funksjon)
using Serilog;
using Serilog.Events;
=======
using Aplzz.Models;
>>>>>>> d99f336 (endring)
=======
using Aplzz.DAL;
>>>>>>> c4e2647 (fikset sql lite feil. :))
=======
using Aplzz.Models;
>>>>>>> f6cecd9 (fikse feil)

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD

builder.Services.AddDbContext<PostDbContext>(options => {
=======
builder.Services.AddDbContext<DbContexts>(options => {
>>>>>>> 86d362f (login system endring)
=======
builder.Services.AddDbContext<PostDbContext>(options => {
>>>>>>> ab71774 (fikset sql lite feil. :))
=======
=======
>>>>>>> 1628de3 (fikse feil)
// Add DbContext for Posts
<<<<<<< HEAD
<<<<<<< HEAD
=======

>>>>>>> f6cecd9 (fikse feil)
builder.Services.AddDbContext<PostDbContext>(options => {
<<<<<<< HEAD
>>>>>>> 6322eac (ok)
    options.UseSqlite(
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        builder.Configuration["ConnectionStrings:DatabaseConnection"]);
=======
        builder.Configuration["ConnectionStrings:ItemDbContextConnection"]);
        builder.Configuration["ConnectionStrings:UserDbContextConnection"]);
>>>>>>> 5847ef8 (logg inn funksjoon)
=======
        builder.Configuration["ConnectionStrings:DatabaseConnection"]);
>>>>>>> 5504f1b (database endringer)
=======
=======
>>>>>>> c63ba55 (endring)
        builder.Configuration["ConnectionStrings:DatabaseConnection"]);
<<<<<<< HEAD
=======
        builder.Configuration["ConnectionStrings:PostDbContextConnection"]);
>>>>>>> 051aac6 (.)
<<<<<<< HEAD
>>>>>>> 17bd246 (.)
=======
=======
=======
builder.Services.AddDbContext<DbContexts>(options => {
>>>>>>> 5d12ca7 (accountprofile)
=======
=======
});

builder.Services.AddScoped<IPostRepository, PostRepository>();

builder.Services.AddSession(options => {
    options.Cookie.Name = ".Applz.Session";
// Add DbContext for Posts
<<<<<<< HEAD
>>>>>>> f6cecd9 (fikse feil)
builder.Services.AddDbContext<DbContexts>(options =>
{
>>>>>>> 4fa072a (account)
    options.UseSqlite(builder.Configuration["ConnectionStrings:PostDbContextConnection"]);
=======
>>>>>>> 834f36d (feil fiksing)
});

// Add DbContext for Account Profiles
builder.Services.AddDbContext<AccountDbContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:AccountDbContextConnection"]);
<<<<<<< HEAD
>>>>>>> 7071121 (ok)
>>>>>>> 6322eac (ok)
});

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
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
=======
>>>>>>> 86d362f (login system endring)
=======
>>>>>>> 7ae0213 (La til test user for å teste like funksjonen)
=======
>>>>>>> a3b8ecd (account)
var loggerConfiguration = new LoggerConfiguration()
    .MinimumLevel.Information() // levels: Trace< Information < Warning < Erorr < Fatal
    .WriteTo.File($"Logs/app_{DateTime.Now:yyyyMMdd_HHmmss}.log");

loggerConfiguration.Filter.ByExcluding(e => e.Properties.TryGetValue("SourceContext", out var value) &&
                            e.Level == LogEventLevel.Information &&
                            e.MessageTemplate.Text.Contains("Executed DbCommand"));
<<<<<<< HEAD
>>>>>>> c954901 (Errohandling og logging)
=======
=======
=======
        builder.Configuration["ConnectionStrings:DatabaseConnection"]);
});

<<<<<<< HEAD
>>>>>>> d99f336 (endring)
=======
builder.Services.AddScoped<IPostRepository, PostRepository>();

>>>>>>> c4e2647 (fikset sql lite feil. :))
builder.Services.AddSession(options => {
    options.Cookie.Name = ".Applz.Session";
    options.Cookie.IsEssential = true;
=======
>>>>>>> 5d12ca7 (accountprofile)
});
<<<<<<< HEAD
>>>>>>> f4ab8f9 (login system endring)
<<<<<<< HEAD
>>>>>>> 86d362f (login system endring)
=======
=======
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
>>>>>>> ff3fccc (La til test user for å teste like funksjonen)
>>>>>>> 7ae0213 (La til test user for å teste like funksjonen)

var loggerConfiguration = new LoggerConfiguration()
    .MinimumLevel.Information() // levels: Trace< Information < Warning < Erorr < Fatal
    .WriteTo.File($"Logs/app_{DateTime.Now:yyyyMMdd_HHmmss}.log");

loggerConfiguration.Filter.ByExcluding(e => e.Properties.TryGetValue("SourceContext", out var value) &&
    e.Level == LogEventLevel.Information &&
    e.MessageTemplate.Text.Contains("Executed DbCommand"));

builder.Services.AddScoped<IPostRepository, PostRepository>();
=======
>>>>>>> d99f336 (endring)
=======
// Add Distributed Memory Cache for session support
builder.Services.AddDistributedMemoryCache();

// Add session services and configure session options
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

>>>>>>> 4fa072a (account)

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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Add session middleware to the request pipeline
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Post}/{action=Index}/{id?}");

app.Run();

using Microsoft.EntityFrameworkCore;
using Aplzz.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PostDbContext>(options => {
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:DatabaseConnection"]);
});

builder.Services.AddScoped<IPostRepository, PostRepository>();

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

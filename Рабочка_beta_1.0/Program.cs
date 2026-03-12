using Microsoft.EntityFrameworkCore;
using Рабочка_beta_1._0.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
// Добавляем сервисы до сборки
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath= "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/Login";
    });
builder.Services.AddAuthorization();
// подключение к бд  MySQL
var connetionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<IjobsContext>(options =>
    options.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString))
);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

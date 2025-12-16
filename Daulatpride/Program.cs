using Daulatpride.Extension;
using Daulatpride.Controllers;
using Daulatpride.Domain.Interface;
using Daulatpride.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// -------------------- SERVICES --------------------

// MVC
builder.Services.AddControllersWithViews();

// Application / DI services
builder.Services.AddApplicationServices();

// HttpContext Accessor (only once)
builder.Services.AddHttpContextAccessor();

// Session cache
builder.Services.AddDistributedMemoryCache();

// Session configuration
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // 60 minutes
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// -------------------- BUILD APP --------------------
var app = builder.Build();

// -------------------- MIDDLEWARE --------------------

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ❗ Session must be BEFORE Authorization
app.UseSession();

app.UseAuthorization();

// -------------------- ROUTES --------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"
);

app.Run();

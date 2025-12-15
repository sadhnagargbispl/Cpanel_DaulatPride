using Daulatpride.Extension;
using Daulatpride.Controllers;
using Daulatpride.Domain.Interface;
using Daulatpride.Infrastructure.Repository;
//using Daulatpride.Extension;
//using Daulatpride.Fillters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
//builder.Services.AddScoped<I_Login, LoginRepository>();
//builder.Services.AddScoped<SessionCheckFilter>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // 60 minutes timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");
app.Run();

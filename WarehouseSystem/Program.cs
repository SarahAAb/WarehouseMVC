using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WarehouseSystem.Data;
using WarehouseSystem.Models;
using WarehouseSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IWarehouseServices, WarehouseServices>();
builder.Services.AddScoped<ICityServices, CityServices>();
builder.Services.AddScoped<ICountryServices, CountryServices>();
builder.Services.AddScoped<IItemServices, ItemServices>();
builder.Services.AddScoped<IAccountServices,AccountServices>();
builder.Services.AddDbContext<WarehouseContext>();
builder.Services.AddIdentity<ApplicationUsers, IdentityRole>().AddEntityFrameworkStores<WarehouseContext>();
builder.Services.Configure<IdentityOptions>(Options =>
{
    Options.Password.RequiredLength = 5;
    Options.Password.RequireNonAlphanumeric = false;
    Options.Password.RequireUppercase = false;
    Options.Password.RequireLowercase = false;
});
builder.Services.ConfigureApplicationCookie(config =>
{
    config.ExpireTimeSpan = TimeSpan.FromDays(2);
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();

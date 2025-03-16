using BusinessReportingMVC.Data;
using BusinessReportingMVC.Repositories;
using BusinessReportingMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBusinessReportingRepository, BusinessReportingRepository>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAdminService, AdminService>();

//builder.Services.AddScoped<IAuthorizationHandler, ReportOwnerHandler>();

builder.Services.AddControllersWithViews()
                                .AddSessionStateTempDataProvider();

builder.Services.AddSession();

builder.Services.AddHttpContextAccessor();

// ========
// || Auth
// ========
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "DefaultCookie";
})
    .AddCookie("DefaultCookie");

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Approved", policy =>
        policy.RequireClaim("Approved", "True"))
    .AddPolicy("SeachtAccess", policy =>
        policy.RequireClaim("Position", "Seacht"))
    .AddPolicy("AdminOnly", policy =>
        policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin"));


// ======================
// || Database & Context
// ======================
var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<BusinessReportingDbContext>(options =>
{
    options.UseSqlServer(defaultConnection);
});


// ==============
// || Middleware
// ==============
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

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
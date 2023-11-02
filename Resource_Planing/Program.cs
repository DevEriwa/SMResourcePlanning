using Core.Config;
using Core.Db;
using Core.Models;
using Logic.Helpers;
using Logic.IHelpers;
using Logic.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NETCore.MailKit.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserHelper, UserHelper>();
builder.Services.AddScoped<IDropdownHelper, DropdownHelper>();
builder.Services.AddScoped<IAccountHelper, AccountHelper>();
builder.Services.AddScoped<IRotaHelper, RotaHelper>();
builder.Services.AddScoped<ILeaveApplicationHelper, LeaveApplicationHelper>();
builder.Services.AddScoped<IEmailHelper, EmailHelper>();
builder.Services.AddScoped<IEmailServices, EmailServices>();
builder.Services.AddSingleton<IEmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("Resource_Planing")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

}).AddEntityFrameworkStores<AppDbContext>();

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
UpdateDatabase(app);
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

static void UpdateDatabase(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices
        .GetRequiredService<IServiceScopeFactory>()
        .CreateScope())
    {
        using (var context = serviceScope.ServiceProvider.GetService<AppDbContext>())
        {
            context?.Database.Migrate();
        }
    }
}

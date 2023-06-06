using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using BookEvent.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<BookEventDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookEventConnectionString")));

builder.Services.AddIdentity<BookEvent.Models.ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<BookEventDBContext>()
    .AddDefaultTokenProviders()
    .AddUserManager<UserManager<BookEvent.Models.ApplicationUser>>()
    .AddDefaultUI();

builder.Services.AddScoped<SignInManager<BookEvent.Models.ApplicationUser>>();

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
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();

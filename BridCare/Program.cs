using BridCare.Data;
using BridCare.Models;
using BridCare.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// DataBase
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CaregiverService>();
builder.Services.AddScoped<InstitutionService>();
builder.Services.AddScoped<ElderlyService>();
builder.Services.AddScoped<ShiftService>();
builder.Services.AddScoped<CaregiverApplicationService>();
builder.Services.AddScoped<InstitutionApplicationService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OnlyCaregiver", policy => policy.RequireClaim("Role", "Caregiver"));
    options.AddPolicy("OnlyInstitution", policy => policy.RequireClaim("Role", "Institution"));
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

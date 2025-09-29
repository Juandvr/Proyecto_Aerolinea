using Microsoft.EntityFrameworkCore;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.Services.AirportServices;
using SystemStore.Services.AirportServices;

var builder = WebApplication.CreateBuilder(args);

// DbContext

builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Services
builder.Services.AddScoped<AddAirport>();
builder.Services.AddScoped<UpdateAirport>();
builder.Services.AddScoped<GetAirportById>();
builder.Services.AddScoped<DeleteAirport>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

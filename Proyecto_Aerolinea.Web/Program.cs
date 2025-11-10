using Microsoft.EntityFrameworkCore;
using Proyecto_Aerolinea.Web;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.Data.Seeders;
using Proyecto_Aerolinea.Web.Services.Abstract;
using Proyecto_Aerolinea.Web.Services.Abtractions;
using Proyecto_Aerolinea.Web.Services.Implementation;
using Proyecto_Aerolinea.Web.Services.Implementations;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.AddCustomConfiguration();

builder.Services.AddHttpContextAccessor(); // importante para IHttpContextAccessor





WebApplication app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var permissionsSeeder = services.GetRequiredService<PermissionsSeeder>();
        await permissionsSeeder.SeedAsync();

        var userRolesSeeder = services.GetRequiredService<UserRolesSeeder>();
        await userRolesSeeder.SeedAsync();

        Console.WriteLine("Seeders ejecutados correctamente");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error ejecutando los seeders: {ex.Message}");
    }
}

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
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.AddCustomWebApplicationConfiguration();

app.Run();
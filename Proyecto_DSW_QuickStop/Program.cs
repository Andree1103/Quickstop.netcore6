//0.importar
using Proyecto_DSW_QuickStop.Controllers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//1.Agregamos un NUEVO AMBITO para el DAO de Ventas
builder.Services.AddScoped<VentasDAO>();

//2.agregar el tiempo maxiomo de las variables de session
builder.Services.AddSession(
    tiempo => tiempo.IdleTimeout = TimeSpan.FromMinutes(20)
    );

var app = builder.Build();

//3. incorporar la session //4.Luego vamos a programar a VentasDAO.cs de DAO.
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
/*
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");*/





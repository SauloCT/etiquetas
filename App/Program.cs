using VendaERP.Core;
using App.Services;
using App.Services.Interfaces;
using App.Models;
using App.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.Configure<DBSettings>(builder.Configuration.GetSection("MongoConnection"));

builder.Services.AddSingleton<DBAccess>();

// Registrar serviços da aplicação
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IEtiquetasService, EtiquetasService>();
builder.Services.AddScoped<IAutocompletarService, AutocompletarService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Etiquetas/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Middleware de tratamento de erros
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Etiquetas}/{action=Index}/{id?}");

app.Run();

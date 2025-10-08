using ISP_API;
using ISP_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Instancia de Startup
var startup = new Startup(builder.Configuration);

// Configuraci�n de servicios
startup.ConfigureServices(builder.Services);
// Registro del servicio en segundo plano
builder.Services.AddHostedService<SaldoBackgroundService>();
// Construcci�n de la aplicaci�n
var app = builder.Build();
var env = app.Environment;

// Configuraci�n de middleware
startup.Configure(app, env);

// Ejecutar la aplicaci�n
app.Run();

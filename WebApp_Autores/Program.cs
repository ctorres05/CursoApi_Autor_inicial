using Serilog;
using WebApp_Autores;




var builder = WebApplication.CreateBuilder(args);



/********PAra escribir en el log ******/
/***************************************/
/***************************************/

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()                               // Nivel mínimo de log
    .WriteTo.Console()                                        // Salida en consola
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // Archivo de log diario
    .CreateLogger();

// Integrar Serilog con el sistema de logging
builder.Host.UseSerilog();


/*******  PAra escribir en el log ******/
/***************************************/
/***************************************/

var startup = new Startup(builder.Configuration);  /*Agrgado ct*/

startup.ConfigureServices(builder.Services);    /*Agregado Ct*/


var app = builder.Build();

var logger1 = (ILogger<Startup>)app.Services.GetService(typeof(ILogger<Startup>));


startup.Configue(app, app.Environment, logger1);   /*Agregado Ct*/

// Prueba de logs
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("La aplicación ha iniciado correctamente.");
logger.LogError("Este es un ejemplo de log de error.");


app.Run();

//Log.CloseAndFlush(); // Asegúrate de cerrar Serilog al finalizar


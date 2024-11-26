using WebApp_Autores;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup (builder.Configuration);  /*Agrgado ct*/

startup.ConfigureServices(builder.Services);    /*Agregado Ct*/



var app = builder.Build();

startup.Configue(app, app.Environment);   /*Agregado Ct*/


app.Run();

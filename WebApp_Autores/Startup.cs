﻿using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;
using WebApp_Autores.Controllers;
using WebApp_Autores.Midelware;
using WebApp_Autores.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApp_Autores.Filtros;
using WebApp_Autores.Filtros;


namespace WebApp_Autores
{
    public class Startup
    {
        public Startup(IConfiguration configuration )
        {
            Configuration = configuration;

            //var autoresController = new AutoresController(new ApplicationDbContext(null),  new ServicioA());
            //***** ESto no se usa se hace por inyeccion de dependencia en el mertodo  ConfigureServices***********/
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) 
        {
            /*services.AddControllers();  Esto es lo original basico*/


            //services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            //Amtes era esta linea , ahora para agergar los filtros para todos los controller se pone lo de abajo

            services.AddControllers(opciones => 
            {
                opciones.Filters.Add(typeof(FiltroDeExecpcion));

            }).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            services.AddTransient<IServicio, ServicioA>();   /*ESto me inyecta el servicio A cada vez que se haga referencia a 
                                                                la interface Iservicio*/

            /*Ejemplo de TEmporalidad de los servicios*/
            services.AddTransient<ServicioTransit>();
            services.AddScoped<ServicioScoped>();
            services.AddSingleton<ServicioSingleton>();

            services.AddTransient<MiFiltroDeAcciones>();   /*Aca el fitro se usa especificamente en los controller que yo quiero mediante el [MiFiltro]*/



            services.AddResponseCaching();   //me activa lo del cache
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();  /*me permite el control de acceso*/




            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configue(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {

            //  app.UseMiddleware<LogearRespuestaHttpMidelware>(); /*Se reemplaza por la linea de abajo*/
            app.UseLoguearRespuestaHttp();


            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();   
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseResponseCaching();   /*Esto me permite cahear respuestas de un peticion http*/



            app.UseAuthorization();  /*Esto es para la autorizacion de acecso*/

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


           
           

        }

    }
}

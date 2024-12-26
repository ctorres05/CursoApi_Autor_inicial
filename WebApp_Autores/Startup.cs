﻿using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;
using WebApp_Autores.Controllers;
using WebApp_Autores.Servicios;

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


            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            services.AddTransient<IServicio, ServicioA>();   /*ESto me inyecta el servicio A cada vez que se haga referencia a 
                                                                la interface Iservicio*/

            /*Ejemplo de TEmporalidad de los servicios*/
            services.AddTransient<ServicioTransit>();
            services.AddScoped<ServicioScoped>();
            services.AddSingleton<ServicioSingleton>();




            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configue(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {

            app.Use(async (contexto, siguiente) =>
            {
                using (var ms = new MemoryStream())
                {
                    var cuerpooriginalrespuesta = contexto.Response.Body;
                    contexto.Response.Body = ms;

                    await siguiente.Invoke();

                    ms.Seek(0, SeekOrigin.Begin);
                    string respueta = new StreamReader(ms).ReadToEnd();
                    ms.Seek(0, SeekOrigin.Begin);

                    await ms.CopyToAsync(cuerpooriginalrespuesta);
                    contexto.Response.Body = cuerpooriginalrespuesta;
                    respueta = "CRT %%%%%%%" + respueta + " CRT %%%%%%%";

                    logger.LogInformation(respueta);


                }
            }

            );

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


           
           

        }

    }
}

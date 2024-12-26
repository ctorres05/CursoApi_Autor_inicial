namespace WebApp_Autores.Midelware
{
    public static class LogearRespuestaHttpMidelwareExtension   /*Esta clase estatica es para simplificar el llamado o utilizacion de la misma*/
    {
        public static IApplicationBuilder UseLoguearRespuestaHttp(this IApplicationBuilder app)
        {
            return app.UseMiddleware <LogearRespuestaHttpMidelware>();

        }
    }


    public class LogearRespuestaHttpMidelware
    {
        private readonly RequestDelegate siguiente;
        private readonly ILogger<LogearRespuestaHttpMidelware> logger;
      
        public LogearRespuestaHttpMidelware(    RequestDelegate siguiente , 
                                                ILogger<LogearRespuestaHttpMidelware> logger)
        {
            this.siguiente = siguiente;
            this.logger = logger;
        }
        //los midelwarte debem tener dos metodos denominados
        // Invoke o InvokeAsyc
        //

        public async Task InvokeAsync(HttpContext contexto)
        {
            using (var ms = new MemoryStream())
            {
                var cuerpooriginalrespuesta = contexto.Response.Body;
                contexto.Response.Body = ms;
               
                await siguiente(contexto);
               
                ms.Seek(0, SeekOrigin.Begin);
                string respueta = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);

                await ms.CopyToAsync(cuerpooriginalrespuesta);
                contexto.Response.Body = cuerpooriginalrespuesta;
                respueta = "CRT %%%%%%%" + respueta + " CRT %%%%%%%";

                logger.LogInformation(respueta);


            }
        }
    }
}

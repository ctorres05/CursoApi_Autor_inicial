using Microsoft.AspNetCore.Mvc.Filters;


namespace WebApp_Autores.Filtros
{
           
    public class MiFiltroDeAcciones : IActionFilter

        {
            private readonly ILogger<MiFiltroDeAcciones> logger;

            public MiFiltroDeAcciones(ILogger<MiFiltroDeAcciones> logger)
            {
                this.logger = logger;
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                logger.LogInformation("Anste de Ejecutar la accion");
            }


            public void OnActionExecuted(ActionExecutedContext context)
            {
                logger.LogInformation("Despues de Ejecutar la accion");
            }


        }

    
}

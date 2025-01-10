using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp_Autores.Filtros
{
    public class FiltroDeExecpcion  : ExceptionFilterAttribute

    {
        private readonly ILogger<FiltroDeExecpcion> logger;

        public FiltroDeExecpcion( ILogger<FiltroDeExecpcion> logger) 

        {
            this.logger = logger;
        }


        public override void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception, context.Exception.Message);
            base.OnException(context);
        }
    }
}

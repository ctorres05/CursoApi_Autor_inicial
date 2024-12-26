namespace WebApp_Autores.Servicios


{
    public class ServicioA : IServicio
    {
        
        private readonly ILogger<ServicioA> logger;
        private readonly ServicioTransit    servicioTransit;
        private readonly ServicioScoped     servicioScoped;
        private readonly ServicioSingleton  servicioSingleton;

        public ServicioA(   ILogger<ServicioA> logger, 
                            ServicioTransit servicioTransit, 
                            ServicioScoped servicioScoped, 
                            ServicioSingleton servicioSingleton)

        {
            this.logger = logger;
            this.servicioTransit = servicioTransit;
            this.servicioScoped = servicioScoped;
            this.servicioSingleton = servicioSingleton;
        }

        public Guid DameGuidTransit ()  { return servicioTransit.guid; }
        public Guid DameGuidScoped()    { return servicioScoped.guid; }
        public Guid DameGuidSingleton() { return servicioSingleton.guid; }

        void IServicio.RealizarTarea( )
        {
            throw new NotImplementedException();
        }
    }

    public class ServicioB : IServicio
    {
        public Guid DameGuidScoped()
        {
            throw new NotImplementedException();
        }

        public Guid DameGuidSingleton()
        {
            throw new NotImplementedException();
        }

        public Guid DameGuidTransit()
        {
            throw new NotImplementedException();
        }

        void IServicio.RealizarTarea()
        {
            throw new NotImplementedException();
        }
    }

    public class ServicioTransit
    {
        public Guid guid = Guid.NewGuid();
    }

    public class ServicioScoped
    {
        public Guid guid = Guid.NewGuid();
    }

    public class ServicioSingleton
    {
        public Guid guid = Guid.NewGuid();
    }
}

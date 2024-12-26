
namespace WebApp_Autores.Servicios
{
    public interface IServicio
    {
        Guid DameGuidScoped();
        Guid DameGuidSingleton();
        Guid DameGuidTransit();
         void RealizarTarea();
    }



}

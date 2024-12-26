using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp_Autores.Entidades;
using WebApp_Autores.Servicios;


namespace WebApp_Autores.Controllers
{
    [ApiController]  /*permite hacer validaciones automaticas*/
    [Route("api/autores_CT")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext contex;
        private readonly IServicio servicio;
        private readonly ServicioTransit serv_Transit;
        private readonly ServicioTransit serv_Transit2;
        private readonly ServicioScoped serv_Scoped;
        private readonly ServicioSingleton serv_Singleton;
        private readonly ILogger<AutoresController> logger;

        public AutoresController(ApplicationDbContext contex, 
                                 IServicio          servicio, 
                                 ServicioTransit    serv_transit, 
                                 ServicioScoped     serv_scoped,  
                                 ServicioSingleton  serv_singleton   ,                               
                                 //ILogger<AutoresController> logger)
                                 ILogger<AutoresController> logger,
                                 ServicioTransit serv_transit2)

            

        {
            this.contex         = contex;
            this.servicio       = servicio;
            this.serv_Transit   = serv_transit;
            this.serv_Scoped    = serv_scoped;
            this.serv_Singleton = serv_singleton;
            this.serv_Transit2  = serv_transit2;

            this.logger = logger;
        }


        [HttpGet]                       /* ruta seria esta: api/autores_CT  */
        [HttpGet("listado")]           /* ruta seria esta: api/autores_CT/listado  */
        [HttpGet("/listado")]          /* ruta seria esta: /listado  */
        public async Task<ActionResult<List<Autor>>> Get()
        {
           // servicio.RealizarTarea();

            //logger.LogInformation("despues4 NO FUNCIONA");

            return await contex.Autores.Include(x=> x.Libros).ToListAsync();     /*Esto me trae en caso de que esten relacionados las tablas*/
          //  return await contex.Autores.ToListAsync();



        }

        [HttpGet("DameGUID")]
        public ActionResult ObtenerGuid()
        {
            logger.LogInformation("***Hola Mundo antes de retornar los guid****");
            logger.LogInformation($"despues transit serv_Transit {serv_Transit.guid} TRansit servicio.DameGuidTransit  {servicio.DameGuidTransit()} Scope  {servicio.DameGuidScoped()} Single {servicio.DameGuidSingleton()} ")  ;
            logger.LogInformation($"despues2 TRansit serv_Transit {serv_Transit.guid} TRansit servicio.DameGuidTransit {servicio.DameGuidTransit()} Scope  {servicio.DameGuidScoped()} Single {servicio.DameGuidSingleton()} ");

            var ServicioA_Tr1 = serv_Transit2.guid;



            Guid nuevoguid = Guid.NewGuid();


            logger.LogInformation($"despues3 TRansit serv_Transit2 nuevo {ServicioA_Tr1} otro Guid {nuevoguid}");



            return Ok(  
                        new 
                        {   
                            AutoresControllertransist = serv_Transit.guid ,  /*tomo el dato de la clase*/
                            ServicioA_Tr = servicio.DameGuidTransit(),       /*toma del metodo del servicio*/   
                            AutoresServiceScope = serv_Scoped.guid ,     
                            ServicioA_Sc = servicio.DameGuidScoped(),
                            AutoresSingleton = serv_Singleton.guid ,
                            ServicioA_Si = servicio.DameGuidSingleton(),
                            NuevoGuid = nuevoguid,

                        }
                        
                     );
           
        }

        [HttpGet("primero")]
        public async Task<ActionResult<Autor>> Primerautor([FromHeader] int miparametro , [FromQuery] string nombre)
        {
            return await contex.Autores.FirstOrDefaultAsync();
                //FirstOrDefaultAsync.ToListAsync();

        }

        [HttpGet("buscador/{id:int}")]
        public async Task<ActionResult<Autor>> Buscador( int id)
        {
            var existeautor = await contex.Autores.AnyAsync(x => x.Id == id);   /*Busca el id supongo que en la base de datos*/

            //FirstOrDefaultAsync.ToListAsync();

            if (!existeautor)
            {
                return NotFound("El id de autror Inexistente");
            }
            else
            {
                //return await contex.Autores.FirstOrDefaultAsync(); 
                var autor = await contex.Autores.Where(x => x.Id == id).Select(x => new { x.Id, x.Nombre, x.Edad }).FirstOrDefaultAsync();
                return Ok(autor);
                
            }
        }

        [HttpGet("buscador_curso/{id:int}/{nombre=car}")]
        public async Task<ActionResult<Autor>> Buscadorcurso([FromRoute]  int id, string nombre)
        {
            var existeautor = await contex.Autores.FirstOrDefaultAsync(x => x.Id == id);   /*Busca el id supongo que en la base de datos*/

            //FirstOrDefaultAsync.ToListAsync();

            if (existeautor == null)
            {
                return NotFound($"El id {id} de autror Inexistente  nombre {nombre}");
            }
            else
            {
               return Ok(existeautor);
            }
        }

        [HttpGet("buscador_curso/{nombre}")]  /*Busca una cadena*/
        public async Task<ActionResult<Autor>> Buscadorcurso([FromRoute] String nombre)
        {
            var existeautor = await contex.Autores.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));   /*Busca el id supongo que en la base de datos*/

            //FirstOrDefaultAsync.ToListAsync();

            if (existeautor == null)
            {
                return NotFound("El id de autror Inexistente");
            }
            else
            {
                return Ok(existeautor);
            }
        }


        [HttpPost]
        public async Task<ActionResult> Post( [FromBody] Autor autor)
        {
            var existeautor = await contex.Autores.AnyAsync(x => x.Nombre == autor.Nombre);


            if (existeautor)
            {
                return BadRequest($"El Autor cuyo nombre es {autor.Nombre} ya existe en la Base de datos");
            }

            contex.Add(autor);
            await contex.SaveChangesAsync();
            return Ok(autor);


        }

        //  [HttpPut ("{id:int}/{nom:string}")]   //Viene la url api/autores_CT/id
        [HttpPut("{id:int}")]   //Viene la url api/autores_CT/id

        public async Task<ActionResult> Put(Autor autor, int id/*, string nom */)
        {
            if (autor.Id != id)
            {
                return BadRequest("El id de la url no coinside con el id de la clase");

            }
            // autor.Nombre = "PEPE";
            var existeautor = await contex.Autores.AnyAsync(x => x.Id == id);
            if (!existeautor)
            {
                return NotFound();

            }

            contex.Update(autor);
            await contex.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(Autor autor, int id)
        {
            if (autor.Id != id)
            {
                return BadRequest("El id de la url no coinside con el id de la clase");

            }
            var existeautor = await contex.Autores.AnyAsync(x => x.Id == id);   /*Busca el id supongo que en la base de datos*/

            if (!existeautor)
            {
                return NotFound();
                
            }
            contex.Remove(autor);
            await contex.SaveChangesAsync();
            return Ok();




        }

    }


}

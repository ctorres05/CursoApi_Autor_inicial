using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp_Autores.Entidades;


namespace WebApp_Autores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public LibrosController(ApplicationDbContext contex)
        {
            this.context = contex;
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get (int id)
        {
            return await context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]

        public async Task<ActionResult> Post(Libro libro)
        {
            var existeautor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);
            if (!existeautor) 
            {
                return BadRequest($"No Existe el autor ID {libro.AutorId}");
            }

            context.Add(libro);

            await context.SaveChangesAsync();


            return Ok();
        }

       
    }
}

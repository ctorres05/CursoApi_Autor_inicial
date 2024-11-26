using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp_Autores.Entidades;


namespace WebApp_Autores.Controllers
{
    [ApiController]  /*permite hacer validaciones automaticas*/
    [Route("api/autores_CT")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext contex;

        public AutoresController(ApplicationDbContext contex)
        {
            this.contex = contex;

        }


        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await contex.Autores.Include(x=> x.Libros).ToListAsync();


        }

        [HttpPost]

        public async Task<ActionResult> Post(Autor autor)
        {
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

using Microsoft.AspNetCore.Mvc;
using WebApp_Autores.Entidades;

namespace WebApp_Autores.Controllers
{
    [ApiController]
    [Route("api/Xautores_CT_V2")]
    public class XAutores_v2Controller : Controller
    {
        [HttpGet]
        public ActionResult<List<Autor>> Get()
        {
            return new List<Autor>()
            {
                new Autor() {Id = 10, Nombre = "XCarlos",     Edad = 533 },
                new Autor() {Id = 20, Nombre = "X1Carlos",    Edad = 513 },
                new Autor() {Id = 30, Nombre = "X2Carlos",    Edad = 533 },
                new Autor() {Id = 40, Nombre = "X33Carlos",   Edad = 153 },
                new Autor() {Id = 50, Nombre = "X444Carlos",  Edad = 53 }

            };

        }
    }
    [ApiController]
    [Route("api/Xautores_CT_V3")]
    public class XAutores_v3Controller : Controller
    {
        [HttpGet]
        public ActionResult<List<Autor>> Get()
        {
            return new List<Autor>()
            {
                new Autor() {Id = 10, Nombre = "XXXCarlos",     Edad = 533 },
                new Autor() {Id = 20, Nombre = "XXX1Carlos",    Edad = 513 },
                new Autor() {Id = 30, Nombre = "XXX2Carlos",    Edad = 533 },
                new Autor() {Id = 40, Nombre = "XXX33Carlos",   Edad = 153 },
                new Autor() {Id = 50, Nombre = "XXX444Carlos",  Edad = 53 }

            };

        }
    }
}

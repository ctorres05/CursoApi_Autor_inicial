﻿using WebApp_Autores.Validaciones;

namespace WebApp_Autores.Entidades
{
    public class Libro
    {
        public int Id { get; set; }

        [PrimeraLetraMayusculaAtribute]
        public string Titulo { get; set; }

        public int AutorId { get; set; }
        public Autor Autor { get; set; }   /*propiedad de navegacion supongo que es para la FK*/



    }
}

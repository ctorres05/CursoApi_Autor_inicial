﻿namespace WebApp_Autores.Entidades
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }

        public List<Libro> Libros { get; set; }

    }
}

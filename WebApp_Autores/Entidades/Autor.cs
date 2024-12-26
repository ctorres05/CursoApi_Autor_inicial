using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Timers;
using WebApp_Autores.Validaciones;

namespace WebApp_Autores.Entidades
{
    public class Autor  : IValidatableObject
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido ")]
        [StringLength(maximumLength: 50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [PrimeraLetraMayusculaAtribute] /*Esta es una validacion mia que esta en la carpeta de Validaciones del proyecto*/
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido ")]
        [Range (18,120)]
        public int Edad { get; set; }


        [NotMapped]
        [Url]
        public string Url { get; set; }

        public List<Libro> Libros { get; set; }

        [NotMapped]
        public int Mayor { get; set; }

        [NotMapped]
        public int Menor { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Mayor < Menor)
                yield return new ValidationResult("El campo mayor {Mayor} no puede ser menor a  " , new string[] { nameof(Menor) });
             
            
            //throw new NotImplementedException();
        }
    }
}

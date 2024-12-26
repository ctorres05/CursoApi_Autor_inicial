using System.ComponentModel.DataAnnotations;

namespace WebApp_Autores.Validaciones
{
    public class PrimeraLetraMayusculaAtribute : ValidationAttribute

    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
             return ValidationResult.Success;
            }

            var primeraletra = value.ToString()[0].ToString();
            if (primeraletra != primeraletra.ToUpper())
            {
                return new ValidationResult("La primera letra del campo debe ser Mayuscula");

            }   
            return ValidationResult.Success;
            
        }

    }
}

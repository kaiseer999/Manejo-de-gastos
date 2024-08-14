using System.ComponentModel.DataAnnotations;

namespace Manejo_de_gastos.Validaciones
{
    public class PrimeraLetraMayusculaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var Primeraletra = value.ToString()[0].ToString();

            if (Primeraletra != Primeraletra.ToUpper())
            {
                return new ValidationResult("La primera debe de ser mayuscula");
            }

            return ValidationResult.Success;

        }
    }
}

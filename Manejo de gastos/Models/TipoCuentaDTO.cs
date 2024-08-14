using Manejo_de_gastos.Controllers;
using Manejo_de_gastos.Validaciones;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace Manejo_de_gastos.Models
{
    public class TipoCuentaDTO /*:IValidatableObject*/
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "El {0} debe de estar entre {1} y {2} carcateres de longitud")]
        [Display(Name = "Nombre del tipo cuenta")]
        [PrimeraLetraMayuscula]
        [Remote(action: "VerificarExisteTipoCuenta", controller: "TipoCuentas")]
        public string Nombre { get; set; }
        public int UsuarioId { get; set;}
        public int Orden { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

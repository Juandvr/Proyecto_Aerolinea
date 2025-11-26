using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.DTOs
{
    public class ChangaPasswordDTO
    {
        [Display(Name = "Contraseña actual")]
        [MinLength(4, ErrorMessage = "Minimo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Display(Name = "Nueva Contraseña")]
        [MinLength(4, ErrorMessage = "Minimo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Display(Name = "Nueva Contraseña")]
        [MinLength(4, ErrorMessage = "Minimo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }
    }
}

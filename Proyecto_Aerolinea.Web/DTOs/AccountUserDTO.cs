using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.DTOs
{
    public class AccountUserDTO
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un Email válido")]
        [Display(Name = "Correo")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(100, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public required string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public required string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Display(Name = "Documento")]
        public required string Document { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Display(Name = "Nombre")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Display(Name = "Apellido")]
        public required string LastName { get; set; }

        [Display(Name = "Foto")]
        public string? Photo { get; set; }

        [Display(Name = "Teléfono")]
        [MaxLength(32)]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string PhoneNumber { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
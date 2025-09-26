using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Models.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required, StringLength(50)]
        public string FirsName { get; set; }
        [Required, StringLength(50)]
        public string LastName { get; set; }
        [Required, StringLength(100)]
        public string Email { get; set; }
        [Required, StringLength(50)]
        public string Password { get; set; }
        [Required, StringLength(15)]
        public string Phone { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Role { get; set; }

        // Relacion con Reservation

        public ICollection<Reservation> Reservations { get; set; }
    }
}

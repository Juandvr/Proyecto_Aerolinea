using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Models.Entities
{
    public class Passenger
    {
        [Key]
        public Guid PassengerId { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required, StringLength(20)]
        public string Phone { get; set; }

        [Required, StringLength(100)]
        public string Email { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        // Relaciones
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}

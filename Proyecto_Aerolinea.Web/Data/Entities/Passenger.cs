using Proyecto_Aerolinea.Web.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Data.Entities
{
    public class Passenger : IId
    {
        [Key]
        public Guid Id { get; set; }

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

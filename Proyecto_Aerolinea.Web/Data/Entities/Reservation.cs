using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Data.Entities
{
    public class Reservation
    {
        [Key]
        public Guid ReservationId { get; set; }

        [Required]
        public DateTime ReservationDate { get; set; }

        [Required, StringLength(25)]
        public string Status { get; set; }

        // FK User
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }

        // Relaciones
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}

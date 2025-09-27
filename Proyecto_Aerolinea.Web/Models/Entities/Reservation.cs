using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Models.Entities
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        [Required]
        public DateTime ReservationDate { get; set; }
        [Required, StringLength(25)]
        public string Status { get; set; }

        // FK User
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        // Relacion con Payment
        public ICollection<Payment> Payments { get; set; }

        // Relacion con Ticket
        public ICollection<Ticket> Tickets { get; set; }
    }
}

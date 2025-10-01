using Proyecto_Aerolinea.Web.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Data.Entities
{
    public class Payment : IId
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required, StringLength(50)]
        public string PaymentMethod { get; set; }

        [Required, StringLength(30)]
        public string Status { get; set; }

        // FK Reservation
        [Required]
        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}

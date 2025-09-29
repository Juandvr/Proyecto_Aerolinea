using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Models.Entities
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

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
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}

using Microsoft.Identity.Client;
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
        [Required]

        // FK Reservation
        public int Reservationid { get; set; }
        public Reservation reservation { get; set; }

    }
}

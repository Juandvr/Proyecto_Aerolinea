using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Models.Entities
{
    public class Ticket
    {

        [Key]
        public int TicketId { get; set; }
        [Required, StringLength(100)]
        public string QRCodr { get; set; }
        [Required]
        public decimal Price { get; set; }
        // FK Reservation
        [Required]
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        // FK Flight
        [Required]
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
        // FK Passenger
        [Required]
        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }
        // FK SeatAssignment
        public int SeatAssignmentId { get; set; }
        public SeatAssignment SeatAssignment { get; set; }
    }
}

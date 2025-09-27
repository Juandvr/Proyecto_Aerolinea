using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Models.Entities
{
    public class Seat
    {
        [Key]
        public int SeatId { get; set; }
        [Required, StringLength(25)]
        public string SeatNumber { get; set; }
        [Required, StringLength(50)]
        public string Class { get; set; }
        // FK Aircraft
        [Required]
        public int AircraftId { get; set; }
        public Aircraft Aircraft { get; set; }
        // Relacion con SeatAssignment
        public ICollection<SeatAssignment> SeatAssignments { get; set; }
    }
}

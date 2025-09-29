using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Models.Entities
{
    public class Seat
    {
        [Key]
        public Guid SeatId { get; set; }

        [Required, StringLength(25)]
        public string SeatNumber { get; set; }

        [Required, StringLength(50)]
        public string Class { get; set; }

        // FK Aircraft
        [Required]
        public Guid AircraftId { get; set; }
        public Aircraft Aircraft { get; set; }

        // Relaciones
        public ICollection<SeatAssignment> SeatAssignments { get; set; } = new List<SeatAssignment>();
    }
}

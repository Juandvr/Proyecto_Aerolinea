using Proyecto_Aerolinea.Web.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Data.Entities
{
    public class Seat : IId
    {
        [Key]
        public Guid Id { get; set; }

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

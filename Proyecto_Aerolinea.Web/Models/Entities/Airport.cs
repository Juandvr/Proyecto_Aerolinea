using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Models.Entities
{
    public class Airport
    {
        [Key]
        public int AirpotId  { get; set; }
        [Required, StringLength(80)]
        public string AirpotName { get; set; }
        [Required, StringLength(80)]
        public string AirpotCity { get; set; }
        [Required, StringLength(80)]
        public string AirpotCountry { get; set; }
        [Required]
        public string IATACode { get; set; }
        // Relacion con Flight
        public ICollection<Flight> Flights { get; set; }
    }
}

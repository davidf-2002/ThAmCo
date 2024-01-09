using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Dtos
{
    public class ReservationDTO
    {
        //[Key, MinLength(13), MaxLength(13)]
        public string Reference { get; set; }

        //[DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        //[Required]
        //[MinLength(5), MaxLength(5)]
        public string VenueCode { get; set; }
    }
}

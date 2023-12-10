using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.DTO
{
    public class MenuDTO
    {
        public int MenuId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

    }
}

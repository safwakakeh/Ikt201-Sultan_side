using System.ComponentModel.DataAnnotations;

namespace Ikt201_Sultan_side.Models
{
    public class Bord
    {
        [Key]
        public int BordId { get; set; }
        [Required]
        public short Plasser { get; set; }
        [Required]
        public short MaksPlasser { get; set; }
    }
}

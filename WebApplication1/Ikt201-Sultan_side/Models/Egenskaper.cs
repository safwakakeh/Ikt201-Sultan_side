using System.ComponentModel.DataAnnotations;

namespace Ikt201_Sultan_side.Models
{
    public class Egenskaper
    {
        [Key]
        public int EgenskapId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Navn { get; set; }
    }
}

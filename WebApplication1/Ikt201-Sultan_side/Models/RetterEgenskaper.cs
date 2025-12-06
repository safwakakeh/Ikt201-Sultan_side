using System.ComponentModel.DataAnnotations;

namespace Ikt201_Sultan_side.Models
{
    public class RetterEgenskaper
    {
        [Key]
        public int RetterEgenskaperId { get; set; }
        [Required]
        public int RettId { get; set; }
        [Required]
        public int EgenskapId { get; set; }
    }
}

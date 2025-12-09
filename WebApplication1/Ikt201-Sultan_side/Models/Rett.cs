using System.ComponentModel.DataAnnotations;

namespace Ikt201_Sultan_side.Models
{
    public class Rett
    {
        [Key]
        public int RettId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Navn { get; set; }
        [Required]
        public int KategoriId { get; set; }
        [Required]
        public float Pris { get; set; }
        public bool Tilgjengelighet { get; set; } = true;
        public string Beskrivelse { get; set; }
    }
}

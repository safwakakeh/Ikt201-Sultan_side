using System.ComponentModel.DataAnnotations;

namespace Ikt201_Sultan_side.Models
{
    public class Kategori
    {
        [Key]
        public int KategoriId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Navn { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Ikt201_Sultan_side.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Navn { get; set; }
        [Required]
        [MaxLength(150)]
        public string Epost { get; set; }
        [MaxLength(15)]
        public string Telefon { get; set; }
        public bool Admin { get; set; }
    }
}

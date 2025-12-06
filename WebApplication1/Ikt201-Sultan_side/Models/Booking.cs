using System;
using System.ComponentModel.DataAnnotations;

namespace Ikt201_Sultan_side.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        [Required]
        public int PersonId { get; set; }
        [Required]
        public int BordId { get; set; }
        [Required]
        public DateTime Tid { get; set; }
        [Required]
        public DateTime TidSlutt { get; set; }
        [Required]
        public short AntallGjester { get; set; }
        public bool Bekreftet { get; set; } = false;
        public int? BekreftetAdminId { get; set; }
        public DateTime? BekreftetTid { get; set; }
    }
}

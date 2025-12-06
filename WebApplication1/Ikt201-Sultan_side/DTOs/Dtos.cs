namespace Ikt201_Sultan_side.DTOs
{
    public class EgenskaperDto
    {
        public int EgenskapId { get; set; }
        public string Navn { get; set; }
    }

    public class PersonDto
    {
        public int PersonId { get; set; }
        public string Navn { get; set; }
        public string Epost { get; set; }
        public string Telefon { get; set; }
        public bool Admin { get; set; }
    }

    public class BordDto
    {
        public int BordId { get; set; }
        public short Plasser { get; set; }
        public short MaksPlasser { get; set; }
    }

    public class BookingDto
    {
        public int BookingId { get; set; }
        public int KundeId { get; set; }
        public int BordId { get; set; }
        public DateTime Tid { get; set; }
        public DateTime TidSlutt { get; set; }
        public short AntallGjester { get; set; }
        public bool Bekreftet { get; set; }
        public int? BekreftetAdminId { get; set; }
        public DateTime? BekreftetTid { get; set; }
    }

    public class KategoriDto
    {
        public int KategoriId { get; set; }
        public string Navn { get; set; }
    }

    public class RettDto
    {
        public int RettId { get; set; }
        public string Navn { get; set; }
        public int KategoriId { get; set; }
        public float Pris { get; set; }
        public bool Tilgjengelighet { get; set; }
        public string Beskrivelse { get; set; }
    }

    public class RetterEgenskaperDto
    {
        public int RetterEgenskaperId { get; set; }
        public int RettId { get; set; }
        public int EgenskapId { get; set; }
    }
}

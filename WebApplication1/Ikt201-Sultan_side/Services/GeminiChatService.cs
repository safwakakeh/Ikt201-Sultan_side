using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.GenAI;

namespace Ikt201_Sultan_side.Services
{
     public class GeminiChatService
    {
        private readonly Client _client;

        private const string SystemInstruction = @"
Du er AI-vert for restauranten 'Sultan Oslo Food & Sweets – مطعم و حلويات السلطان أوسلو' i Oslo.

- Svar på samme språk som brukeren (norsk, arabisk eller engelsk).
- Vær varm, høflig og hjelpsom.
- Du kan anbefale mat, søtsaker og drikke.
- Du kan svare enkelt om halal, vegetar og allergier
  (men minn om at gjesten må dobbeltsjekke med personalet).
- Du kan gi informasjon om meny, åpningstider, beliggenhet og generell service.
";
        
        private const string RestaurantFacts = @"
Tjenestealternativer: Uteservering · Peis · Egnet for å se sport
Adresse: Grønlandsleiret 16B, 0190 Oslo
Telefon: 40 30 90 95

Åpningstider:
- Søndag: 11–03
- Mandag: 11–03
- Tirsdag: 11–03
- Onsdag: 11–03
- Torsdag: 11–03
- Fredag: 11–03
- Lørdag: 11–04

Meny:

--- Vestlig meny (Nyhet) ---
Meksikano (Spicy) Tallerken – 239 kr
Kylling Fajita i Rull – 180 kr
Kylling Fajita Tallerken – 239 kr
Zinger (Spicy) i Rull – 180 kr
Zinger (Spicy) Tallerken – 239 kr
Meksikano (Spicy) i Rull – 180 kr
Kylling Crispy i Rull – 180 kr
Kylling Crispy Tallerken – 239 kr

--- Shawarma ---
Lamme Shawarma i Rull – 160 kr
Lamme Shawarma Tallerken – 229 kr
Kylling Shawarma Tallerken – 219 kr
Kylling Shawarma i Rull – 150 kr
Sultan Spesial Kylling Shawarma Tallerken – 239 kr

--- Forretter ---
Mtabal – 75 kr
Hummus med Kjøtt – 99 kr
Mini Meze – 150 kr
Yalanji – 99 kr
Baba Ghanough – 75 kr
Salat – 75 kr
Pommes Frites – 50 kr
Hummus – 75 kr
Muhammara – 75 kr

--- Drikke ---
Coca-Cola / Zero / Fanta / Urge – 39 kr
Solo, Mozell – 45 kr
Mango Juice – 49 kr
Ayran – 39 kr
Palestina Cola Zero – 39 kr

--- Burger ---
Spicy Burger – 169 kr
Classic Beef Burger – 169 kr
Crispy Chicken Burger – 159 kr
Mushroom Burger – 169 kr

--- Søtsaker ---
Baklava – 120 kr
Kunafa – 120 kr
Harisa (2 stk) – 100 kr
Assorted Nawashif – 390 kr
Pistasj Baklava / Mamoul / Honning-baklava – ulike priser

--- Pizza Rossa ---
Indiavolata – 220 kr
Sultan Spesial Pizza – 220 kr
Tunfisk Pizza – 220 kr
Classic Beef Pizza – 220 kr
Spicy Margherita – 180 kr
Pepperoni & Ananas – 220 kr
Capricciosa – 220 kr
Marinara – 180 kr
Mixed Pizza – 220 kr
Margherita – 180 kr
Vegetariana – 190 kr
La Polo – 220 kr

--- Pizza Bianca ---
Truffel Beef – 220 kr
Funghi E Tartufo – 220 kr
Pollo Bianco – 220 kr
";


        public GeminiChatService(Client client)
        {
            _client = client;
        }

        public async Task<string> AskAsync(string userMessage)
        {
            var prompt = new StringBuilder();
            prompt.AppendLine(SystemInstruction);
            prompt.AppendLine();
            prompt.AppendLine("INFORMASJON OM RESTAURANTEN:");
            prompt.AppendLine(RestaurantFacts);
            prompt.AppendLine();
            prompt.AppendLine("Bruker:");
            prompt.AppendLine(userMessage);

            var response = await _client.Models.GenerateContentAsync(
                model: "gemini-2.5-flash",
                contents: prompt.ToString());

            // Hent ut bare tekst fra alle parts som faktisk har tekst
            var candidate = response.Candidates?.FirstOrDefault();
            var content = candidate?.Content;

            var textParts = content?.Parts?
                .Select(p => p.Text)                // p.Text er selve svaret
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .ToList();

            var text = (textParts != null && textParts.Count > 0)
                ? string.Join("\n", textParts)
                : null;

            return string.IsNullOrWhiteSpace(text)
                ? "Beklager, jeg klarte ikke å lage et svar akkurat nå."
                : text.Trim();
        }

    }
}
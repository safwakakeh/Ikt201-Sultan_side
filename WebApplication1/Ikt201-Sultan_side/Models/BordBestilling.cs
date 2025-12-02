using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ikt201_Sultan_side.Models;

public class Bordbestilling
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public int Guests { get; set; }
    public DateTime Time { get; set; }

    public List<SelectListItem> GuestOptions { get; set; } =
        Enumerable.Range(1, 10)
            .Select(i => new SelectListItem { Value = i.ToString(), Text = i.ToString() })
            .ToList();
}

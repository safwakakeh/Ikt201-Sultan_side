using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ikt201_Sultan_side.Models;

public class Bordbestilling
{
    public string Navn { get; set; }
    public string Tlf { get; set; }
    public string Email { get; set; }
    public int Gjester { get; set; }
    public DateTime Time { get; set; }

    public List<SelectListItem> GuestOptions { get; set; } =
        Enumerable.Range(1, 10)
            .Select(i => new SelectListItem { Value = i.ToString(), Text = i.ToString() })
            .ToList();
}

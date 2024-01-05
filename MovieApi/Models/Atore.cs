using System;
using System.Collections.Generic;

namespace MovieApi.Models;

public partial class Atore
{
    public int Id { get; set; }

    public string? PrimeiroNome { get; set; }

    public string? UltimoNome { get; set; }

    public string? Genero { get; set; }

    public virtual ICollection<ElencoFilme> ElencoFilmes { get; set; } = new List<ElencoFilme>();
}

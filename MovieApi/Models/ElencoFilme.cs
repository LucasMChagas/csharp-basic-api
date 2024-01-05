using System;
using System.Collections.Generic;

namespace MovieApi.Models;

public partial class ElencoFilme
{
    public int Id { get; set; }

    public int IdAtor { get; set; }

    public int? IdFilme { get; set; }

    public string? Papel { get; set; }

    public virtual Atore IdAtorNavigation { get; set; } = null!;

    public virtual Filme? IdFilmeNavigation { get; set; }
}

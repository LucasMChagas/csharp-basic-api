using System;
using System.Collections.Generic;

namespace MovieApi.Models;

public partial class Filme
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public int? Ano { get; set; }

    public int? Duracao { get; set; }

    public virtual ICollection<ElencoFilme> ElencoFilmes { get; set; } = new List<ElencoFilme>();

    public virtual ICollection<FilmesGenero> FilmesGeneros { get; set; } = new List<FilmesGenero>();
}

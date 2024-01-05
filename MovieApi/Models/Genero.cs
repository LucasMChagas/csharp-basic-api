using System;
using System.Collections.Generic;

namespace MovieApi.Models;

public partial class Genero
{
    public int Id { get; set; }

    public string? Genero1 { get; set; }

    public virtual ICollection<FilmesGenero> FilmesGeneros { get; set; } = new List<FilmesGenero>();
}

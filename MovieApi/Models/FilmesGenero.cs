using System;
using System.Collections.Generic;

namespace MovieApi.Models;

public partial class FilmesGenero
{
    public int Id { get; set; }

    public int? IdGenero { get; set; }

    public int? IdFilme { get; set; }

    public virtual Filme? IdFilmeNavigation { get; set; }

    public virtual Genero? IdGeneroNavigation { get; set; }
}

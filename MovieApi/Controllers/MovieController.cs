using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models;
using MovieApi.ViewModels;

namespace MovieApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public MovieController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("get")]
        public IActionResult Get([FromQuery]int page = 1, [FromQuery] int perPage = 10)
        {
            try
            {
                var movies = _appDbContext
                            .Filmes
                            .AsNoTracking()
                            .Skip((page - 1) * perPage)
                            .Take(perPage)
                            .OrderBy(x => x.Nome)
                            .ToList();

                return Ok(new ResultViewModel<List<Filme>>(movies));
            }
            catch (Exception error)
            {
                return BadRequest(new ResultViewModel<List<Filme>>(error.Message));
            }
        }

        [HttpGet("get/{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var movie = _appDbContext
                .Filmes                               
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);

                if (movie == null)
                    return NotFound(new ResultViewModel<Filme>("Conteúdo não encontrado!"));

                return Ok(movie);
            }
            catch (Exception error)
            {
                return StatusCode(500, new ResultViewModel<List<Filme>>("Falha interna no servidor"));
            }

        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] MovieEditorViewModel model)
        {
            try
            {
                var movie = new Filme
                {
                    Nome = model.Nome,
                    Ano = model.Ano,
                    Duracao = model.Duracao,
                };

                _appDbContext.Filmes.Add(movie);
                _appDbContext.SaveChanges();

                return Created("/", movie);
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Filme>("Erro ao criar"));
            }
        }

        [HttpPut("update/{id:int}")]
        public IActionResult UpdateMovie([FromRoute] int id, [FromBody] MovieEditorViewModel model)
        {
            try
            {
                var movie = _appDbContext.Filmes
                            .FirstOrDefault(x => x.Id == id);

                if (movie == null)
                    return NotFound(new ResultViewModel<Filme>("Conteúdo não encontrado!"));

                movie.Nome = model.Nome;
                movie.Ano = model.Ano;
                movie.Duracao = model.Duracao;

                _appDbContext.Filmes.Update(movie);
                _appDbContext.SaveChanges();

                return Ok(new ResultViewModel<Filme>(movie));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<List<Filme>>("Falha interna no servidor"));
            }
        }

        [HttpDelete("delete/{id:int}")]
        public IActionResult DeleteMovie([FromRoute] int id)
        {
            try
            {
                var movie = _appDbContext.Filmes.FirstOrDefault(x => x.Id == id);

                var registrosElencos = _appDbContext.ElencoFilmes.Where(x => x.IdFilme == id);
                var registrosGeneros = _appDbContext.FilmesGeneros.Where(x => x.IdFilme == id);

                if (movie == null)
                    return NotFound(new ResultViewModel<Filme>("Conteúdo não encontrado!"));

                _appDbContext.ElencoFilmes.RemoveRange(registrosElencos);
                _appDbContext.FilmesGeneros.RemoveRange(registrosGeneros);
                _appDbContext.Filmes.Remove(movie);

                _appDbContext.SaveChanges();

                return Ok(new ResultViewModel<Filme>(movie));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<List<Filme>>("Falha interna no servidor"));
            }
        }
    }
}

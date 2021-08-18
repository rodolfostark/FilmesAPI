using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;

        public FilmeController(FilmeContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] Filme filme)
        {
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmePorId), new { Id = filme.Id }, filme);
        }
        [HttpGet]
        public IEnumerable<Filme> RecuperarFilmes()
        {
            return _context.Filmes;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if(filme != null)
            {
                return Ok(filme);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarFilme(int id, [FromBody] Filme filmeNovo)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if(filme == null)
            {
                return NotFound();
            }
            filme.Titulo = filmeNovo.Titulo;
            filme.Genero = filmeNovo.Genero;
            filme.Diretor = filmeNovo.Diretor;
            filme.Duracao = filmeNovo.Duracao;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return NotFound();
            }
            _context.Filmes.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

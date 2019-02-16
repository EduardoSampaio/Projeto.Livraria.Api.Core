using Microsoft.AspNetCore.Mvc;
using Projeto.Livraria.Dados.Interfaces;
using Projeto.Livraria.Entidades;
using System;

namespace Projeto.Livaria.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {

        private readonly ILivroRepositorio _repo;

        public LivrosController(ILivroRepositorio repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult FindAll()
        {
            var livros = _repo.GetAll();
            return Ok(livros);
        }

        [HttpGet("{id}")]
        public IActionResult FindById(int id)
        {
            var usuario = _repo.Find(id);
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Save([FromBody] Livro livro)
        {
            try
            {
                _repo.Add(livro);
                _repo.SaveChanges();
                return Created("", livro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]   Livro livro)
        {
            try
            {
                var entidade = _repo.Find(id);
                if (entidade == null)
                {
                    return NotFound();
                }

                _repo.Update(livro);
                _repo.SaveChanges();
                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var entidade = _repo.Find(id);
                if (entidade == null)
                {
                    return NotFound();
                }

                _repo.Delete(id);
                _repo.SaveChanges();
                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}


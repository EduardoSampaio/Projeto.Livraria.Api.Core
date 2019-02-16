using Microsoft.AspNetCore.Mvc;
using Projeto.Livraria.Dados.Interfaces;
using Projeto.Livraria.Entidades;
using System;


namespace Projeto.Livaria.Api.Controllers
{

    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepositorio _repo;

        public UsuariosController(IUsuarioRepositorio repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult FindAll()
        {
            var usuarios = _repo.GetAll();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult FindById(int id)
        {
            var usuario = _repo.Find(id);
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Save([FromBody] Usuario usuario)
        {
            try
            {
                _repo.Add(usuario);
                _repo.SaveChanges();
                return Created("", usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("/login")]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            try
            {
                var entidade =_repo.Login(usuario);

                if (entidade == null) {
                    return NotFound();
                }
      
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]  Usuario usuario)
        {
            try
            {
                var entidade = _repo.Find(id);
                if (entidade == null)
                {
                    return NotFound();
                }

                _repo.Update(usuario);
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

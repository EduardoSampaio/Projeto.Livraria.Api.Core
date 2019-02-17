using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Projeto.Livaria.Api.Models;
using Projeto.Livraria.Dados.Interfaces;
using Projeto.Livraria.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.Livaria.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {

        private readonly ILivroRepositorio _repo;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public LivrosController(ILivroRepositorio repo,IMapper mapper,ILogger<LivrosController> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult FindAll()
        {
            var livros = _repo.GetAll();
            var model = _mapper.Map<List<LivroModel>>(livros);
            _logger.LogInformation("Response: ", model);

            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult FindById(int id)
        {
          
            var livro = _repo.Find(id);
            var model = _mapper.Map<LivroModel>(livro);
            _logger.LogInformation("Response: ",model);
            return Ok(model);
        }

        [HttpPost]
        public IActionResult Save([FromBody] LivroModelCadastrar model)
        {
            if (!ModelState.IsValid)        
                return BadRequest();
            

            try
            {
               
                var entidade = _mapper.Map<Livro>(model);
                _repo.Add(entidade);
                _repo.SaveChanges();

                _logger.LogInformation("Salvo com sucesso!");

                return Created("", model);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Erro ao Salvar");
                return BadRequest(ex.Message);
            }

        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]   LivroModel model)
        {
            try
            {
                var entidade = _repo.Find(id);
                if (entidade == null)
                {
                    _logger.LogCritical("Objeto Não Encontrado");
                    return NotFound();
                }

                entidade = _mapper.Map<Livro>(model);

                _repo.Update(entidade);
                _repo.SaveChanges();
                _logger.LogInformation("Atualizado com sucesso !");
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogCritical("Erro ao Atualizar");
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
                    _logger.LogCritical("Objeto Não Encontrado");
                    return NotFound();
                }

                _repo.Delete(id);
                _repo.SaveChanges();
                _logger.LogInformation("Deletado com sucesso !");
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogCritical("Erro ao Deletar");
                return BadRequest(ex.Message);
            }
        }
    }
}


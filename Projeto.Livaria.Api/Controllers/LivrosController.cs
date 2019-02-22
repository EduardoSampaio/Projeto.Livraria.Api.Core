using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Projeto.Livaria.Api.Models;
using Projeto.Livraria.Dados.Interfaces;
using Projeto.Livraria.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

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
        public ResponseHandler FindAll()
        {
            var livros = _repo.GetAll();
            var model = _mapper.Map<List<LivroModel>>(livros);
            _logger.LogInformation("Response: ", model);

            return ResponseHandler.BuildResponse(model, "v1", DateTime.Now, HttpStatusCode.OK, HttpContext.Response);
        }

        [HttpGet("{id}")]
        public ResponseHandler FindById(int id)
        {
          
            var livro = _repo.Find(id);
            if (livro == null) {
                _logger.LogInformation("Objeto Não encontrado");
                return ResponseHandler.BuildResponse("v1", "Objeto Não encontrado", DateTime.Now, HttpStatusCode.NotFound, HttpContext.Response);
            }
            var model = _mapper.Map<LivroModel>(livro);
            _logger.LogInformation("Response: ",model);


            return ResponseHandler.BuildResponse(model, "v1", DateTime.Now, HttpStatusCode.OK, HttpContext.Response);
        }

        [HttpPost]
        public ResponseHandler Save([FromBody] LivroModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Erro na validação");
            }
    
            try
            {
               
                var entidade = _mapper.Map<Livro>(model);
                _repo.Add(entidade);
                _repo.SaveChanges();

                _logger.LogInformation("Salvo com sucesso!");

                return ResponseHandler.BuildResponse(model, "v1", DateTime.Now, HttpStatusCode.Created, HttpContext.Response);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Erro ao Salvar");
                return ResponseHandler.BuildResponse("v1", $"Erro ao Salvar exception: {ex.Message} ", DateTime.Now, HttpStatusCode.NotFound, HttpContext.Response);
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


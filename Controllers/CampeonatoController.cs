using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CadastroDeCampeonato.Data;
using CadastroDeCampeonato.Models;
using CadastroDeCampeonato.Repository.Interfaces;
using CadastroDeCampeonato.Services.Interfaces;
using CadastroDeCampeonato.Models.Dtos;
using CadastroDeCampeonato.Services;
using NuGet.Protocol;

namespace CadastroDeCampeonato.Controllers
{
    [Route("api/campeonato")]
    [ApiController]
    public class CampeonatoController : ControllerBase
    {
        private readonly ICampeonatoService _campeonatoService;

        public CampeonatoController(ICampeonatoService campeonatoService)
        {
            _campeonatoService = campeonatoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campeonato>>> Getcampeonato()
        {
            return await _campeonatoService.List();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Campeonato>> GetCampeonato(int id)
        {
            var campeonato = await _campeonatoService.GetEntityById(id);

            if (campeonato == null)
            {
                return NotFound();
            }

            return campeonato;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCampeonato(int id, Campeonato campeonato)
        {
            if (id != campeonato.id)
            {
                return BadRequest();
            }

            try
            {
                _campeonatoService.Update(campeonato);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_campeonatoService.GetEntityById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Campeonato>> PostCampeonato(Campeonato campeonato)
        {
            if (_campeonatoService.GetEntityByName(campeonato.nome).Result == null)
            {
                _campeonatoService.Create(campeonato);
                return CreatedAtAction("GetCampeonato", new { id = campeonato.id }, campeonato);
            }
            else
                return StatusCode(500, $"Erro: O campeonato {campeonato.nome} já existe!");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampeonato(int id)
        {
            var campeonato = await _campeonatoService.GetEntityById(id);
            if (campeonato == null)
            {
                return NotFound();
            }

            _campeonatoService.Delete(campeonato);

            return NoContent();
        }

        [Route("time-campeonato")]
        [HttpPost]
        public async Task<ActionResult<Campeonato>> PostCampeonatoTime(string nomeCampeonato, string nomeTime)
        {
            CampeonatoTimeDto campeonatoTime = new CampeonatoTimeDto()
            {
                NomeCampeonato = nomeCampeonato,
                NomeTime = nomeTime
            };
            Campeonato campeonato = _campeonatoService.GetEntityByName(nomeCampeonato).Result;

            if (campeonato == null)
                return StatusCode(500, $"Erro: O campeonato {nomeCampeonato} não existe");
            else if (_campeonatoService.ValidarCampeonato(nomeCampeonato))
                return StatusCode(500, $"Erro: O campeonato {nomeCampeonato} já possui 8 times existe");
            else if (_campeonatoService.ValidarTimeCadastrado(campeonatoTime))
            {

                return StatusCode(500, $"Erro: O time {nomeTime} Já foi cadastrado");

            }
            else if (!_campeonatoService.ValidarTimeExistente(nomeTime))
            {
                return StatusCode(500, $"Erro: O time {nomeTime} não foi encontrado");

            }
            else
            {
                _campeonatoService.CadastrarTime(campeonatoTime);
                return StatusCode(200, $"Ok, Time {nomeTime} cadastrado!");
            }
        }

        [Route("gerar-campeonato")]
        [HttpPost]
        public async Task<ActionResult<Campeonato>> GerarCampeonato(string nomeCampeonato)
        {
            if (_campeonatoService.GetEntityByName(nomeCampeonato).Result == null)
                return StatusCode(500, $"Erro: O campeonato {nomeCampeonato} não existe");
            else if (_campeonatoService.ValidarCampeonato(nomeCampeonato))
            {
                List<PartidaTime> partidas = _campeonatoService.GerarCampeonato(nomeCampeonato).Result;
                var timeCampeao = _campeonatoService.GetCampeao(nomeCampeonato).Result;
                return StatusCode(200, $"Resultado do campeonato: {timeCampeao.ToJson()}");
            }
            else
                return StatusCode(500, $"Erro: O campeonato {nomeCampeonato} não possui 8 times ou já foi gerado!");
        }

        [Route("historico-campeonato")]
        [HttpGet]
        public async Task<ActionResult<Campeonato>> GetHistoricoCampeonato(string nomeCampeonato)
        {
            if (_campeonatoService.GetEntityByName(nomeCampeonato).Result == null)
                return StatusCode(500, $"Erro: O campeonato {nomeCampeonato} não existe");
            else if (_campeonatoService.ValidarCampeonato(nomeCampeonato))
            {
                List<PartidaTime> partidas = _campeonatoService.GetHistoricoCampeonato(nomeCampeonato).Result;
                return StatusCode(200, $"Resultado do campeonato: {partidas.ToJson()}");
            }
            else
                return StatusCode(500, $"Erro: O campeonato {nomeCampeonato} não foi realizado!");
        }

        [Route("pontuacao-time-campeonato")]
        [HttpGet]
        public async Task<ActionResult<Campeonato>> GetPontuacaoTimeCampeonato(string nomeCampeonato, string nomeTime)
        {
            if (_campeonatoService.GetEntityByName(nomeCampeonato).Result == null)
                return StatusCode(500, $"Erro: O campeonato {nomeCampeonato} não existe");
            else if (_campeonatoService.TimeCadastradoCampeonatoValidacao(nomeCampeonato, nomeTime))
            {
                CampeonatoTimeDto campeonatoTime = _campeonatoService.GetHistoricoTimeCampeonato(nomeCampeonato, nomeTime);
                return StatusCode(200, $"Resultado do Time:{campeonatoTime.ToJson()} ");
            }
            else
                return StatusCode(500, $"Erro: O campeonato {nomeCampeonato} não foi realizado!");
        }

        [Route("campeao-campeonato")]
        [HttpGet]
        public async Task<ActionResult<Campeonato>> getCampeao(string nomeCampeonato)
        {
            if (_campeonatoService.GetEntityByName(nomeCampeonato).Result == null)
                return StatusCode(500, $"Erro: O campeonato {nomeCampeonato} não existe");
            else if (_campeonatoService.GetCampeao(nomeCampeonato).Result != null)
            {
                var time = _campeonatoService.GetCampeao(nomeCampeonato).Result;
                return StatusCode(200, $"O time campeão foi :{time.nome}");
            }
            else
                return StatusCode(500, $"Erro: O campeonato {nomeCampeonato} não foi realizado!");
        }
    }
}

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

namespace CadastroDeCampeonato.Controllers
{
    [Route("api/time")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        private readonly ITimeRepository _timeRepository;

        public TimeController(ITimeRepository timeRepository)
        {
            _timeRepository = timeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Time>>> Gettime()
        {
            return await _timeRepository.List();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Time>> GetTime(int id)
        {
            var time = _timeRepository.GetEntityById(id).Result;

            if (time == null)
            {
                return NotFound();
            }

            return time;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTime(int id, Time time)
        {
            if (id != time.id)
            {
                return BadRequest();
            }

            try
            {
                _timeRepository.Update(time);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_timeRepository.GetEntityById(id) == null)
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
        public async Task<ActionResult<Time>> PostTime(Time time)
        {
            if (_timeRepository.GetEntityByName(time.nome) == null)
            {
                await _timeRepository.Add(time);
                return CreatedAtAction("GetTime", new { id = time.id }, time);
            }
            else
                return StatusCode(500, $"Erro: O time {time.nome} já existe!");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTime(int id)
        {
            var time = await _timeRepository.GetEntityById(id);
            if (time == null)
            {
                return NotFound();
            }
            _timeRepository.Delete(time);
            return NoContent();
        }
    }
}

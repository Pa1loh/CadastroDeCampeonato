using CadastroDeCampeonato.Models;
using CadastroDeCampeonato.Repository;
using CadastroDeCampeonato.Repository.Interfaces;
using CadastroDeCampeonato.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CadastroDeCampeonato.Services
{
    public class TimeService : ITimeService
    {
        private readonly ITimeRepository _timeRepository;

        public TimeService(ITimeRepository timeRepository)
        {
            _timeRepository = timeRepository;
        }

        public void Create(Time objeto)
        {
            try
            {
                _timeRepository.Add(objeto);

            }
            catch { }
        }

        public void Delete(Time objeto)
        {
            try
            {
                _timeRepository.Delete(objeto);

            }
            catch { }
        }

        public Task<Time> GetEntityById(int Id)
        {
            try
            {
                return _timeRepository.GetEntityById(Id);

            }
            catch { return null; }
        }
        public Task<Time> GetEntityByName(string name)
        {
            try
            {
                return _timeRepository.GetEntityByName(name);

            }
            catch { return null; }
        }

        public Task<List<Time>> List()
        {
            try
            {
                return _timeRepository.List();

            }
            catch { return null; }
        }

        public void Update(Time objeto)
        {
            try
            {
                _timeRepository.Update(objeto);

            }
            catch { }
        }
    }
}

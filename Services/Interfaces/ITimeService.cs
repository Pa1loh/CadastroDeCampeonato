using CadastroDeCampeonato.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeCampeonato.Services.Interfaces
{
    public interface ITimeService
    {
        void Create(Time objeto);
        void Update(Time objeto);
        void Delete(Time objeto);
        Task<Time> GetEntityById(int Id);
        Task<Time> GetEntityByName(string name);
        Task<List<Time>> List();
    }

}

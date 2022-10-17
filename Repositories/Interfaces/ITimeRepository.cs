using CadastroDeCampeonato.Models;
using CadastroDeCampeonato.Models.Dtos;
using CadastroDeCampeonato.Repositories.Interfaces;

namespace CadastroDeCampeonato.Repository.Interfaces
{
    public interface ITimeRepository : ICrudGenericRepository<Time>{
        Task<Time> GetEntityByName(string Name);
    }
}

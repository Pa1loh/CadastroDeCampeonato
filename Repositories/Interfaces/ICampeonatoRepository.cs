using CadastroDeCampeonato.Models;
using CadastroDeCampeonato.Repositories.Interfaces;

namespace CadastroDeCampeonato.Repository.Interfaces
{
    public interface ICampeonatoRepository : ICrudGenericRepository<Campeonato>
    {
        Task<Campeonato> GetEntityByName(string Name);

    }
}

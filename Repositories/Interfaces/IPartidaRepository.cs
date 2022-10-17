using CadastroDeCampeonato.Models;

namespace CadastroDeCampeonato.Repositories.Interfaces
{
    public interface IPartidaRepository : ICrudGenericRepository<Partida>
    {
        Task<List<Partida>> GetEntityByCampeonatoId(int id);
        Task<Partida> GetEntityByCampeonatoIdEStatus(int campeonatoId, int faseCampeonato);
    }
}

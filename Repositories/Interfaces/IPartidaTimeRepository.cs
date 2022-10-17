using CadastroDeCampeonato.Models;

namespace CadastroDeCampeonato.Repositories.Interfaces
{
    public interface IPartidaTimeRepository:ICrudGenericRepository<PartidaTime>{
        Task<List<PartidaTime>> GetEntityByPartidaList(List<Partida> partidas);
        int GetPontuacao(int timeId, int campeonatoId);
    }
}

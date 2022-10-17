using CadastroDeCampeonato.Models;

namespace CadastroDeCampeonato.Repositories.Interfaces
{
    public interface ICampeonatoTimeRepository : ICrudGenericRepository<CampeonatoTime> {
        Task<CampeonatoTime> GetEntityByIdTimeIdCampeonato(int timeId, int campeonatoId);
        Task<List<CampeonatoTime>> GetListTimesCampeonato(int idCampeonato);
    }
}

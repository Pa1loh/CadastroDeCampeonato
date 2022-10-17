using CadastroDeCampeonato.Data;
using CadastroDeCampeonato.Models;
using CadastroDeCampeonato.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeCampeonato.Repositories
{
    public class CampeonatoTimeRepository : ICampeonatoTimeRepository
    {
        private readonly DbContextOptions<ApiDbContext> _OptionsBuilder;

        public CampeonatoTimeRepository()
        {
            _OptionsBuilder = new DbContextOptions<ApiDbContext>();
        }

        public async Task Add(CampeonatoTime Objeto)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                await data.Set<CampeonatoTime>().AddAsync(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(CampeonatoTime Objeto)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                data.Set<CampeonatoTime>().Remove(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task<CampeonatoTime> GetEntityById(int Id)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                return await data.Set<CampeonatoTime>().FirstOrDefaultAsync(x => x.id == Id);
            }
        }

        public async Task<CampeonatoTime> GetEntityByIdTimeIdCampeonato(int timeId, int campeonatoId)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                return await data.Set<CampeonatoTime>().FirstOrDefaultAsync(x => x.timeId == timeId &&
                                                                            x.campeonatoId == campeonatoId);
            }
        }

        public async Task<List<CampeonatoTime>> GetListTimesCampeonato(int idCampeonato)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                return await data.Set<CampeonatoTime>().Where(x => x.campeonatoId == idCampeonato).ToListAsync();
            }
        }

        public async Task<List<CampeonatoTime>> List()
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                return await data.Set<CampeonatoTime>().ToListAsync();
            }
        }

        public async Task Update(CampeonatoTime Objeto)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                data.Set<CampeonatoTime>().Update(Objeto);
                await data.SaveChangesAsync();
            }
        }
    }
}


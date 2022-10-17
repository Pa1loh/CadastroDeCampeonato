using CadastroDeCampeonato.Data;
using CadastroDeCampeonato.Models;
using CadastroDeCampeonato.Models.Enums;
using CadastroDeCampeonato.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeCampeonato.Repositories
{
    public class PartidaRepository : IPartidaRepository
    {
        private readonly DbContextOptions<ApiDbContext> _OptionsBuilder;

        public PartidaRepository()
        {
            _OptionsBuilder = new DbContextOptions<ApiDbContext>();
        }

        public async Task Add(Partida Objeto)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                data.Set<Partida>().Add(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(Partida Objeto)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                data.Set<Partida>().Remove(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task<List<Partida>> GetEntityByCampeonatoId(int campeonatoId)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                return await data.Set<Partida>().Where(x => x.campeonatoId == campeonatoId).ToListAsync();
            }
        }

        public async Task<Partida> GetEntityByCampeonatoIdEStatus(int campeonatoId, int faseCampeonato)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                return await data.Set<Partida>().FirstOrDefaultAsync(x => x.campeonatoId == campeonatoId &&
                                                                    x.fasePartida == (FasePartida)3);
            }
        }

        public async Task<Partida> GetEntityById(int Id)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                return await data.Set<Partida>().FirstOrDefaultAsync(x => x.id == Id);
            }
        }

        public async Task<List<Partida>> List()
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                return await data.Set<Partida>().ToListAsync();
            }
        }

        public async Task Update(Partida Objeto)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                data.Set<Partida>().Update(Objeto);
                await data.SaveChangesAsync();
            }
        }
    }
}

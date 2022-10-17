using CadastroDeCampeonato.Data;
using CadastroDeCampeonato.Models;
using CadastroDeCampeonato.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeCampeonato.Repositories
{
    public class PartidaTimeRepository : IPartidaTimeRepository
    {
        private readonly DbContextOptions<ApiDbContext> _OptionsBuilder;

        public PartidaTimeRepository()
        {
            _OptionsBuilder = new DbContextOptions<ApiDbContext>();
        }
        public async Task Add(PartidaTime Objeto)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                await data.Set<PartidaTime>().AddAsync(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(PartidaTime Objeto)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                data.Set<PartidaTime>().Remove(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task<PartidaTime> GetEntityById(int Id)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                return await data.Set<PartidaTime>().FirstOrDefaultAsync(x => x.id == Id);
            }
        }

        public async Task<List<PartidaTime>> GetEntityByPartidaList(List<Partida> partidas)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                List<PartidaTime> partidasTimes = new List<PartidaTime>();
                foreach (Partida partida in partidas)
                {
                    var partidasTimeAdd = data.Set<PartidaTime>().Where(x => x.partidaId == partida.id).ToListAsync().Result;
                    if (partidasTimeAdd != null)
                        foreach (var partidaTime in partidasTimeAdd)
                        {
                            partidasTimes.Add(partidaTime);
                        }

                }
                return partidasTimes;
            }
        }

        public  int GetPontuacao(int timeId, int campeonatoId)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                return  data.Set<PartidaTime>().Where(x => x.timeId == timeId &&
                                                           x.CampeonatoId == campeonatoId).Sum(i => i.golsFeitos);              
            }
        }

        public async Task<List<PartidaTime>> List()
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                return await data.Set<PartidaTime>().ToListAsync();
            }
        }

        public async Task Update(PartidaTime Objeto)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                data.Set<PartidaTime>().Update(Objeto);
                await data.SaveChangesAsync();
            }
        }
    }
}

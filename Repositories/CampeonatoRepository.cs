using CadastroDeCampeonato.Data;
using CadastroDeCampeonato.Models;
using CadastroDeCampeonato.Repositories.Interfaces;
using CadastroDeCampeonato.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CadastroDeCampeonato.Repository
{
    public class CampeonatoRepository : ICampeonatoRepository
    {
        private readonly DbContextOptions<ApiDbContext> _OptionsBuilder;

        public CampeonatoRepository()
        {
            _OptionsBuilder = new DbContextOptions<ApiDbContext>();
        }

        public async Task Add(Campeonato Objeto)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                await data.Set<Campeonato>().AddAsync(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(Campeonato Objeto)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                data.Set<Campeonato>().Remove(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task<Campeonato> GetEntityById(int Id)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                return await data.Set<Campeonato>().FirstOrDefaultAsync(x => x.id == Id);
            }
        }

        public async Task<Campeonato> GetEntityByName(string Name)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                return await data.Set<Campeonato>().FirstOrDefaultAsync(x => x.nome == Name);
            }
        }

        public async Task<List<Campeonato>> List()
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                return await data.Set<Campeonato>().ToListAsync();
            }
        }

        public async Task Update(Campeonato Objeto)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                data.Set<Campeonato>().Update(Objeto);
                await data.SaveChangesAsync();
            }
        }
    }
}

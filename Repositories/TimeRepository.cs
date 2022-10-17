using CadastroDeCampeonato.Data;
using CadastroDeCampeonato.Models;
using CadastroDeCampeonato.Models.Dtos;
using CadastroDeCampeonato.Repositories.Interfaces;
using CadastroDeCampeonato.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace CadastroDeCampeonato.Repository
{
    public class TimeRepository : ITimeRepository
    {
        private readonly DbContextOptions<ApiDbContext> _OptionsBuilder;

        public TimeRepository()
        {
            _OptionsBuilder = new DbContextOptions<ApiDbContext>();
        }

        public async Task Add(Time Objeto)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                await data.Set<Time>().AddAsync(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(Time Objeto)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                data.Set<Time>().Remove(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task<Time> GetEntityById(int Id)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                return await data.Set<Time>().FirstOrDefaultAsync(x => x.id == Id);
            }
        }

        public async Task<Time> GetEntityByName(string Name)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                return await data.Set<Time>().FirstOrDefaultAsync(x => x.nome == Name);
            }
        }

        public async Task<List<Time>> List()
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                return await data.Set<Time>().ToListAsync();
            }
        }

        public async Task Update(Time Objeto)
        {
            using (var data = new ApiDbContext(_OptionsBuilder))
            {
                data.Set<Time>().Update(Objeto);
                await data.SaveChangesAsync();
            }
        }
    }
}

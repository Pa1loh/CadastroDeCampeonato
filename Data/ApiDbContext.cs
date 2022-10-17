using CadastroDeCampeonato.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeCampeonato.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Campeonato> campeonato { get; set; }
        public DbSet<Time> time { get; set; }
        public DbSet<Partida> partida { get; set; }
        public DbSet<PartidaTime> partidaTime { get; set; }
        public DbSet<CampeonatoTime> campeonatoTime { get; set; }


        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .Build();

            optionsBuilder.UseSqlite(configuration.GetConnectionString("DbConnection"));
        }


    }


}

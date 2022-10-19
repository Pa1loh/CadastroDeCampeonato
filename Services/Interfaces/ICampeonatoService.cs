using CadastroDeCampeonato.Models;
using CadastroDeCampeonato.Models.Dtos;

namespace CadastroDeCampeonato.Services.Interfaces
{
    public interface ICampeonatoService
    {
        void Create(Campeonato objeto);
        void Update(Campeonato objeto);
        void Delete(Campeonato objeto);
        Task<Campeonato> GetEntityById(int Id);
        Task<CampeonatoTime> GetCampeonatoTimeEntityById(int id);
        Task<Campeonato> GetEntityByName(string name);
        Task<List<Campeonato>> List();
        void CadastrarTime(CampeonatoTimeDto objeto);
        bool ValidarCampeonato(string nomeCampeonato);
        Task<List<PartidaTime>> GerarCampeonato(string nomeCampeonato);
        List<int> SortearTimes(List<int> idsTime);
        public Task<List<PartidaTime>> GetHistoricoCampeonato(string nomeCampeonato);
        bool TimeCadastradoCampeonatoValidacao(string nomeCampeonato, string nomeTime);
        CampeonatoTimeDto GetHistoricoTimeCampeonato(string nomeCampeonato, string nomeTime);
        bool ValidarTimeCadastrado(CampeonatoTimeDto campeonatoTime);
        bool ValidarTimeExistente(string nomeTime);
        Task<Time> GetCampeao(string nomeCampeonato);
        bool ValidarCampeonatoExistente(string nomeCampeonato);
    }
}

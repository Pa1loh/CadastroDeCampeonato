using CadastroDeCampeonato.Models;
using CadastroDeCampeonato.Models.Dtos;
using CadastroDeCampeonato.Models.Enums;
using CadastroDeCampeonato.Repositories;
using CadastroDeCampeonato.Repositories.Interfaces;
using CadastroDeCampeonato.Repository;
using CadastroDeCampeonato.Repository.Interfaces;
using CadastroDeCampeonato.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Reflection.Metadata.Ecma335;

namespace CadastroDeCampeonato.Services
{
    public class CampeonatoService : ICampeonatoService
    {
        private readonly ICampeonatoRepository _campeonatoRepository;
        private readonly ITimeService _timeService;
        private readonly ICampeonatoTimeRepository _campeonatoTimeRepository;
        private readonly IPartidaTimeRepository _partidaTimeRepository;
        private readonly IPartidaRepository _partidaRepository;

        public CampeonatoService(ICampeonatoRepository campeonatoRepository,
                                 ITimeService timeService,
                                 ICampeonatoTimeRepository campeonatoTimeRepository,
                                 IPartidaTimeRepository partidaTimeRepository,
                                 IPartidaRepository partidaRepository)
        {
            _campeonatoRepository = campeonatoRepository;
            _timeService = timeService;
            _campeonatoTimeRepository = campeonatoTimeRepository;
            _partidaTimeRepository = partidaTimeRepository;
            _partidaRepository = partidaRepository;
        }

        public void CadastrarTime(CampeonatoTimeDto objeto)
        {
            if (CampeonatoTimeExiste(objeto))
            {
                objeto = BuscarIdsCampeonatoTime(objeto);
                CampeonatoTime campeonatoTime = new CampeonatoTime()
                {
                    campeonatoId = objeto.idCampeonato,
                    timeId = objeto.idTime
                };
                try
                {
                    _campeonatoTimeRepository.Add(campeonatoTime);
                }
                catch { throw; }
            }
        }
        public void Create(Campeonato objeto)
        {
            try
            {
                _campeonatoRepository.Add(objeto);

            }
            catch { }
        }

        public void Delete(Campeonato objeto)
        {
            try
            {
                _campeonatoRepository.Delete(objeto);

            }
            catch { }
        }

        public Task<List<PartidaTime>> GerarCampeonato(string nomeCampeonato)
        {
            try
            {
                var campeonato = _campeonatoRepository.GetEntityByName(nomeCampeonato).Result;

                var campeonatoTime = _campeonatoTimeRepository.GetListTimesCampeonato(campeonato.id);
                List<int> idsTime = new List<int>();

                foreach (var time in campeonatoTime.Result)
                {
                    idsTime.Add(time.timeId);

                }

                idsTime = SortearTimes(idsTime);
                ExecutarRodadas(idsTime, campeonato.id);

            }
            catch { }

            return GetHistoricoCampeonato(nomeCampeonato);
        }

        public Task<List<PartidaTime>> GetHistoricoCampeonato(string nomeCampeonato)
        {
            int idCampeonato = GetEntityByName(nomeCampeonato).Result.id;
            List<Partida> partidasCampeonato = _partidaRepository.GetEntityByCampeonatoId(idCampeonato).Result;
            return _partidaTimeRepository.GetEntityByPartidaList(partidasCampeonato);
        }

        public Task<Campeonato> GetEntityById(int Id)
        {
            try
            {
                return _campeonatoRepository.GetEntityById(Id);

            }
            catch { return null; }
        }

        public Task<List<Campeonato>> List()
        {
            try
            {
                return _campeonatoRepository.List();

            }
            catch { return null; }
        }

        public bool ValidarCampeonato(string nomeCampeonato)
        {
            var campeonato = _campeonatoRepository.GetEntityByName(nomeCampeonato);
            var campeonatoTime = _campeonatoTimeRepository.GetListTimesCampeonato(campeonato.Result.id);
            var partida = _partidaRepository.GetEntityByCampeonatoId(campeonato.Result.id).Result;
            if (campeonatoTime.Result.Count() == 8 && partida.Count == 0)
                return true;
            else
                return false;

        }

        public void Update(Campeonato objeto)
        {
            try
            {
                _campeonatoRepository.Update(objeto);

            }
            catch { }
        }
        private bool CampeonatoTimeExiste(CampeonatoTimeDto objeto)
        {
            if ((_timeService.GetEntityByName(objeto.NomeTime) != null) &&
                (_campeonatoRepository.GetEntityByName(objeto.NomeTime) != null))
                return true;
            else
                return false;
        }
        private CampeonatoTimeDto BuscarIdsCampeonatoTime(CampeonatoTimeDto objeto)
        {
            var campeonato = _campeonatoRepository.GetEntityByName(objeto.NomeCampeonato);
            var time = _timeService.GetEntityByName(objeto.NomeTime);

            objeto.idTime = time.Result.id;
            objeto.idCampeonato = campeonato.Result.id;
            return objeto;

        }

        public Task<Campeonato> GetEntityByName(string name)
        {
            return _campeonatoRepository.GetEntityByName(name);
        }

        public List<int> SortearTimes(List<int> idsTime)
        {

            List<int> idsTimeAleatorio = new List<int>();
            Random numeroAleatorioGerado = new Random();
            int IndexRepeticao = idsTime.Count();

            for (int i = 0; i < IndexRepeticao; i++)
            {
                int numeroAleatorio = numeroAleatorioGerado.Next(idsTime.Count());
                idsTimeAleatorio.Add(idsTime[numeroAleatorio]);
                idsTime.Remove(idsTime[numeroAleatorio]);
            }
            return idsTimeAleatorio;

        }

        public async Task ExecutarRodadas(List<int> idsTime, int campeonatoId)
        {
            foreach (int fasePartida in Enum.GetValues(typeof(FasePartida)))
            {

                idsTime = await ExecutarPartida(idsTime, campeonatoId, fasePartida);

            }
        }

        private async Task<List<int>> ExecutarPartida(List<int> idsTime, int campeonatoId, int indexFasePartida)
        {
            FasePartida fasePartida = (FasePartida)indexFasePartida;
            List<int> idsTimeVencedores = new List<int>();
            List<int> idsTimesPartidas = new List<int>();
            int numeroPartidas = 0;
            int partidaId = 0;

            foreach (var timeId in idsTime)
            {
                if (novaPartida(numeroPartidas))
                {
                    Partida partida = new Partida()
                    {
                        campeonatoId = campeonatoId,
                        fasePartida = fasePartida
                    };
                    await _partidaRepository.Add(partida);
                    partidaId = partida.id;
                }

                PartidaTime partidaTime = new PartidaTime()
                {
                    partidaId = partidaId,
                    timeId = timeId,
                    CampeonatoId = campeonatoId

                };

                await _partidaTimeRepository.Add(partidaTime);

                idsTimesPartidas.Add(partidaTime.id);
                if (idsTimesPartidas.Count == 2)
                {
                    idsTimeVencedores.Add(VencedorDaPartida(idsTimesPartidas));
                    idsTimesPartidas = new List<int>();
                }

                numeroPartidas++;
            }
            return idsTimeVencedores;

        }
        private int VencedorDaPartida(List<int> idsTimesPartidas)
        {
            Random golsAleatoriosGerados = new Random();

            foreach (int id in idsTimesPartidas)
            {
                var partidaTime = _partidaTimeRepository.GetEntityById(id);
                partidaTime.Result.golsFeitos = golsAleatoriosGerados.Next(8);
                _partidaTimeRepository.Update(partidaTime.Result);
            }
            if (ValidarVencedor(idsTimesPartidas))
                return GetVencedorPartida(idsTimesPartidas);
            else
                return ExecutarPenaltis(idsTimesPartidas);
        }

        private int ExecutarPenaltis(List<int> idsTimesPartidas)
        {
            Random penaltisGerados = new Random();
            int[] golsDePenalti = new int[2];
            bool temVencedor = false;
            int contador = 0;

            do
            {
                golsDePenalti[0] += penaltisGerados.Next(5);
                golsDePenalti[1] += penaltisGerados.Next(5);
                if (golsDePenalti[0] != golsDePenalti[1])
                    temVencedor = true;

            } while (temVencedor != true);


            foreach (int id in idsTimesPartidas)
            {
                PartidaTime partidaUpdate = _partidaTimeRepository.GetEntityById(id).Result;
                partidaUpdate.penaltisFeitos = golsDePenalti[contador];
                _partidaTimeRepository.Update(partidaUpdate);
                contador++;
            }
            int idTime1 = _partidaTimeRepository.GetEntityById(idsTimesPartidas.FirstOrDefault()).Result.timeId;
            int idTime2 = _partidaTimeRepository.GetEntityById(idsTimesPartidas.Last()).Result.timeId;

            if (golsDePenalti[0] > golsDePenalti[1])
                return idTime1;
            else
                return idTime2;
        }

        private int GetVencedorPartida(List<int> idsTimesPartidas)
        {
            PartidaTime Time1, Time2;

            Time1 = _partidaTimeRepository.GetEntityById(idsTimesPartidas.FirstOrDefault()).Result;
            Time2 = _partidaTimeRepository.GetEntityById(idsTimesPartidas.FirstOrDefault()).Result;

            if (Time1.golsFeitos > Time2.golsFeitos)
                return Time1.timeId;
            else
                return Time2.timeId;
        }
        private bool ValidarVencedor(List<int> idsTimesPartidas)
        {
            int golsTime1, golsTime2 = 0;

            golsTime1 = _partidaTimeRepository.GetEntityById(idsTimesPartidas.FirstOrDefault()).Result.golsFeitos;
            golsTime2 = _partidaTimeRepository.GetEntityById(idsTimesPartidas.Last()).Result.golsFeitos;

            if (golsTime1 == golsTime2)
                return false;
            else
                return true;
        }
        private bool novaPartida(int numeroPartidas)
        {
            if (numeroPartidas == 0 || numeroPartidas % 2 == 0)
                return true;
            else
                return false;
        }
        public Task<CampeonatoTime> GetCampeonatoTimeEntityById(int id)
        {
            try
            {
                return _campeonatoTimeRepository.GetEntityById(id);

            }
            catch { return null; }
        }

        public bool TimeCadastradoCampeonatoValidacao(string nomeCampeonato, string nomeTime)
        {
            Time time = _timeService.GetEntityByName(nomeTime).Result;
            Campeonato campeonato = _campeonatoRepository.GetEntityByName(nomeCampeonato).Result;

            if (time != null && campeonato != null)
            {
                CampeonatoTime campeonatoTime = _campeonatoTimeRepository.GetEntityByIdTimeIdCampeonato(time.id, campeonato.id).Result;
                if (campeonatoTime != null)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public CampeonatoTimeDto GetHistoricoTimeCampeonato(string nomeCampeonato, string nomeTime)
        {

            Time time = _timeService.GetEntityByName(nomeTime).Result;
            Campeonato campeonato = _campeonatoRepository.GetEntityByName(nomeCampeonato).Result;
            CampeonatoTimeDto campeonatoTimeRetorno = new CampeonatoTimeDto();

            if (time != null && campeonato != null)
            {
                CampeonatoTime campeonatoTime = _campeonatoTimeRepository.GetEntityByIdTimeIdCampeonato(time.id, campeonato.id).Result;
                if (campeonatoTime != null)
                {
                    campeonatoTimeRetorno.idCampeonato = campeonatoTime.campeonatoId;
                    campeonatoTimeRetorno.idTime = time.id;
                    campeonatoTimeRetorno.NomeTime = time.nome;
                    campeonatoTimeRetorno.NomeCampeonato = campeonato.nome;
                    campeonatoTimeRetorno.Pontos = _partidaTimeRepository.GetPontuacao(time.id, campeonato.id);

                }

                return campeonatoTimeRetorno;
            }
            return campeonatoTimeRetorno;
        }

        public bool ValidarTimeCadastrado(CampeonatoTimeDto campeonatoTime)
        {
            Time time = _timeService.GetEntityByName(campeonatoTime.NomeTime).Result;
            Campeonato campeonato = GetEntityByName(campeonatoTime.NomeCampeonato).Result;
            if (time == null || campeonato == null)
                return false;
            var campeonatoTimeEncontrado = _campeonatoTimeRepository.GetEntityByIdTimeIdCampeonato(time.id,
                                                                                                   campeonato.id);

            if (campeonatoTimeEncontrado.Result == null)
                return false;
            else
                return true;
        }

        public bool ValidarTimeExistente(string nomeTime)
        {
            if (_timeService.GetEntityByName(nomeTime).Result == null)
                return false;
            else
                return true;
        }

        public Task<Time> GetCampeao(string nomeCampeonato)
        {
            List<Partida> partidas = new List<Partida>();
            // pegar id da partida status 3 e campeonato id
            var campeonato = _campeonatoRepository.GetEntityByName(nomeCampeonato).Result;
            partidas.Add(_partidaRepository.GetEntityByCampeonatoIdEStatus(campeonato.id, 3).Result);
            var partidaTime = _partidaTimeRepository.GetEntityByPartidaList(partidas).Result;
            int golTimeCampeao = partidaTime.Max(x => x.golsFeitos);
            var partidaTimeVencedora = partidaTime.FirstOrDefault(x => x.golsFeitos == golTimeCampeao);
            return _timeService.GetEntityById(partidaTimeVencedora.timeId);
        }
    }
}

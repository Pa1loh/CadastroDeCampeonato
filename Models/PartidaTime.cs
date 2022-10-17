using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroDeCampeonato.Models
{
    public class PartidaTime
    {
        public int id { get; set; }
        [ForeignKey("Time")]
        [Required(ErrorMessage = "Id do Time é obrigatório")]
        public int timeId { get; set; }
        public virtual Time time { get; set; }
        [ForeignKey("Partida")]
        [Required(ErrorMessage = "Id da Partida")]
        public int partidaId { get; set; }
        public virtual Partida Partida { get; set; }
        public int golsFeitos { get; set; }
        public int penaltisFeitos { get; set; }
        [ForeignKey("Campeonato")]
        [Required(ErrorMessage = "Id do Time é obrigatório")]
        public int CampeonatoId { get; set; }
        public virtual Campeonato campeonato { get; set; }

    }
}

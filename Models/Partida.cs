using CadastroDeCampeonato.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroDeCampeonato.Models
{
    public class Partida
    {
        public int id { get; set; }
        [ForeignKey("Campeonato")]
        [Required(ErrorMessage = "Id do Campeonato é obrigatório")]
        public int campeonatoId { get; set; }
        public virtual Campeonato campeonato { get; set; }
        public FasePartida fasePartida { get; set; }

    }
}

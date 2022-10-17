using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroDeCampeonato.Models
{
    public class CampeonatoTime
    {
        public int id { get; set; }
        [ForeignKey("Campeonato")]
        [Required(ErrorMessage = "Id do Campeonato é obrigatório")]
        public int campeonatoId { get; set; }
        public virtual Campeonato campeonato { get; set; }
        [ForeignKey("Time")]
        [Required(ErrorMessage = "Id do Time é obrigatório")]
        public int timeId { get; set; }
        public virtual Time time { get; set; }
    }
}

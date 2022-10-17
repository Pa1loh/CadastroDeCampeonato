using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroDeCampeonato.Models
{
    public class Campeonato
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "O nome do campeonato é obrigatório", AllowEmptyStrings = false)]
        public string nome { get; set; }
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime inicio { get; set; }
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime fim { get; set; }

    }
}

using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeCampeonato.Models
{
    public class Time
    {
        public int id { get; set; }
        [Required]
        public string nome { get; set; }

    }
}

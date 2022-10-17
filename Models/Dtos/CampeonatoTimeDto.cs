namespace CadastroDeCampeonato.Models.Dtos
{
    public class CampeonatoTimeDto
    {
        public int id { get; set; }
        public int idCampeonato { get; set; }
        public int idTime { get; set; }
        public string? NomeCampeonato { get; set; }
        public string? NomeTime { get; set; }
        public int Pontos { get; set; }

    }
}

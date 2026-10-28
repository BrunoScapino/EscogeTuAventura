namespace PresentacionLectura.Models
{
    public class HistoriaApiDTO
    {
        public int Id { get; set; }

        public int CapituloInicialId { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Sinopsis { get; set; } = string.Empty;

        public List<int> CategoriasIds { get; set; } = new List<int>();
    }
}
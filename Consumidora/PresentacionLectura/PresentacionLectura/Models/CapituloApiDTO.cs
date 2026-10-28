namespace PresentacionLectura.Models
{
    public class CapituloApiDTO
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Texto { get; set; } = string.Empty;

        public bool EsFinal { get; set; }
    }
}
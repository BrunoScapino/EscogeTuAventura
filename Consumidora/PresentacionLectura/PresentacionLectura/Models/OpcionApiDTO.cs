namespace PresentacionLectura.Models
{
    public class OpcionApiDTO
    {
        public int Id { get; set; }

        public string Texto { get; set; } = string.Empty;

        public int CapituloDestinoId { get; set; }
    }
}
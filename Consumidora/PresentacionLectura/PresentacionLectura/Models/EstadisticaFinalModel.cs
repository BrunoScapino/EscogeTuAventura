namespace PresentacionLectura.Models
{
    public class EstadisticaFinalModel
    {
        public int IdHistoria { get; set; }

        public string TituloHistoria { get; set; } = string.Empty;

        public int IdFinal { get; set; }

        public string TituloFinal { get; set; } = string.Empty;

        public int VecesAlcanzado { get; set; }

        public bool EsFinalMasAlcanzado { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using EscogeTuAventura.InterfacesDelDominio;
using System.Collections.Generic;
using EscogeTuAventura.Excepciones;


namespace EscogeTuAventura.Entidades
{
    public class Historia : IValidable
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Sinopsis { get; set; } = string.Empty;

        public EstadoHistoria Estado { get; set; }

        public List<Categoria> Categorias { get; set; } = new List<Categoria>();

        public CapituloIntermedio CapituloInicial { get; set; } = null!;

        public List<Capitulo> Capitulos { get; set; } = new List<Capitulo>();

        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Titulo))
                throw new HistoriaException("El título es requerido.");

            if (string.IsNullOrWhiteSpace(Sinopsis))
                throw new HistoriaException("La sinopsis es requerida.");

            if (Categorias == null || Categorias.Count == 0)
                throw new HistoriaException("La historia debe tener al menos una categoría.");

            if (CapituloInicial == null)
                throw new HistoriaException("La historia debe tener un capítulo inicial.");
        }

        public void ValidarPublicacion()
        {
            Validar();

            if (!Capitulos.Contains(CapituloInicial))
            {
                throw new HistoriaException(
                    "El capítulo inicial debe formar parte de la historia.");
            }

            foreach (Capitulo capitulo in Capitulos)
            {
                capitulo.Validar();

                if (capitulo.Historia != this)
                {
                    throw new HistoriaException(
                        "Todos los capítulos deben pertenecer a esta historia.");
                }

                if (capitulo is CapituloIntermedio intermedio)
                {
                    if (intermedio.Opciones.Count == 0)
                    {
                        throw new HistoriaException(
                            $"El capítulo intermedio '{intermedio.Titulo}' debe tener al menos una opción.");
                    }

                    foreach (Opcion opcion in intermedio.Opciones)
                    {
                        opcion.Validar();

                        if (opcion.CapituloOrigen != intermedio ||
                            !Capitulos.Contains(opcion.CapituloDestino))
                        {
                            throw new HistoriaException(
                                "Las opciones deben conectar capítulos de la misma historia.");
                        }
                    }

                }
            }
            foreach (Capitulo capitulo in Capitulos)
            {
                if (capitulo is CapituloIntermedio)
                {
                    bool puedeLlegarAUnFinal =
                        PuedeLlegarAUnFinal(
                            capitulo,
                            new HashSet<Capitulo>());

                    if (!puedeLlegarAUnFinal)
                    {
                        throw new HistoriaException(
                            $"El capítulo '{capitulo.Titulo}' no tiene ningún camino hacia un final.");
                    }
                }
            }
        }
        private bool PuedeLlegarAUnFinal(
        Capitulo capitulo,
        HashSet<Capitulo> capitulosVisitados)
        {
            if (capitulo is CapituloFinal)
            {
                return true;
            }

            if (capitulo is not CapituloIntermedio capituloIntermedio)
            {
                return false;
            }

            if (capitulosVisitados.Contains(capitulo))
            {
                return false;
            }

            capitulosVisitados.Add(capitulo);

            foreach (Opcion opcion in capituloIntermedio.Opciones)
            {
                HashSet<Capitulo> visitadosDeEstaOpcion =
                    new HashSet<Capitulo>(capitulosVisitados);

                if (PuedeLlegarAUnFinal(
                    opcion.CapituloDestino,
                    visitadosDeEstaOpcion))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
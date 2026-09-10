using System;
using System.Collections.Generic;
using System.Text;
using EscogeTuAventura.Excepciones;


namespace EscogeTuAventura.Entidades
{
    public class CapituloFinal : Capitulo
    {
        public int VecesAlcanzado { get; set; }

        public override void Validar()
        {
            if (string.IsNullOrWhiteSpace(Titulo))
                throw new CapituloException("El título del capítulo es requerido.");

            if (string.IsNullOrWhiteSpace(Texto))
                throw new CapituloException("El texto del capítulo es requerido.");

            if (VecesAlcanzado < 0)
                throw new CapituloException("La cantidad de veces alcanzado no puede ser negativa.");
        }
    }
}
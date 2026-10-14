using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;

namespace LogicaAplicacion.InterfacesDeCasoDeUso.Capitulos
{
    public interface IAgregarCapituloCU
    {
        void Agregar(CapituloDTO capitulo, int administradorId);
    }
}
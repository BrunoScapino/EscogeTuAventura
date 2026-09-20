using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;

namespace LogicaAplicacion.InterfacesDeCasoDeUso.Historias
{
    public interface IAgregarHistoriaCU
    {
        void Agregar(HistoriaDTO historia);
    }
}
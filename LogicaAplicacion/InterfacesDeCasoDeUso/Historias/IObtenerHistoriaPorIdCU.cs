using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;

namespace LogicaAplicacion.InterfacesDeCasoDeUso.Historias
{
    public interface IObtenerHistoriaPorIdCU
    {
        HistoriaDTO ObtenerPorId(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;

namespace LogicaAplicacion.InterfacesDeCasoDeUso.Capitulos
{
    public interface IEncontrarCapitulosPorHistoriaCU
    {
        List<CapituloDTO> EncontrarPorHistoria(int historiaId);
    }
}
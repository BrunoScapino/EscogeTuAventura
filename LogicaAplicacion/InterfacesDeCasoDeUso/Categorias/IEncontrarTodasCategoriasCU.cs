using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;
using System.Collections.Generic;

namespace LogicaAplicacion.InterfacesDeCasoDeUso.Categorias
{
    public interface IEncontrarTodasCategoriasCU
    {
        List<CategoriaDTO> EncontrarTodas();
    }
}
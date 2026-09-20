using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;

namespace LogicaAplicacion.InterfacesDeCasoDeUso.Categorias
{
    public interface IAgregarCategoriaCU
    {
        void Agregar(CategoriaDTO categoria);
    }
}
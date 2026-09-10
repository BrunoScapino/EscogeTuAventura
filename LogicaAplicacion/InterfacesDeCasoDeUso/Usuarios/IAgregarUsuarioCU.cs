using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;

namespace LogicaAplicacion.InterfacesDeCasoDeUso.Usuarios
{
    public interface IAgregarUsuarioCU
    {
        void Agregar(UsuarioDTO usuario);
    }
}
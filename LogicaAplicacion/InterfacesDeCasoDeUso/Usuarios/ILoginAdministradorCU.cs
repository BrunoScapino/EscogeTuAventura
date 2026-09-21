using System;
using System.Collections.Generic;
using System.Text;
using DTOs.DTOs;

namespace LogicaAplicacion.InterfacesDeCasoDeUso.Usuarios
{
    public interface ILoginAdministradorCU
    {
        UsuarioDTO Login(string nombreUsuario, string password);
    }
}
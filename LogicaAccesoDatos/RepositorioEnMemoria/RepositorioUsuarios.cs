using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using EscogeTuAventura.Entidades;
using EscogeTuAventura.InterfacesDeRepositorio;

namespace LogicaAccesoDatos.RepositorioEnMemoria
{
    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        private static List<Usuario> usuarios = new List<Usuario>();

        public void Add(Usuario nuevo)
        {
            nuevo.Validar();
            usuarios.Add(nuevo);
        }

        public Usuario FindById(int id)
        {
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.Id == id)
                    return usuario;
            }

            return null;
        }

        public List<Usuario> FindAll()
        {
            return new List<Usuario>(usuarios);
        }

        public Usuario Login(string nombreUsuario, string password)
        {
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.NombreUsuario == nombreUsuario &&
                    usuario.Password.Verify(password))
                {
                    return usuario;
                }
            }

            return null;
        }

        public void Update(Usuario obj)
        {
            // Todavía no es necesario para los requerimientos actuales.
        }

        public void Remove(int id)
        {
            // Todavía no es necesario para los requerimientos actuales.
        }
    }
}
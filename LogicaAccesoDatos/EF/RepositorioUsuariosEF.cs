using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDeRepositorio;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioUsuariosEF : IRepositorioUsuarios
    {
        private EscogeTuAventuraContext contexto;

        public RepositorioUsuariosEF(EscogeTuAventuraContext contexto)
        {
            this.contexto = contexto;
        }

        public void Add(Usuario nuevo)
        {
            nuevo.Validar();

            bool existe = contexto.Usuarios
                .Any(u => u.NombreUsuario == nuevo.NombreUsuario);

            if (existe)
            {
                throw new UsuarioException(
                    "Ya existe un usuario con ese nombre de usuario.");
            }

            contexto.Usuarios.Add(nuevo);
            contexto.SaveChanges();
        }

        public List<Usuario> FindAll()
        {
            return contexto.Usuarios.ToList();
        }

        public Usuario FindById(int id)
        {
            Usuario? usuario = contexto.Usuarios
                .FirstOrDefault(u => u.Id == id);

            if (usuario == null)
            {
                throw new UsuarioException("No se encontró el usuario.");
            }

            return usuario;
        }

        public Usuario Login(string nombreUsuario, string password)
        {
            Usuario? usuario = contexto.Usuarios
                .FirstOrDefault(u =>
                    u.NombreUsuario == nombreUsuario &&
                    u.Password.Value == password);

            if (usuario == null)
            {
                throw new UsuarioException(
                    "Usuario o contraseña incorrectos.");
            }

            return usuario;
        }

        public void Update(Usuario nuevo)
        {
            nuevo.Validar();

            Usuario existente = FindById(nuevo.Id);

            bool nombreOcupado = contexto.Usuarios.Any(u =>
                u.NombreUsuario == nuevo.NombreUsuario &&
                u.Id != nuevo.Id);

            if (nombreOcupado)
            {
                throw new UsuarioException(
                    "Ya existe un usuario con ese nombre de usuario.");
            }

            existente.NombreCompleto = nuevo.NombreCompleto;
            existente.Email = nuevo.Email;
            existente.NombreUsuario = nuevo.NombreUsuario;
            existente.Password = nuevo.Password;
            existente.Rol = nuevo.Rol;

            contexto.SaveChanges();
        }

        public void Remove(int id)
        {
            Usuario usuario = FindById(id);

            contexto.Usuarios.Remove(usuario);
            contexto.SaveChanges();
        }
    }
}
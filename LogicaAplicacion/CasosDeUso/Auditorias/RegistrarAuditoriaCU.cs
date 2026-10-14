using EscogeTuAventura.Entidades;
using EscogeTuAventura.Excepciones;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.InterfacesDeCasoDeUso.Auditorias;

namespace LogicaAplicacion.CasosDeUso.Auditorias
{
    public class RegistrarAuditoriaCU : IRegistrarAuditoriaCU
    {
        private IRepositorioAuditorias repositorioAuditorias;
        private IRepositorioHistorias repositorioHistorias;
        private IRepositorioUsuarios repositorioUsuarios;

        public RegistrarAuditoriaCU(
            IRepositorioAuditorias repositorioAuditorias,
            IRepositorioHistorias repositorioHistorias,
            IRepositorioUsuarios repositorioUsuarios)
        {
            this.repositorioAuditorias = repositorioAuditorias;
            this.repositorioHistorias = repositorioHistorias;
            this.repositorioUsuarios = repositorioUsuarios;
        }

        public void Registrar(
            int historiaId,
            int administradorId,
            AccionAuditoria accion)
        {
            Historia historia =
                repositorioHistorias.FindById(historiaId);

            Usuario administrador =
                repositorioUsuarios.FindById(administradorId);

            if (administrador.Rol != Rol.Administrador)
            {
                throw new AuditoriaException(
                    "El usuario indicado no es administrador.");
            }

            Auditoria nuevaAuditoria = new Auditoria
            {
                Accion = accion,
                Fecha = DateTime.Now,
                Historia = historia,
                Administrador = administrador
            };

            repositorioAuditorias.Add(nuevaAuditoria);
        }
    }
}
using EscogeTuAventura.Entidades;

namespace LogicaAplicacion.InterfacesDeCasoDeUso.Auditorias
{
    public interface IRegistrarAuditoriaCU
    {
        void Registrar( int historiaId, int administradorId, AccionAuditoria accion);
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using EscogeTuAventura.InterfacesDelDominio;
using System;
using EscogeTuAventura.Excepciones;

namespace EscogeTuAventura.Entidades
{
    public class Auditoria : IValidable
    {
        public int Id { get; set; }

        public AccionAuditoria Accion { get; set; }

        public DateTime Fecha { get; set; }

        public Usuario Administrador { get; set; } = null!;

        public Historia Historia { get; set; } = null!;

        public void Validar()
        {
            if (Administrador == null)
                throw new AuditoriaException("La auditoría debe tener un administrador.");

            if (Historia == null)
                throw new AuditoriaException("La auditoría debe tener una historia.");
        }
    }
}
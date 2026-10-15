using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAccesoDatos.EF;
using LogicaAccesoDatos.RepositorioEnMemoria;
using LogicaAplicacion.CasosDeUso.Auditorias;
using LogicaAplicacion.CasosDeUso.Capitulos;
using LogicaAplicacion.CasosDeUso.Categorias;
using LogicaAplicacion.CasosDeUso.Historias;
using LogicaAplicacion.CasosDeUso.Usuarios;
using LogicaAplicacion.InterfacesDeCasoDeUso.Auditorias;
using LogicaAplicacion.InterfacesDeCasoDeUso.Capitulos;
using LogicaAplicacion.InterfacesDeCasoDeUso.Categorias;
using LogicaAplicacion.InterfacesDeCasoDeUso.Historias;
using LogicaAplicacion.InterfacesDeCasoDeUso.Usuarios;
using LogicaAplicacion.CasosDeUso.Opciones;
using LogicaAplicacion.InterfacesDeCasoDeUso.Opciones;

namespace ApiWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            // Contexto y repositorios.
            builder.Services.AddScoped<EscogeTuAventuraContext>();

            builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuariosEF>();
            builder.Services.AddScoped<IRepositorioCategorias, RepositorioCategoriasEF>();
            builder.Services.AddScoped<IRepositorioHistorias, RepositorioHistoriasEF>();
            builder.Services.AddScoped<IRepositorioCapitulos, RepositorioCapitulosEF>();
            builder.Services.AddScoped<IRepositorioOpciones, RepositorioOpcionesEF>();
            builder.Services.AddScoped<IRepositorioAuditorias, RepositorioAuditoriasEF>();

            // La lectura todavía usa este repositorio hasta implementar
            // los repositorios EF de lecturas.
            builder.Services.AddScoped<IRepositorioLecturas, RepositorioLecturas>();

            // Casos de uso de usuarios.
            builder.Services.AddScoped<IAgregarUsuarioCU, AgregarUsuarioCU>();
            builder.Services.AddScoped<IEncontrarTodosUsuariosCU, EncontrarTodosUsuariosCU>();
            builder.Services.AddScoped<IObtenerUsuarioPorIdCU, ObtenerUsuarioPorIdCU>();
            builder.Services.AddScoped<ILoginAdministradorCU, LoginAdministradorCU>();

            // Casos de uso de categorías.
            builder.Services.AddScoped<IAgregarCategoriaCU, AgregarCategoriaCU>();
            builder.Services.AddScoped<IEncontrarTodasCategoriasCU, EncontrarTodasCategoriasCU>();
            builder.Services.AddScoped<IObtenerCategoriaPorIdCU, ObtenerCategoriaPorIdCU>();

            // Casos de uso de historias.
            builder.Services.AddScoped<IAgregarHistoriaCU, AgregarHistoriaCU>();
            builder.Services.AddScoped<IModificarHistoriaCU, ModificarHistoriaCU>();
            builder.Services.AddScoped<IEliminarHistoriaCU, EliminarHistoriaCU>();
            builder.Services.AddScoped<IPublicarHistoriaCU, PublicarHistoriaCU>();
            builder.Services.AddScoped<IEncontrarTodasHistoriasCU, EncontrarTodasHistoriasCU>();
            builder.Services.AddScoped<IObtenerHistoriaPorIdCU, ObtenerHistoriaPorIdCU>();
            builder.Services.AddScoped<IEncontrarHistoriasPublicadasCU, EncontrarHistoriasPublicadasCU>();
            builder.Services.AddScoped<IEncontrarHistoriasPublicadasPorCategoriaCU,
                EncontrarHistoriasPublicadasPorCategoriaCU>();

            // Casos de uso de capítulos.
            builder.Services.AddScoped<IAgregarCapituloCU, AgregarCapituloCU>();
            builder.Services.AddScoped<IEncontrarCapitulosPorHistoriaCU,
                EncontrarCapitulosPorHistoriaCU>();
            builder.Services.AddScoped<IObtenerCapituloPorIdCU, ObtenerCapituloPorIdCU>();
            builder.Services.AddScoped<IEncontrarOpcionesPorCapituloCU,
            EncontrarOpcionesPorCapituloCU>();

            // Casos de uso de auditorías.
            builder.Services.AddScoped<IRegistrarAuditoriaCU, RegistrarAuditoriaCU>();
            builder.Services.AddScoped<IEncontrarAuditoriasCU, EncontrarAuditoriasCU>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
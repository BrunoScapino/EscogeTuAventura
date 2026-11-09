using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAplicacion.CasosDeUso.Usuarios;
using LogicaAplicacion.CasosDeUso.Categorias;
using LogicaAplicacion.CasosDeUso.Historias;
using LogicaAplicacion.CasosDeUso.Capitulos;
using LogicaAplicacion.InterfacesDeCasoDeUso.Usuarios;
using LogicaAplicacion.InterfacesDeCasoDeUso.Categorias;
using LogicaAplicacion.InterfacesDeCasoDeUso.Historias;
using LogicaAplicacion.InterfacesDeCasoDeUso.Capitulos;
using LogicaAccesoDatos.EF;
using Microsoft.AspNetCore.Authentication.Cookies;
using LogicaAplicacion.CasosDeUso.Auditorias;
using LogicaAplicacion.InterfacesDeCasoDeUso.Auditorias;

namespace Presentacion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Cuenta/Login";
                options.AccessDeniedPath = "/Cuenta/AccesoDenegado";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
            });

            builder.Services.AddAuthorization();

            // Inicializamos los repositorios en memoria disponibles en el proyecto.
            builder.Services.AddScoped<EscogeTuAventuraContext>();
            builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuariosEF>();
            builder.Services.AddScoped<IRepositorioCategorias, RepositorioCategoriasEF>();
            builder.Services.AddScoped<IRepositorioHistorias, RepositorioHistoriasEF>();
            builder.Services.AddScoped<IRepositorioCapitulos, RepositorioCapitulosEF>();
            builder.Services.AddScoped<IRepositorioOpciones, RepositorioOpcionesEF>();
            builder.Services.AddScoped<IRepositorioAuditorias, RepositorioAuditoriasEF>();

            // Inicializamos los casos de uso de usuarios.
            builder.Services.AddScoped<IAgregarUsuarioCU, AgregarUsuarioCU>();
            builder.Services.AddScoped<IEncontrarTodosUsuariosCU, EncontrarTodosUsuariosCU>();
            builder.Services.AddScoped<IObtenerUsuarioPorIdCU, ObtenerUsuarioPorIdCU>();
            builder.Services.AddScoped<ILoginAdministradorCU, LoginAdministradorCU>();

            // Inicializamos los casos de uso de categorias.
            builder.Services.AddScoped<IAgregarCategoriaCU, AgregarCategoriaCU>();
            builder.Services.AddScoped<IEncontrarTodasCategoriasCU, EncontrarTodasCategoriasCU>();
            builder.Services.AddScoped<IObtenerCategoriaPorIdCU, ObtenerCategoriaPorIdCU>();

            // Inicializamos los casos de uso de historias.
            builder.Services.AddScoped<IAgregarHistoriaCU, AgregarHistoriaCU>();
            builder.Services.AddScoped<IModificarHistoriaCU, ModificarHistoriaCU>();
            builder.Services.AddScoped<IEliminarHistoriaCU, EliminarHistoriaCU>();
            builder.Services.AddScoped<IPublicarHistoriaCU, PublicarHistoriaCU>();
            builder.Services.AddScoped<IEncontrarTodasHistoriasCU, EncontrarTodasHistoriasCU>();
            builder.Services.AddScoped<IObtenerHistoriaPorIdCU, ObtenerHistoriaPorIdCU>();
            builder.Services.AddScoped<IEncontrarHistoriasPublicadasCU, EncontrarHistoriasPublicadasCU>();
            builder.Services.AddScoped<IEncontrarHistoriasPublicadasPorCategoriaCU, EncontrarHistoriasPublicadasPorCategoriaCU>();

            // Inicializamos los casos de uso de capitulos.
            builder.Services.AddScoped<IAgregarCapituloCU, AgregarCapituloCU>();
            builder.Services.AddScoped<IEncontrarCapitulosPorHistoriaCU, EncontrarCapitulosPorHistoriaCU>();
            builder.Services.AddScoped<IObtenerCapituloPorIdCU, ObtenerCapituloPorIdCU>();

            // Inicializamos los casos de uso de auditorías.
            builder.Services.AddScoped<IRegistrarAuditoriaCU, RegistrarAuditoriaCU>();
            builder.Services.AddScoped<IEncontrarAuditoriasCU, EncontrarAuditoriasCU>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}

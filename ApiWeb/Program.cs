using System.Text;
using EscogeTuAventura.InterfacesDeRepositorio;
using LogicaAccesoDatos.EF;
using LogicaAplicacion.CasosDeUso.Auditorias;
using LogicaAplicacion.CasosDeUso.Capitulos;
using LogicaAplicacion.CasosDeUso.Categorias;
using LogicaAplicacion.CasosDeUso.Historias;
using LogicaAplicacion.CasosDeUso.Lecturas;
using LogicaAplicacion.CasosDeUso.Metricas;
using LogicaAplicacion.CasosDeUso.Opciones;
using LogicaAplicacion.CasosDeUso.Usuarios;
using LogicaAplicacion.InterfacesDeCasoDeUso.Auditorias;
using LogicaAplicacion.InterfacesDeCasoDeUso.Capitulos;
using LogicaAplicacion.InterfacesDeCasoDeUso.Categorias;
using LogicaAplicacion.InterfacesDeCasoDeUso.Historias;
using LogicaAplicacion.InterfacesDeCasoDeUso.Lecturas;
using LogicaAplicacion.InterfacesDeCasoDeUso.Metricas;
using LogicaAplicacion.InterfacesDeCasoDeUso.Opciones;
using LogicaAplicacion.InterfacesDeCasoDeUso.Usuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace ApiWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            var claveJwt = Encoding.UTF8.GetBytes(
            builder.Configuration["Jwt:Key"] ?? string.Empty);

            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(claveJwt)
                        };
                });

            builder.Services.AddAuthorization();

            string connectionString =
            builder.Configuration.GetConnectionString("EscogeTuAventura") ?? throw new InvalidOperationException("No se encontró la cadena de conexión.");

            builder.Services.AddDbContext<EscogeTuAventuraContext>(
                options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuariosEF>();
            builder.Services.AddScoped<IRepositorioCategorias, RepositorioCategoriasEF>();
            builder.Services.AddScoped<IRepositorioHistorias, RepositorioHistoriasEF>();
            builder.Services.AddScoped<IRepositorioCapitulos, RepositorioCapitulosEF>();
            builder.Services.AddScoped<IRepositorioOpciones, RepositorioOpcionesEF>();
            builder.Services.AddScoped<IRepositorioAuditorias, RepositorioAuditoriasEF>();

            // La lectura todavía usa este repositorio hasta implementar
            // los repositorios EF de lecturas.
            builder.Services.AddScoped<IRepositorioLecturas, RepositorioLecturasEF>();

            // Casos de uso de usuarios.
            builder.Services.AddScoped<IAgregarUsuarioCU, AgregarUsuarioCU>();
            builder.Services.AddScoped<IEncontrarTodosUsuariosCU, EncontrarTodosUsuariosCU>();
            builder.Services.AddScoped<IObtenerUsuarioPorIdCU, ObtenerUsuarioPorIdCU>();
            builder.Services.AddScoped<ILoginAdministradorCU, LoginAdministradorCU>();
            builder.Services.AddScoped<ILoginUsuarioCU, LoginUsuarioCU>();

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
            builder.Services.AddScoped<IEncontrarHistoriasPublicadasPorCategoriaCU,EncontrarHistoriasPublicadasPorCategoriaCU>();

            // Casos de uso de capítulos.
            builder.Services.AddScoped<IAgregarCapituloCU, AgregarCapituloCU>();
            builder.Services.AddScoped<IEncontrarCapitulosPorHistoriaCU, EncontrarCapitulosPorHistoriaCU>();
            builder.Services.AddScoped<IObtenerCapituloPorIdCU, ObtenerCapituloPorIdCU>();
            builder.Services.AddScoped<IEncontrarOpcionesPorCapituloCU,EncontrarOpcionesPorCapituloCU>();

            // Casos de uso de auditorías.
            builder.Services.AddScoped<IRegistrarAuditoriaCU, RegistrarAuditoriaCU>();
            builder.Services.AddScoped<IEncontrarAuditoriasCU, EncontrarAuditoriasCU>();

            // Casos de uso de lecturas.
            builder.Services.AddScoped<IIniciarLecturaCU, IniciarLecturaCU>();
            builder.Services.AddScoped<IFinalizarLecturaCU, FinalizarLecturaCU>();
            builder.Services.AddScoped<IObtenerRankingHistoriasCU, ObtenerRankingHistoriasCU>();
            builder.Services.AddScoped<IObtenerEstadisticasFinalesCU, ObtenerEstadisticasFinalesCU>();
            builder.Services.AddScoped<IObtenerRankingUsuariosCU, ObtenerRankingUsuariosCU>();
            builder.Services.AddScoped<IEncontrarLecturasPorUsuarioYMesCU, EncontrarLecturasPorUsuarioYMesCU>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
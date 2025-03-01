using Microsoft.Extensions.DependencyInjection;
using SGHR.Persistence.Interfaces.habitacion;
using SGHR.Persistence.Interfaces.reserva;
using SGHR.Persistence.Interfaces.servicio;
using SGHR.Persistence.Interfaces.usuario;
using SGHR.Persistence.Repositories.habitacion;
using SGHR.Persistence.Repositories.reserva;
using SGHR.Persistence.Repositories.servicio;
using SGHR.Persistence.Repositories.usuario;


namespace SGHR.IOC.DependencyInjection
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            #region Inyeccion de dependencia habitacion
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IEstadoHabitacionRepository, EstadoHabitacionRepository>();
            services.AddScoped<IHabitacionRepository, HabitacionRepository>();
            services.AddScoped<IPisoRepository, PisoRepository>();
            services.AddScoped<ITarifaRepository, TarifaRepository>();
            #endregion

            #region Inyeccion de dependencias de reserva
            services.AddScoped<IReservaRepository, ReservaRepository>();
            #endregion

            #region Inyeccion de dependencias de servicio
            services.AddScoped<IServicioRepository, ServiciosRepository>();
            #endregion

            #region Inyeccion de dependencias de usuario
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IRolUsuarioRepository, RolUsuarioRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            #endregion
            return services;
        }
    }
}

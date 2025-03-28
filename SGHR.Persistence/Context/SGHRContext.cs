﻿
using Microsoft.EntityFrameworkCore;
using SGHR.Domain.Entities.habitacion;
using SGHR.Domain.Entities.reserva;
using SGHR.Domain.Entities.servicio;
using SGHR.Domain.Entities.usuario;
using SGHR.Persistence.Context.Seeds.Habitacion;

namespace SGHR.Persistence.Context
{
    public class SGHRContext : DbContext
    {
        public SGHRContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<EstadoHabitacion> EstadoHabitaciones { get; set; }
        public DbSet<Habitacion> Habitaciones { get; set; }
        public DbSet<Piso> Pisos { get; set; }
        public DbSet<Tarifa> Tarifas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<RolUsuario> RolUsuarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Servicios> Servicios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relaciones

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Cliente)
                .WithMany()
                .HasForeignKey(r => r.IdCliente);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Habitacion)
                .WithMany()
                .HasForeignKey(r => r.IdHabitacion);


            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.RolUsuario)
                .WithMany()
                .HasForeignKey(u => u.IdRolUsuario);

            // Restricciones de estado predeterminado
            modelBuilder.Entity<Categoria>()
                .Property(c => c.Estado)
                .HasDefaultValue(true);

            modelBuilder.Entity<Categoria>()
                .Property(c => c.FechaCreacion)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Tarifa>()
                .Property(t => t.Estado)
                .HasDefaultValue(true);

            modelBuilder.Entity<Cliente>()
                .Property(c => c.Estado)
                .HasDefaultValue(true);

            modelBuilder.Entity<Cliente>()
                .Property(c => c.FechaCreacion)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<EstadoHabitacion>()
                .Property(e => e.Estado)
                .HasDefaultValue(true);

            modelBuilder.Entity<EstadoHabitacion>()
                .Property(e => e.FechaCreacion)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Habitacion>()
                .Property(h => h.Estado)
                .HasDefaultValue(true);

            modelBuilder.Entity<Habitacion>()
                .Property(h => h.FechaCreacion)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Piso>()
                .Property(p => p.Estado)
                .HasDefaultValue(true);

            modelBuilder.Entity<Piso>()
                .Property(p => p.FechaCreacion)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Reserva>()
                .Property(r => r.FechaEntrada)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Reserva>()
                .Property(r => r.TotalPagado)
                .HasDefaultValue(0m);

            modelBuilder.Entity<Reserva>()
                .Property(r => r.CostoPenalidad)
                .HasDefaultValue(0m);

            modelBuilder.Entity<RolUsuario>()
                .Property(r => r.Estado)
                .HasDefaultValue(true);

            modelBuilder.Entity<RolUsuario>()
                .Property(r => r.FechaCreacion)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Estado)
                .HasDefaultValue(true);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.FechaCreacion)
                .HasDefaultValueSql("getdate()");

            modelBuilder.ApplyConfiguration(new HabitacionSeed());
            modelBuilder.ApplyConfiguration(new PisoSeed());
            modelBuilder.ApplyConfiguration(new CategoriaSeed());
            modelBuilder.ApplyConfiguration(new EstadoHabitacionSeed());
            modelBuilder.ApplyConfiguration(new TarifaSeed());

        }
    }
}
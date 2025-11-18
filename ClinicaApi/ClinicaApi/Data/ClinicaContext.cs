using ClinicaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CLINICAAPI.Data
{
    public class ClinicaContext : DbContext
    {
        public ClinicaContext(DbContextOptions<ClinicaContext> options) : base(options) { }

        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Clinica> Clinicas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define Crm como chave primária de Medico
            modelBuilder.Entity<Medico>()
                .HasKey(m => m.Crm);

            // Define relacionamentos 1:N
            modelBuilder.Entity<Clinica>()
                .HasMany(c => c.Medicos)
                .WithOne(m => m.Clinica)
                .HasForeignKey(m => m.ClinicaId);

            modelBuilder.Entity<Clinica>()
                .HasMany(c => c.Pacientes)
                .WithOne(p => p.Clinica)
                .HasForeignKey(p => p.ClinicaId);
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ClinicaApi.Models
{
    public class Clinica
    {
        [Key]
        public int Id { get; set; }  // Chave primária padrão

        public string Nome { get; set; } = null!;

        public string Endereco { get; set; } = null!;

        // Relacionamento 1:N com Médicos
        public List<Medico> Medicos { get; set; } = new List<Medico>();

        // Relacionamento 1:N com Pacientes (se desejar)
        public List<Paciente> Pacientes { get; set; } = new List<Paciente>();
    }
}

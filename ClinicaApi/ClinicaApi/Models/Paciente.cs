using System.ComponentModel.DataAnnotations;

namespace ClinicaApi.Models
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public int Idade { get; set; }

        public string Endereco { get; set; } = null!;

        public int ClinicaId { get; set; }          // FK obrigatória

        public Clinica? Clinica { get; set; }       // navegação OPCIONAL
    }
}

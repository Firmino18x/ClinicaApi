using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClinicaApi.Models
{
    public class Medico
    {
        [Key]
        public int Crm { get; set; }  // Chave primária explícita

        public string Nome { get; set; } = null!;
        public int Idade { get; set; }
        public string Endereco { get; set; } = null!;

        public int ClinicaId { get; set; }   // Chave estrangeira obrigatória
        [JsonIgnore]
        public Clinica? Clinica { get; set; }  // Navegação obrigatória à Clínica
    }
}

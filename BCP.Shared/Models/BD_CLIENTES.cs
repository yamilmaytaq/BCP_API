using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BCP.Shared.Models
{
    public class BD_CLIENTES
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Paterno { get; set; }
        [MaxLength(30)]
        public string Materno { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nombres { get; set; }
        [Required]
        [MaxLength(12)]
        public string CI { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [MaxLength(30)]
        public string Genero { get; set; }
        [Required]
        public int Celular { get; set; }
        [Required]
        public decimal NivelIngresos { get; set; }
        [Required]
        [MaxLength(30)]
        public string Correo { get; set; }
        [Required]
        [MaxLength(14)]
        public string Cuenta { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}

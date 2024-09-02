using System.ComponentModel.DataAnnotations;

namespace BCP_API_JM.Models.DTO
{
    public class BD_CLIENTES_UPDATE_DTO
    {
        [Required]
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
    }
}

using System.ComponentModel.DataAnnotations;

namespace BCP.Shared.Models.DTO
{
    public class BD_USUARIOS_UPDATE_DTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Contrasenia { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BCP.Shared.Models.DTO
{
    public class BD_USUARIOS_LOGIN_DTO
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Contrasenia { get; set; }
    }
}

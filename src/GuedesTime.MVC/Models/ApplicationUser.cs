using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GuedesTime.MVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        // URL/Path relativo da imagem (ex.: "/uploads/avatars/<id>.png" ou "/assets/avatar/x.jpg")
        public string Imagem { get; set; } = string.Empty;

        public string AvatarContentType { get; set; } = string.Empty;
        public string AvatarDataProtected { get; set; } = string.Empty;

        // CPF (opcional) - armazenado sem máscara (somente dígitos)
        public string Documento { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string Password { get; set; }
        public string ConfirmPassord { get; set; }
    }
}

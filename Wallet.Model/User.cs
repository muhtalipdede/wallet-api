using System.ComponentModel.DataAnnotations;

namespace Wallet.Model
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string PasswordHash { get; set; }
    }
}

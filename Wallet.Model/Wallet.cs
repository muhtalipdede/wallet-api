using System.ComponentModel.DataAnnotations;

namespace Wallet.Model
{
    public class Wallet
    {
        [Key]
        public Guid WalletId { get; set; }

        [Required]
        [MaxLength(50)]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(3)]
        public string Currency { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Wallet.Constant;

namespace Wallet.Model
{
    public class WalletTransaction
    {
        [Key]
        public Guid WalletTransactionId { get; set; }

        [Required]
        public Guid WalletId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }
        public Wallet Wallet { get; set; }
    }
}

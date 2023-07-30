using Wallet.Constant;

namespace Wallet.Dto
{
    public class CreateWalletTransactionResponse
    {
        public Guid WalletTransactionId { get; set; }
        public Guid WalletId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}

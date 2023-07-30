using Wallet.Constant;

namespace Wallet.Dto
{
    public class CreateWalletTransactionRequest
    {
        public Guid WalletId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}

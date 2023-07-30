namespace Wallet.Dto
{
    public class DepositRequest
    {
        public Guid WalletId { get; set; }
        public decimal Amount { get; set; }
    }
}

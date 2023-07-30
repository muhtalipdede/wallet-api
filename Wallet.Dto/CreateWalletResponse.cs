namespace Wallet.Dto
{
    public class CreateWalletResponse
    {
        public Guid WalletId { get; set; }
        public Guid UserId { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public string Name { get; set; }
    }
}

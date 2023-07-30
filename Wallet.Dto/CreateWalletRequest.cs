namespace Wallet.Dto
{
    public class CreateWalletRequest
    {
        public Guid UserId { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public string Name { get; set; }
    }
}

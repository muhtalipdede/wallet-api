using Wallet.Model;

namespace Wallet.Data
{
    public class WalletRepository : IWalletRepository
    {
        private readonly WalletDbContext _walletDbContext;
        public WalletRepository(WalletDbContext walletDbContext)
        {
            _walletDbContext = walletDbContext;
        }

        public async Task<Model.Wallet> Get(Guid walletId)
        {
            var result = await _walletDbContext.Wallets.FindAsync(walletId);

            if (result == null)
            {
                throw new Exception("Wallet not found");
            }

            return result;
        }
        public async Task<Model.Wallet> Create(Model.Wallet request)
        {
            var result = await _walletDbContext.Wallets.AddAsync(request);
            await _walletDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<decimal> GetBalance(Guid walletId)
        {
            var result = await _walletDbContext.Wallets.FindAsync(walletId);

            if (result == null)
            {
                throw new Exception("Wallet not found");
            }

            return result.Balance;
        }

        public async Task UpdateBalance(Guid walletId, decimal amount)
        {
            var wallet = await _walletDbContext.Wallets.FindAsync(walletId);

            if (wallet == null)
            {
                throw new Exception("Wallet not found");
            }

            wallet.Balance += amount;
            await _walletDbContext.SaveChangesAsync();
        }
    }

    public interface IWalletRepository
    {
        Task<Model.Wallet> Get(Guid walletId);
        Task<Model.Wallet> Create(Model.Wallet request);
        Task<decimal> GetBalance(Guid walletId);
        Task UpdateBalance(Guid walletId, decimal amount);
    }
}

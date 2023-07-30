using Wallet.Model;

namespace Wallet.Data
{
    public class WalletTransactionRepository : IWalletTransactionRepository
    {
        private readonly WalletDbContext _walletDbContext;
        public WalletTransactionRepository(WalletDbContext walletDbContext)
        {
            _walletDbContext = walletDbContext;
        }
        public async Task<Model.WalletTransaction> Create(Model.WalletTransaction walletTransaction)
        {
            var result = await _walletDbContext.WalletTransactions.AddAsync(walletTransaction);
            _walletDbContext.SaveChanges();
            return result.Entity;
        }
    }

    public interface IWalletTransactionRepository
    {
        Task<Model.WalletTransaction> Create(Model.WalletTransaction walletTransaction);
    }
}

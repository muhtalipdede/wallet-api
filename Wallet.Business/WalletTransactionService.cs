using Wallet.Data;

namespace Wallet.Business
{
    public class WalletTransactionService : IWalletTransactionService
    {
        private readonly IWalletTransactionRepository _walletTransactionRepository;
        private readonly IWalletTransactionValidator _walletTransactionValidator;

        public WalletTransactionService(IWalletTransactionRepository walletTransactionRepository, IWalletTransactionValidator walletTransactionValidator)
        {
            _walletTransactionRepository = walletTransactionRepository;
            _walletTransactionValidator = walletTransactionValidator;
        }

        public async Task<Dto.CreateWalletTransactionResponse> Create(Dto.CreateWalletTransactionRequest request)
        {
            var validationResult = await _walletTransactionValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.Message);
            }

            var model = new Model.WalletTransaction
            {
                WalletTransactionId = Guid.NewGuid(),
                WalletId = request.WalletId,
                Amount = request.Amount,
                TransactionType = request.TransactionType
            };

            var result = await _walletTransactionRepository.Create(model);

            if (result == null)
            {
                throw new Exception("Wallet transaction creation failed");
            }

            return new Dto.CreateWalletTransactionResponse
            {
                WalletTransactionId = result.WalletTransactionId,
                WalletId = result.WalletId,
                Amount = result.Amount,
                TransactionType = result.TransactionType
            };
        }
    }

    public interface IWalletTransactionService
    {
        Task<Dto.CreateWalletTransactionResponse> Create(Dto.CreateWalletTransactionRequest request);
    }
}

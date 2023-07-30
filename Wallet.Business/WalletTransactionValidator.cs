using Wallet.Data;

namespace Wallet.Business
{
    public class WalletTransactionValidator : IWalletTransactionValidator
    {
        private readonly IWalletRepository _walletRepository;
        public WalletTransactionValidator(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<Dto.ValidationResult> Validate(Dto.CreateWalletTransactionRequest request)
        {
            var result = new Dto.ValidationResult();

            var wallet = await _walletRepository.Get(request.WalletId);

            if (wallet == null)
            {
                result.IsValid = false;
                result.Message = "Wallet not found";
                return result;
            }

            if (request.TransactionType == Constant.TransactionType.Debit)
            {
                if (wallet.Balance < request.Amount)
                {
                    result.IsValid = false;
                    result.Message = "Insufficient balance";
                    return result;
                }
            }

            result.IsValid = true;
            return result;
        }
    }

    public interface IWalletTransactionValidator
    {
        Task<Dto.ValidationResult> Validate(Dto.CreateWalletTransactionRequest request);
    }
}

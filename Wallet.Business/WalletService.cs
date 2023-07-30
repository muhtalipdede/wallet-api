using Wallet.Constant;
using Wallet.Data;
using Wallet.Dto;

namespace Wallet.Business
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletTransactionService _walletTransactionService;

        public WalletService(IWalletRepository walletRepository, IWalletTransactionService walletTransactionService)
        {
            _walletRepository = walletRepository;
            _walletTransactionService = walletTransactionService;
        }
        public async Task<CreateWalletResponse> Create(CreateWalletRequest request)
        {
            var model = new Model.Wallet
            {
                WalletId = Guid.NewGuid(),
                Name = request.Name,
                Balance = request.Balance,
                Currency = request.Currency,
                UserId = request.UserId
            };
            var result = await _walletRepository.Create(model);

            if (result == null)
            {
                throw new Exception("Wallet creation failed");
            }

            return new CreateWalletResponse
            {
                WalletId = result.WalletId,
                Name = result.Name,
                Balance = result.Balance,
                Currency = result.Currency,
                UserId = result.UserId
            };
        }

        public async Task<decimal> GetBalance(Guid walletId)
        {
            var result = await _walletRepository.GetBalance(walletId);
            return result;
        }

        public async Task<ResultCode> Deposit(DepositRequest request)
        {
            var wallet = await _walletRepository.Get(request.WalletId);

            if (wallet == null)
            {
                throw new Exception("Wallet not found");
            }

            await _walletRepository.UpdateBalance(request.WalletId, request.Amount);

            var transaction = new CreateWalletTransactionRequest
            {
                WalletId = request.WalletId,
                Amount = request.Amount,
                TransactionType = TransactionType.Deposit
            };

            var result = await _walletTransactionService.Create(transaction);

            if (result == null)
            {
                throw new Exception("Deposit failed");
            }

            return ResultCode.Success;
        }

        public async Task<ResultCode> Withdraw(WithdrawRequest request)
        {
            var wallet = await _walletRepository.Get(request.WalletId);

            if (wallet == null)
            {
                throw new Exception("Wallet not found");
            }

            if (wallet.Balance < request.Amount)
            {
                throw new Exception("Insufficient balance");
            }

            await _walletRepository.UpdateBalance(request.WalletId, -request.Amount);

            var transaction = new CreateWalletTransactionRequest
            {
                WalletId = request.WalletId,
                Amount = request.Amount,
                TransactionType = TransactionType.Withdraw
            };

            var result = await _walletTransactionService.Create(transaction);

            if (result == null)
            {
                throw new Exception("Withdraw failed");
            }

            return ResultCode.Success;
        }
    }

    public interface IWalletService
    {
        Task<CreateWalletResponse> Create(CreateWalletRequest request);
        Task<decimal> GetBalance(Guid walletId);
        Task<ResultCode> Deposit(DepositRequest request);
        Task<ResultCode> Withdraw(WithdrawRequest request);
    }
}

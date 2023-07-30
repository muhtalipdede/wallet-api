using Moq;
using System;
using System.Threading.Tasks;
using Wallet.Business;
using Wallet.Constant;
using Wallet.Data;
using Wallet.Dto;
using Xunit;

namespace Wallet.Test
{
    public class WalletServiceTest : IClassFixture<DatabaseFixture>
    {
        public WalletServiceTest()
        {
        }

        [Fact]
        public async Task CreateWallet()
        {
            // Arrange
            var walletRepository = new Mock<IWalletRepository>();
            var walletTransactionService = new Mock<IWalletTransactionService>();
            var walletService = new WalletService(walletRepository.Object, walletTransactionService.Object);
            var request = new CreateWalletRequest
            {
                Name = "Test Wallet",
                Balance = 100
            };
            walletRepository.Setup(x => x.Create(It.IsAny<Model.Wallet>())).ReturnsAsync(new Model.Wallet
            {
                WalletId = Guid.NewGuid(),
                Name = request.Name,
                Balance = request.Balance
            });

            // Act
            var result = await walletService.Create(request);

            // Assert
            Assert.Equal(request.Name, result.Name);
            Assert.Equal(request.Balance, result.Balance);
        }

        [Fact]
        public async Task GetBalance()
        {
            // Arrange
            var walletRepository = new Mock<IWalletRepository>();
            var walletTransactionService = new Mock<IWalletTransactionService>();
            var walletService = new WalletService(walletRepository.Object, walletTransactionService.Object);
            var walletId = Guid.NewGuid();
            var balance = 100;
            walletRepository.Setup(x => x.GetBalance(walletId)).ReturnsAsync(balance);

            // Act
            var result = await walletService.GetBalance(walletId);

            // Assert
            Assert.Equal(balance, result);
        }

        [Fact]
        public async Task Deposit()
        {
            // Arrange
            var walletRepository = new Mock<IWalletRepository>();
            var walletTransactionService = new Mock<IWalletTransactionService>();
            var walletService = new WalletService(walletRepository.Object, walletTransactionService.Object);
            var walletId = Guid.NewGuid();
            var amount = 100;
            var wallet = new Model.Wallet
            {
                WalletId = walletId,
                Balance = 0
            };
            var walletTransactionResult = new CreateWalletTransactionResponse
            {
                WalletTransactionId = Guid.NewGuid(),
                TransactionType = TransactionType.Deposit,
                Amount = amount,
                WalletId = walletId,
            };

            walletRepository.Setup(x => x.Get(walletId)).ReturnsAsync(wallet);
            walletRepository.Setup(x => x.UpdateBalance(walletId, amount));
            walletTransactionService.Setup(x => x.Create(It.IsAny<CreateWalletTransactionRequest>())).ReturnsAsync(walletTransactionResult);
            // Act
            var result = await walletService.Deposit(new DepositRequest
            {
                WalletId = walletId,
                Amount = amount
            });

            // Assert
            Assert.Equal(ResultCode.Success, result);
        }

        [Fact]
        public async Task Withdraw()
        {
            // Arrange
            var walletRepository = new Mock<IWalletRepository>();
            var walletTransactionService = new Mock<IWalletTransactionService>();
            var walletService = new WalletService(walletRepository.Object, walletTransactionService.Object);
            var walletId = Guid.NewGuid();
            var amount = 100;
            var wallet = new Model.Wallet
            {
                WalletId = walletId,
                Balance = 200
            };
            var walletTransactionResult = new CreateWalletTransactionResponse
            {
                WalletTransactionId = Guid.NewGuid()
            };
            walletRepository.Setup(x => x.Get(walletId)).ReturnsAsync(wallet);
            walletRepository.Setup(x => x.UpdateBalance(walletId, -amount));
            walletTransactionService.Setup(x => x.Create(It.IsAny<CreateWalletTransactionRequest>())).ReturnsAsync(walletTransactionResult);

            // Act
            var result = await walletService.Withdraw(new WithdrawRequest
            {
                WalletId = walletId,
                Amount = amount
            });

            // Assert
            Assert.Equal(ResultCode.Success, result);
        }
    }
}

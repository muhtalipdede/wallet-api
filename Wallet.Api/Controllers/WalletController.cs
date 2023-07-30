using Microsoft.AspNetCore.Mvc;
using Wallet.Business;
using Wallet.Constant;
using Wallet.Dto;

namespace Wallet.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost]
        [Route("create")]

        public async Task<CreateWalletResponse> Create([FromBody] CreateWalletRequest request)
        {
            var result = await _walletService.Create(request);
            return result;
        }

        [HttpGet]
        [Route("get/{walletId}/balance")]
        public async Task<decimal> GetBalance(Guid walletId)
        {
            var result = await _walletService.GetBalance(walletId);
            return result;
        }

        [HttpPost]
        [Route("deposit")]
        public async Task<ResultCode> Deposit([FromBody] DepositRequest request)
        {
            var result = await _walletService.Deposit(request);
            return result;
        }

        [HttpPost]
        [Route("withdraw")]
        public async Task<ResultCode> Withdraw([FromBody] WithdrawRequest request)
        {
            var result = await _walletService.Withdraw(request);
            return result;
        }
    }
}

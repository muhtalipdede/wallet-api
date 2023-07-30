using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Dto
{
    public class WithdrawRequest
    {
        public Guid WalletId { get; set; }
        public decimal Amount { get; set; }
    }
}

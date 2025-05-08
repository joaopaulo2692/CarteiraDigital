using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Core.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public Guid FromWalletId { get; set; }
        public Wallet FromWallet { get; set; } = null!;

        public Guid ToWalletId { get; set; }
        public Wallet ToWallet { get; set; } = null!;
    }

}

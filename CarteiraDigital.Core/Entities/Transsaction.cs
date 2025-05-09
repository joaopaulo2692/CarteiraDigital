using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Core.Entities
{
    public class Transaction
    {
        public int Id { get; set; } 
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public int FromWalletId { get; set; }
        public Wallet FromWallet { get; set; } = null!;

        public int ToWalletId { get; set; }
        public Wallet ToWallet { get; set; } = null!;
    }

}

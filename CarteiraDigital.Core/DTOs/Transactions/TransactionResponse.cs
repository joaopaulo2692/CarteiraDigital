using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Application.DTOs.Transactions
{
    public class TransactionResponse
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }

        public string FromUser { get; set; } = null!;
        public string ToUser { get; set; } = null!;
    }
}

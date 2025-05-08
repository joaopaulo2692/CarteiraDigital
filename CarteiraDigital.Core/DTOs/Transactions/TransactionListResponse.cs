using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Application.DTOs.Transactions
{
    public class TransactionListResponse
    {
        public List<TransactionResponse> Transactions { get; set; } = new();
    }
}

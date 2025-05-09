using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Application.DTOs.Transactions
{
    public class CreateTransactionRequest
    {
        public int DestinationUserId { get; set; }
        public decimal Amount { get; set; }
    }
}

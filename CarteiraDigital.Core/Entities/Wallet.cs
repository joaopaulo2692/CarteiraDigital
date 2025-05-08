using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CarteiraDigital.Core.Entities
{
    public class Wallet
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Balance { get; set; } = 0m;

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        public virtual ICollection<Transaction> OutgoingTransactions { get; set; } = new List<Transaction>();
        public virtual ICollection<Transaction> IncomingTransactions { get; set; } = new List<Transaction>();
    }

}

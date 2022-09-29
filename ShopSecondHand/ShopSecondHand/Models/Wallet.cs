using System;
using System.Collections.Generic;

#nullable disable

namespace ShopSecondHand.Models
{
    public partial class Wallet
    {
        public Wallet()
        {
            Transactions = new HashSet<Transaction>();
        }

        public Guid Id { get; set; }
        public double? Balance { get; set; }
        public Guid? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}

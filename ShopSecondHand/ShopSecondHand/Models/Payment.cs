using System;
using System.Collections.Generic;

#nullable disable

namespace ShopSecondHand.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Transactions = new HashSet<Transaction>();
        }

        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public TimeSpan? PaymentTime { get; set; }

        public virtual Order Order { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}

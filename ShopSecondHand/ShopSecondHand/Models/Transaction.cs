using System;
using System.Collections.Generic;

#nullable disable

namespace ShopSecondHand.Models
{
    public partial class Transaction
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public Guid? PaymentId { get; set; }
        public Guid? WalletId { get; set; }

        public virtual Payment Payment { get; set; }
        public virtual Wallet Wallet { get; set; }
    }
}

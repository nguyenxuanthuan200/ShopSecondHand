using System;
using ShopSecondHand.Models;

namespace ShopSecondHand.Data.ResponseModels.OrderResponse
{
    public class GetOrderWithTransactionResponse
    {
        public Guid Id { get; set; }
        public Guid? PostId { get; set; }
        public Guid? AccountId { get; set; }
        public double? Total { get; set; }
        public Transaction Transaction { get; set; }
    }
}

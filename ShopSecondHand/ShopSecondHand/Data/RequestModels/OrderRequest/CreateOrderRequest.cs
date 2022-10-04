using System;

namespace ShopSecondHand.Data.RequestModels.OrderRequest
{
    public class CreateOrderRequest
    {
        public Guid? PostId { get; set; }
        public Guid? UserId { get; set; }
        public double? Total { get; set; }
    }
}

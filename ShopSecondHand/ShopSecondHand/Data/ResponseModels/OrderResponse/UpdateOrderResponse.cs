using System;

namespace ShopSecondHand.Data.ResponseModels.OrderResponse
{
    public class UpdateOrderResponse
    {
        public Guid? PostId { get; set; }
        public Guid? UserId { get; set; }
        public double? Total { get; set; }
    }
}

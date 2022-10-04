using System;

namespace ShopSecondHand.Data.ResponseModels.OrderResponse
{
    public class GetOrderResponse
    {
        public Guid Id { get; set; }
        public Guid? PostId { get; set; }
        public Guid? UserId { get; set; }
        public double? Total { get; set; }
    }
}

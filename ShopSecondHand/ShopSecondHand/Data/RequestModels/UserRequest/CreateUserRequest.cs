﻿using System;
namespace ShopSecondHand.Data.RequestModels.UserRequest
{
    public class CreateUserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public Guid? BuildingId { get; set; }
    }
}
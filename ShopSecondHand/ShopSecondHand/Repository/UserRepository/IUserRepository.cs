﻿using ShopSecondHand.Data.RequestModels.UserRequest;
using ShopSecondHand.Data.ResponseModels.UserResponse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.UserRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<GetUserResponse>> GetUser();
        Task<GetUserResponse> GetUserByUserName(String name);
        Task<IEnumerable<GetUserResponse>> GetUserByBuildingName(String name);
        Task<CreateUserResponse> CreateUser(CreateUserRequest userRequest);
        Task<UpdateUserResponse> UpdateUser(String userName, UpdateUserRequest userRequest);
        void DeleteUser(String userName);
    }
}
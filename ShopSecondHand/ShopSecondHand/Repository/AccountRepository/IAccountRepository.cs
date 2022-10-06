using ShopSecondHand.Data.RequestModels.AccountRequest;
using ShopSecondHand.Data.ResponseModels.AccountResponse;
using ShopSecondHand.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.AccountRepository
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
       // Task<IEnumerable<GetAccountResponse>> GetUser();
       // Task<GetAccountResponse> GetUserById(Guid id);
       //// Task<GetAccountResponse> GetUserByUserName(string name);
       // //Task<IEnumerable<GetUserResponse>> GetUserByBuildingName(string name);
       // Task<CreateAccountResponse> CreateUser(CreateAccountRequest userRequest);
       // Task<UpdateAccountResponse> UpdateUser(Guid id, UpdateAccountRequest userRequest);
       // void DeleteUser(Guid id);

        Task<Account> GetByUserNameAndPassword(string userName, string password);
        Task<List<Account>> GetUserByBuildingId(Guid id);
    }
}

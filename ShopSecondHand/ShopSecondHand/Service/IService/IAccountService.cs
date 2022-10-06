using ShopSecondHand.Data.Common;
using ShopSecondHand.Data.RequestModels.AccountRequest;
using ShopSecondHand.Data.ResponseModels.AccountResponse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopSecondHand.Service.IService
{
    public interface IAccountService
    {
        Task<GenericResult<GetAccountResponse>> GetById(Guid id);
        //Task<GenericResult<UserWithWalletDTO>> CreateUser(CreateUserRequest payload);
        Task<GenericResult<UpdateAccountResponse>> UpdateUser(Guid id,UpdateAccountRequest payload);
        Task<GenericResult<List<GetAccountResponse>>> GetAll();
        Task<GenericResult<GetAccountResponse>> DeleteUser(Guid id);
        Task<GenericResult<List<GetAccountResponse>>> GetUserByBuildingId(Guid id);
    }
}

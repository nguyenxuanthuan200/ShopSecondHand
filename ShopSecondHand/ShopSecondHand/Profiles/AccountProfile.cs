using AutoMapper;
using ShopSecondHand.Data.ResponseModels.AccountResponse;
using ShopSecondHand.Models;
using VoiceAPI.Models.Responses.Auths;

namespace ShopSecondHand.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            //CreateMap<Models.User, Data.ResponseModels.UserResponse.GetUserResponse>().ReverseMap();
            //CreateMap<Models.User, Data.ResponseModels.UserResponse.CreateUserResponse>().ReverseMap();
            //CreateMap<Models.User, Data.ResponseModels.UserResponse.UpdateUserResponse>().ReverseMap();

            CreateMap<Account, CreateAccountResponse>().ReverseMap();
            CreateMap<Account, UpdateAccountResponse>().ReverseMap();

            CreateMap<Account, GetAccountResponse>().ReverseMap();

            CreateMap<Account, JwtTokenDTO>().ReverseMap();

            CreateMap<Account, AccountWithWalletDTO>().ReverseMap();
        }

    }
}

using AutoMapper;
using ShopSecondHand.Data.ResponseModels.Auths;

namespace ShopSecondHand.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Models.Account, Data.ResponseModels.AccountResponse.GetAccountResponse>().ReverseMap();
            CreateMap<Models.Account, Data.ResponseModels.AccountResponse.CreateAccountResponse>().ReverseMap();
            CreateMap<Models.Account, Data.ResponseModels.AccountResponse.UpdateAccountResponse>().ReverseMap();

            //CreateMap<Account, CreateAccountResponse>().ReverseMap();
            //CreateMap<Account, UpdateAccountResponse>().ReverseMap();

            //CreateMap<Account, GetAccountResponse>().ReverseMap();

            CreateMap<Models.Account, JwtTokenDTO>().ReverseMap();

            CreateMap<Models.Account, Data.ResponseModels.AccountResponse.AccountWithWalletDTO>().ReverseMap();
        }

    }
}

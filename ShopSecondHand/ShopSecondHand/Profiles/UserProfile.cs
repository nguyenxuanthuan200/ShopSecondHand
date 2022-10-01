using AutoMapper;

namespace ShopSecondHand.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Models.User, Data.ResponseModels.UserResponse.GetUserResponse>().ReverseMap();
            CreateMap<Models.User, Data.ResponseModels.UserResponse.CreateUserResponse>().ReverseMap();
            CreateMap<Models.User, Data.ResponseModels.UserResponse.UpdateUserResponse>().ReverseMap();
        }
    }
}

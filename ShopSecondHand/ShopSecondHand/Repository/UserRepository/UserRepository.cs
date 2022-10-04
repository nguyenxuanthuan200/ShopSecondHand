using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopSecondHand.Data.RequestModels.UserRequest;
using ShopSecondHand.Data.ResponseModels.UserResponse;
using ShopSecondHand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ShopSecondHandContext dbContext;
        private readonly IMapper _mapper;

        public UserRepository(ShopSecondHandContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<CreateUserResponse> CreateUser(CreateUserRequest userRequest)
        {
            var user = await dbContext.Users
                 .FirstOrDefaultAsync(p => p.UserName.Equals(userRequest.UserName));
            if (user != null)
                return null;
            var roleId = await dbContext.Roles
                 .FirstOrDefaultAsync(p => p.Name.Equals("User"));

            User userr = new User();
            {
                userr.Id = Guid.NewGuid();
                userr.UserName = userRequest.UserName;
                userr.Password = userRequest.Password;
                userr.FullName = userRequest.FullName;
                userr.Description = userRequest.Description;
                userr.Phone = userRequest.Phone;
                userr.Gender = userRequest.Gender;
                userr.RoleId = roleId.Id;
                userr.BuildingId = userRequest.BuildingId;
            };
            dbContext.Users.AddAsync(userr);
            dbContext.SaveChangesAsync();
            var re = _mapper.Map<CreateUserResponse>(userr);
            return re;
        }

        public void DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GetUserResponse>> GetUser()
        {
            var user = await dbContext.Users.ToListAsync();
            IEnumerable<GetUserResponse> result = user.Select(
                x =>
                {
                    return new GetUserResponse()
                    {
                        UserName = x.UserName,
                        Password = x.Password,
                        FullName = x.FullName,
                        Description = x.Description,
                        Phone = x.Phone,
                        Gender = x.Gender,
                        BuildingId = x.BuildingId,
                    };
                }
                ).ToList();
            return result;
        }

        public async Task<IEnumerable<GetUserResponse>> GetUserByBuildingName(string name)
        {
            var getByName = await dbContext.Buildings
                .FirstOrDefaultAsync(p => p.Name.Equals(name));
            Guid idBuilding;
            if (getByName != null)
                idBuilding = getByName.Id;
            else
                return null;


            var userByBuilding = await dbContext.Users
                .Where(p => p.BuildingId == idBuilding).ToListAsync();

            IEnumerable<GetUserResponse> result = userByBuilding.Select(
                x =>
                {
                    return new GetUserResponse()
                    {
                        UserName = x.UserName,
                        Password = x.Password,
                        FullName = x.FullName,
                        Description = x.Description,
                        Phone = x.Phone,
                        Gender = x.Gender,
                        BuildingId = x.BuildingId,
                    };
                }
                ).ToList();
            return result;
        }

        public async Task<GetUserResponse> GetUserById(Guid id)
        {
            var getById = await dbContext.Users
               .SingleOrDefaultAsync(p => p.Id == id);
            if (getById != null)
            {
                var re = new GetUserResponse()
                {
                    UserName = getById.UserName,
                    Password = getById.Password,
                    FullName = getById.FullName,
                    Description = getById.Description,
                    Phone = getById.Phone,
                    Gender = getById.Gender,
                    BuildingId = getById.BuildingId,
                };
                return re;
            }
            return null;
        }

        public async Task<GetUserResponse> GetUserByUserName(string name)
        {
            var getByUserName = await dbContext.Users
               .SingleOrDefaultAsync(p => p.UserName.Equals(name));
            if (getByUserName != null)
            {
                var re = new GetUserResponse()
                {
                    UserName = getByUserName.UserName,
                    Password = getByUserName.Password,
                    FullName = getByUserName.FullName,
                    Description = getByUserName.Description,
                    Phone = getByUserName.Phone,
                    Gender = getByUserName.Gender,
                    BuildingId = getByUserName.BuildingId,
                };
                return re;
            }
            return null;
        }

        public async Task<UpdateUserResponse> UpdateUser(Guid id, UpdateUserRequest userRequest)
        {
            var upUser = await dbContext.Users
                .Where(p => p.Id == id)
                .SingleOrDefaultAsync();

            if (upUser == null) return null;
            if (id != userRequest.Id) return null;

            upUser.Password = userRequest.Password;
            upUser.FullName = userRequest.FullName;
            upUser.Description = userRequest.Description;
            upUser.Phone = userRequest.Phone;
            upUser.Gender = userRequest.Gender;
            upUser.BuildingId = userRequest.BuildingId;

            dbContext.Users.Update(upUser);
            await dbContext.SaveChangesAsync();

            var up = _mapper.Map<UpdateUserResponse>(upUser);
            return up;
        }
    }
}

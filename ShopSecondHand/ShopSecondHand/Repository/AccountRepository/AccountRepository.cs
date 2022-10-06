using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopSecondHand.Data.RequestModels.AccountRequest;
using ShopSecondHand.Data.ResponseModels.AccountResponse;
using ShopSecondHand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopSecondHand.Repository.AccountRepository
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        private readonly ShopSecondHandContext dbContext;
        private readonly IMapper _mapper;

        public AccountRepository(ShopSecondHandContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Account> GetByUserNameAndPassword(string userName, string password)
        {
            var account = await Get()
                .Where(tempAccount => tempAccount.UserName.Equals(userName))
                .FirstOrDefaultAsync();

            if (account == null)
                return null;

            var isVerified = BCrypt.Net.BCrypt.EnhancedVerify(password, account.Password);

            return isVerified
                ? account
                : null;
        }

        public async Task<List<Account>> GetUserByBuildingId(Guid id)
        {
            var userBy = await dbContext.Buildings.Join(dbContext.Accounts,
                                                    building => building.Id,
                                                    user => user.BuildingId,
                                                    (building, user) => new { building, user })
                                            .Where(result => result.building.Id.CompareTo(id) == 0)
                                            .Select(result => result.user)
                                            .ToListAsync();


            return userBy;
        }
        //public async Task<CreateAccountResponse> CreateUser(CreateAccountRequest userRequest)
        //{
        //    var user = await dbContext.Accounts
        //         .FirstOrDefaultAsync(p => p.UserName.Equals(userRequest.UserName));
        //    if (user != null)
        //        return null;
        //    var roleId = await dbContext.Roles
        //         .FirstOrDefaultAsync(p => p.Name.Equals("User"));

        //    Account userr = new Account();
        //    {
        //        userr.Id = Guid.NewGuid();
        //        userr.UserName = userRequest.UserName;
        //        userr.Password = userRequest.Password;
        //        userr.FullName = userRequest.FullName;
        //        userr.Description = userRequest.Description;
        //        userr.Phone = userRequest.Phone;
        //        userr.Gender = userRequest.Gender;
        //        userr.RoleId = roleId.Id;
        //        userr.BuildingId = userRequest.BuildingId;
        //    };
        //    dbContext.Accounts.AddAsync(userr);
        //    dbContext.SaveChangesAsync();
        //    var re = _mapper.Map<CreateAccountResponse>(userr);
        //    return re;
        //}

        //public void DeleteUser(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<IEnumerable<GetAccountResponse>> GetUser()
        //{
        //    var user = await dbContext.Accounts.ToListAsync();
        //    IEnumerable<GetAccountResponse> result = user.Select(
        //        x =>
        //        {
        //            return new GetAccountResponse()
        //            {
        //                UserName = x.UserName,
        //                //Password = x.Password,
        //                FullName = x.FullName,
        //                Description = x.Description,
        //                Phone = x.Phone,
        //                Gender = x.Gender,
        //             //   BuildingId = x.BuildingId,
        //            };
        //        }
        //        ).ToList();
        //    return result;
        //}



        //public async Task<GetAccountResponse> GetUserById(Guid id)
        //{
        //    var getById = await dbContext.Accounts
        //       .SingleOrDefaultAsync(p => p.Id == id);
        //    if (getById != null)
        //    {
        //        var re = new GetAccountResponse()
        //        {
        //            UserName = getById.UserName,
        //           // Password = getById.Password,
        //            FullName = getById.FullName,
        //            Description = getById.Description,
        //            Phone = getById.Phone,
        //            Gender = getById.Gender,
        //            //BuildingId = getById.BuildingId,
        //        };
        //        return re;
        //    }
        //    return null;
        //}

        //public async Task<GetAccountResponse> GetUserByUserName(string name)
        //{
        //    var getByUserName = await dbContext.Accounts
        //       .SingleOrDefaultAsync(p => p.UserName.Equals(name));
        //    if (getByUserName != null)
        //    {
        //        var re = new GetAccountResponse()
        //        {
        //            UserName = getByUserName.UserName,
        //          //  Password = getByUserName.Password,
        //            FullName = getByUserName.FullName,
        //            Description = getByUserName.Description,
        //            Phone = getByUserName.Phone,
        //            Gender = getByUserName.Gender,
        //            //BuildingId = getByUserName.BuildingId,
        //        };
        //        return re;
        //    }
        //    return null;
        //}

        //public async Task<UpdateAccountResponse> UpdateUser(Guid id, UpdateAccountRequest userRequest)
        //{
        //    var upUser = await dbContext.Accounts
        //        .Where(p => p.Id == id)
        //        .SingleOrDefaultAsync();

        //    if (upUser == null) return null;
        //    if (id != userRequest.Id) return null;

        //    upUser.Password = userRequest.Password;
        //    upUser.FullName = userRequest.FullName;
        //    upUser.Description = userRequest.Description;
        //    upUser.Phone = userRequest.Phone;
        //    upUser.Gender = userRequest.Gender;
        //    upUser.BuildingId = userRequest.BuildingId;

        //    dbContext.Accounts.Update(upUser);
        //    await dbContext.SaveChangesAsync();

        //    var up = _mapper.Map<UpdateAccountResponse>(upUser);
        //    return up;
        //}
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopSecondHand.Data.RequestModels.AccountRequest;
using ShopSecondHand.Data.ResponseModels.AccountResponse;
using ShopSecondHand.Data.ResponseModels.WalletResponse;
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

        public async Task<IEnumerable<GetAccountResponse>> GetAccountByBuildingId(Guid id)
        {
            var user = await dbContext.Accounts.Where(p => p.Status == true)
                                                .Where(c => c.BuildingId.CompareTo(id) == 0)
                                                .ToListAsync();
            IEnumerable<GetAccountResponse> result = user.Select(
                x =>
                {
                    return new GetAccountResponse()
                    {
                        Id = x.Id,
                        UserName = x.UserName,
                        //Password = x.Password,
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

        public async Task<AccountWithWalletDTO> GetAccountWithWallet(Guid id)
        {
            var getById = await dbContext.Accounts
                .Where(p => p.Status == true)
               .SingleOrDefaultAsync(p => p.Id == id);
            var getWalletById = await dbContext.Wallets
               .SingleOrDefaultAsync(p => p.Id == id);
            if (getById != null)
            {
                var ra = new WalletDTO()
                {
                    Id = getWalletById.Id,
                    Balance = getWalletById.Balance,
                };

                var re = new AccountWithWalletDTO()
                {
                    Id = ra.Id,
                    UserName = getById.UserName,
                    // Password = getById.Password,
                    FullName = getById.FullName,
                    Description = getById.Description,
                    Phone = getById.Phone,
                    Gender = getById.Gender,
                    BuildingId = getById.BuildingId,
                    Wallet = ra
                };
                return re;
            }
            return null;
        }
        public async Task<Account> CreateAccountWithWallet(CreateAccountRequest userRequest)
        {
            var user = await dbContext.Accounts
                 .FirstOrDefaultAsync(p => p.UserName.Equals(userRequest.UserName));
            if (user != null)
                return null;
            var roleId = await dbContext.Roles
                 .FirstOrDefaultAsync(p => p.Name.Equals("USER"));
            var Id = Guid.NewGuid();
            Account userr = new Account();
            {
                userr.Id = Id;
                userr.UserName = userRequest.UserName;
                userr.Password = userRequest.Password;
                userr.FullName = userRequest.FullName;
                userr.Description = userRequest.Description;
                userr.Phone = userRequest.Phone;
                userr.Gender = userRequest.Gender;
                userr.RoleId = roleId.Id;
                userr.Status = true;
                userr.BuildingId = userRequest.BuildingId;
            };
            Wallet wallet = new Wallet();
            {
                wallet.Id = Id;
                wallet.Balance = 0;
            }
            dbContext.Accounts.AddAsync(userr);
            dbContext.Wallets.AddAsync(wallet);
            dbContext.SaveChangesAsync();
            //var re = _mapper.Map<CreateAccountResponse>(userr);
            return userr;
        }
        public async Task<CreateAccountResponse> CreateAccount(CreateAccountRequest userRequest)
        {
            var user = await dbContext.Accounts
                 .FirstOrDefaultAsync(p => p.UserName.Equals(userRequest.UserName));
            if (user != null)
                return null;
            var roleId = await dbContext.Roles
                 .FirstOrDefaultAsync(p => p.Name.Equals("USER"));
            var Id = Guid.NewGuid();
            Account userr = new Account();
            {
                userr.Id = Id;
                userr.UserName = "1a";
                userr.Password = "1a";
                userr.FullName = "1a";
                userr.Description = "1a";
                userr.Phone = "1a";
                userr.Gender = "1a";
                userr.RoleId = roleId.Id;
                userr.Status = true;
                userr.BuildingId = userRequest.BuildingId;
            };
            //Wallet wallet = new Wallet();
            //{
            //    wallet.Id = Id;
            //    wallet.Balance = 0;
            //}
            dbContext.Accounts.AddAsync(userr);
            //dbContext.Wallets.AddAsync(wallet);
            await dbContext.SaveChangesAsync();
            var re = new CreateAccountResponse()
            {
               // Id = userr.Id,
                UserName = userr.UserName,
                // Password = getById.Password,
                FullName = userr.FullName,
                Description = userr.Description,
                Phone = userr.Phone,
                Gender = userr.Gender,
                BuildingId = userr.BuildingId
            };
            return re;
            //var re = _mapper.Map<GetAccountResponse>(userr);
            //return re;
        }

        public void DeleteAccount(Guid id)
        {
            var delete = dbContext.Accounts
              .SingleOrDefault(p => p.Id == id);
            delete.Status = false;
            dbContext.Accounts.Update(delete);
            dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetAccountResponse>> GetAccount()
        {
            //get account with status la chua xoa
            var user = await dbContext.Accounts.Where(p => p.Status == true).ToListAsync();
            IEnumerable<GetAccountResponse> result = user.Select(
                x =>
                {
                    return new GetAccountResponse()
                    {
                        Id = x.Id,
                        UserName = x.UserName,
                        //Password = x.Password,
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



        public async Task<Account> UpdateAccount(Guid id, UpdateAccountRequest userRequest)
        {
            var upUser = await dbContext.Accounts
                .Where(p => p.Id == id)
                .Where(p => p.Status == true)
                .SingleOrDefaultAsync();

            if (upUser == null) return null;
            if (id != userRequest.Id) return null;

            upUser.Password = userRequest.Password;
            upUser.FullName = userRequest.FullName;
            upUser.Description = userRequest.Description;
            upUser.Phone = userRequest.Phone;
            upUser.Gender = userRequest.Gender;
            upUser.BuildingId = userRequest.BuildingId;

            dbContext.Accounts.Update(upUser);
            await dbContext.SaveChangesAsync();

            //var up = _mapper.Map<UpdateAccountResponse>(upUser);

            return upUser;
        }

        public async Task<GetAccountResponse> GetAccountById(Guid id)
        {
            var getById = await dbContext.Accounts
                 .SingleOrDefaultAsync(p => p.Id == id);
            if (getById != null)
            {
                var re = new GetAccountResponse()
                {
                    Id = getById.Id,
                    UserName = getById.UserName,
                    //Password = x.Password,
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
    }
}

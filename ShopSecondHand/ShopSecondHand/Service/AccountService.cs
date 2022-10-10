using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopSecondHand.Data.Common;
using ShopSecondHand.Data.RequestModels.AccountRequest;
using ShopSecondHand.Data.ResponseModels.AccountResponse;
using ShopSecondHand.Data.ResponseModels.WalletResponse;
using ShopSecondHand.Models;
using ShopSecondHand.Repository.AccountRepository;
using ShopSecondHand.Service.IService;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ShopSecondHand.Service
{
    public class AccountService : IAccountService
    {
        private readonly ShopSecondHandContext _dbContext;
        private readonly IMapper _mapper;

        private readonly IAccountRepository _userRepository;

        //private readonly IWalletService _walletService;

        public AccountService(IMapper mapper,
            IAccountRepository userRepository,
            //IWalletService walletService,
            ShopSecondHandContext dbContext
           )
        {
            _mapper = mapper;

            _userRepository = userRepository;

           // _walletService = walletService;

            _dbContext = dbContext;
        }
        //public async Task<GenericResult<UserWithWalletDTO>> CreateUser(CreateUserRequest payload)
        //{
        //    var targetAccount = _mapper.Map<User>(payload);

        //    // targetAccount.Status = AccountStatusEnum.INACTIVE;

        //    // targetAccount.CreatedTime = DateTime.UtcNow;
        //    //lay roleId
        //    var role = await _dbContext.Roles
        //         .FirstOrDefaultAsync(p => p.Name.Equals("User"));

        //    var hashPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(payload.Password);
        //    targetAccount.Password = hashPassword;
        //    targetAccount.RoleId = role.Id;

        //    _userRepository.Create(targetAccount);
        //    await _userRepository.SaveAsync();

        //    // Create Wallet
        //    var targetWallet = await _walletService.CreateNew(targetAccount.Id);

        //    var response = _mapper.Map<UserWithWalletDTO>(targetAccount);
        //    response.Wallet = _mapper.Map<WalletDTO>(targetWallet.Data);

        //    return GenericResult<UserWithWalletDTO>.Success(response);
        //}

        public async Task<GenericResult<List<GetAccountResponse>>> GetAll()
        {
            var targetAccounts = await _userRepository
                .Get()
                .AsNoTracking()
                .ToListAsync();

            var response = _mapper.Map<List<GetAccountResponse>>(targetAccounts);

            //foreach (var tempAccount in response)
            //{
            //    if (tempAccount.Role == RoleEnum.CANDIDATE)
            //    {
            //        var targetCandidate = await _accountRepository.GetCandidateById(tempAccount.Id);
            //        tempAccount.AvatarUrl = targetCandidate?.AvatarUrl;
            //    }
            //    else
            //    {
            //        var targetEnterprise = await _accountRepository.GetEnterpriseById(tempAccount.Id);
            //        tempAccount.LogoUrl = targetEnterprise?.LogoUrl;
            //    }
            //}

            return GenericResult<List<GetAccountResponse>>.Success(response);
        }

        public async Task<GenericResult<GetAccountResponse>> GetById(Guid id)
        {
            var account = await _userRepository.GetById(id);

            if (account == null)
                return GenericResult<GetAccountResponse>.Error((int)HttpStatusCode.NotFound,
                    "User is not found.");

            var response = _mapper.Map<GetAccountResponse>(account);

            //if (account.Role == RoleEnum.CANDIDATE)
            //{
            //    var targetCandidate = await _userRepository.GetCandidateById(account.Id);
            //    response.AvatarUrl = targetCandidate?.AvatarUrl;
            //}
            //else
            //{
            //    var targetEnterprise = await _userRepository.GetEnterpriseById(account.Id);
            //    response.LogoUrl = targetEnterprise?.LogoUrl;
            //}

            return GenericResult<GetAccountResponse>.Success(response);
        }

        public async Task<GenericResult<UpdateAccountResponse>> UpdateUser(Guid id, UpdateAccountRequest payload)
        {
            var account = await _userRepository.GetById(id);

            if (account == null)
                return GenericResult<UpdateAccountResponse>.Error((int)HttpStatusCode.NotFound,
                    "Account is not found.");

            account = _mapper.Map<Account>(payload);

            _userRepository.Update(account);
            await _userRepository.SaveAsync();

            var response = _mapper.Map<UpdateAccountResponse>(account);

            return GenericResult<UpdateAccountResponse>.Success(response);
        }
        public async Task<GenericResult<GetAccountResponse>> DeleteUser(Guid id)
        {
            var account = await _userRepository.GetById(id);

            if (account == null)
                return GenericResult<GetAccountResponse>.Error((int)HttpStatusCode.NotFound,
                    "Account is not found.");

            //if (account.Status == Entities.Enums.AccountStatusEnum.DELETED)
            //    return GenericResult<AccountDTO>.Error("V400_05",
            //        "Tài khoản này đã bị xóa.");

            //account.Status = Entities.Enums.AccountStatusEnum.DELETED;

            _userRepository.Delete(account);
            await _userRepository.SaveAsync();

            var response = _mapper.Map<GetAccountResponse>(account);

            return GenericResult<GetAccountResponse>.Success(response);
        }

        public async Task<GenericResult<List<GetAccountResponse>>> GetUserByBuildingId(Guid id)
        {
            var targeUser = await _userRepository.GetAccountByBuildingId(id);

           // var response = _mapper.Map<GetUserResponse>(targetVoiceDemos);

            var response = _mapper.Map<List<GetAccountResponse>>(targeUser);

            return GenericResult<List<GetAccountResponse>>.Success(response);

        }
    }
}

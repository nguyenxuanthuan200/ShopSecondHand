using ShopSecondHand.Data.Common;
using ShopSecondHand.Data.ResponseModels.WalletResponse;
using System;
using System.Threading.Tasks;

namespace ShopSecondHand.Service.IService
{
    public interface IWalletService
    {
       // public Task<GenericResult<WalletWithTransactionsDTO>> GetById(Guid id);
        //public Task<GenericResult<List<WalletWithTransactionsDTO>>> GetAll();
        public Task<GenericResult<WalletDTO>> CreateNew(Guid id);
    }
}

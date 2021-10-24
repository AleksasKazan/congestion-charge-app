using Contracts.Models;

namespace Domain.Services
{
    public interface IFeeCalcService
    {
        ReceiptResponseModel FeeCalculator(ReceiptRequestModel receipt);
    }
}

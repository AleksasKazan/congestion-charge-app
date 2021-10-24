using Contracts.Models;

namespace Domain.Services
{
    public interface IPrintService
    {
        void Print(ReceiptResponseModel receipt);
    }
}

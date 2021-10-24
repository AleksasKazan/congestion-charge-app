using System;
using System.Collections.Generic;
using Contracts.Enums;
using Contracts.Models;

namespace Services
{
    public interface IReceiptsService
    {
        IEnumerable<ReceiptResponseModel> GetAll();

        void Create(string name, VehicleType vehicleType, DateTime start, DateTime end);
    }
}

using System;
using System.Collections.Generic;
using Contracts.Entities;
using Contracts.Enums;
using Contracts.Models;
using Domain.Services;
using Persistence.Models.WriteModels;
using Persistence.Repositories;

namespace Services
{
    public class ReceiptsService : IReceiptsService
    {
        private readonly IReceiptsRepository _receiptsRepository;
        private readonly IFeeCalcService _feeCalcService;
        private readonly Fee _fee;

        public ReceiptsService(IReceiptsRepository receiptsRepository, IFeeCalcService feeCalcService, Fee fee)
        {
            _receiptsRepository = receiptsRepository;
            _feeCalcService = feeCalcService;
            _fee = fee;
        }

        public IEnumerable<ReceiptResponseModel> GetAll()
        {
            var trips = _receiptsRepository.GetAll();

            if (trips is null)
            {
                throw new Exception("Not found");
            }

            List<ReceiptResponseModel> receipts = new ();

            foreach (var trip in trips)
            {
                var fee = _fee.GetCharge(trip.VehicleType);
                
                var receipt = _feeCalcService.FeeCalculator(new ReceiptRequestModel
                {
                    Name = trip.Name,
                    Start = trip.Start,
                    End = trip.End,
                    VehicleType = trip.VehicleType,
                    AmRate = fee.Item1,
                    PmRate = fee.Item2
                });
                receipts.Add(receipt);
            }

            return receipts;
        }

        public void Create(string name, VehicleType vehicleType, DateTime start, DateTime end)
        {
            _receiptsRepository.Save(new TripWriteModel
            {
                Id = Guid.NewGuid(),
                Name = name,
                VehicleType = vehicleType,
                Start = start,
                End = end
            });
        }
    }
}

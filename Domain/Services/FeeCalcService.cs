using System;
using Contracts.Entities;
using Contracts.Models;

namespace Domain.Services
{
    public class FeeCalcService : IFeeCalcService
    {
        private readonly TimeSpan am7 = TimeTable.Am7;
        private readonly TimeSpan pm12 = TimeTable.Pm12;
        private readonly TimeSpan pm7 = TimeTable.Pm7;
        
        public ReceiptResponseModel FeeCalculator(ReceiptRequestModel receipt)
        {
            TimeSpan amTime = TimeSpan.Zero;
            TimeSpan pmTime = TimeSpan.Zero;

            for (DateTime date = receipt.Start.Date; date.Date <= receipt.End.Date; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday
                    || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }

                TimeSpan start = receipt.Start.Date.Equals(date) && receipt.Start.TimeOfDay > am7 ? receipt.Start.TimeOfDay : am7;
                TimeSpan end = receipt.End.Date.Equals(date) && receipt.End.TimeOfDay < pm7 ? receipt.End.TimeOfDay : pm7;

                TimeSpan tempStart = start > pm12 ? start : pm12;
                TimeSpan tempEnd = end < pm12 ? end : pm12;

                amTime += (pm12 - start) > TimeSpan.Zero && end > start ? tempEnd - start : TimeSpan.Zero;
                pmTime += (end - pm12) > TimeSpan.Zero ? end - tempStart : TimeSpan.Zero;
            }

            decimal amCharge = Math.Floor((decimal)amTime.TotalHours * receipt.AmRate * 10) / 10;
            decimal pmCharge = Math.Floor((decimal)pmTime.TotalHours * receipt.PmRate * 10) / 10;

            return new ReceiptResponseModel
            {
                Name = receipt.Name,
                VehicleType = receipt.VehicleType,
                Start = receipt.Start,
                End = receipt.End,
                MorningFeeAmount = amCharge,
                AfternoonFeeAmount = pmCharge,
                TotalAmount = amCharge + pmCharge,
                AmTime = amTime,
                PmTime = pmTime
            };
        }
    }
}

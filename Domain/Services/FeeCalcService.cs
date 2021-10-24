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
            TimeSpan start = receipt.Start.TimeOfDay;
            TimeSpan end = receipt.End.TimeOfDay;

            //Single day
            if (receipt.Start.Date.Equals(receipt.End.Date)
                && receipt.Start.DayOfWeek != DayOfWeek.Saturday
                && receipt.Start.DayOfWeek != DayOfWeek.Sunday)
            {
                if (start < am7
                    && end > am7
                    && end < pm12)
                {
                    amTime += end - am7;
                }
                else if (start < am7
                    && end > pm12
                    && end < pm7)
                {
                    amTime += pm12 - am7;
                    pmTime += end - pm12;
                }
                else if (start < am7
                    && end > pm7)
                {
                    amTime += pm12 - am7;
                    pmTime += pm7 - pm12;
                }
                else if (start > am7
                    && start < pm12
                    && end > pm7)
                {
                    amTime += pm12 - start;
                    pmTime += pm7 - pm12;
                }
                else if (start > am7
                    && start < pm12
                    && end < pm12)
                {
                    amTime += end - start;
                }
                else if (start > am7
                    && start < pm12
                    && end > pm12
                    && end < pm7)
                {
                    amTime += pm12 - start;
                    pmTime += end - pm12;
                }
                else if (start > pm12
                    && start < pm7
                    && end < pm7)
                {
                    pmTime += end - start;
                }
                else if (start > pm12
                    && start < pm7
                    && end > pm7)
                {
                    pmTime += pm7 - start;
                }
            }

            //First and Last days
            else if (!receipt.Start.Date.Equals(receipt.End.Date)
                && receipt.Start.DayOfWeek != DayOfWeek.Saturday
                && receipt.Start.DayOfWeek != DayOfWeek.Sunday)
            {
                //First day
                if (start < am7)
                {
                    amTime += pm12 - am7;
                    pmTime += pm7 - pm12;
                }
                else if (start > am7
                    && start < pm12)
                {
                    amTime += pm12 - start;
                    pmTime += pm7 - pm12;
                }
                else if (start > pm12
                    && start < pm7)
                {
                    pmTime += (pm7 - start);
                }
                //Last day
                if (end > pm7)
                {
                    amTime += pm12 - am7;
                    pmTime += pm7 - pm12;
                }
                else if (end > am7
                    && end < pm12)
                {
                    amTime += end - am7;
                }
                else if (end > pm12
                    && end < pm7)
                {
                    amTime += pm12 - am7;
                    pmTime += end - pm12;
                }
            }

            //Full days
            for (DateTime date = receipt.Start.AddDays(1); date.Date < receipt.End.Date; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday
                    || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }
                amTime += pm12 - am7;
                pmTime += pm7 - pm12;
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

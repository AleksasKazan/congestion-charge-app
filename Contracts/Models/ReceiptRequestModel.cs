using System;
using Contracts.Enums;

namespace Contracts.Models
{
    public class ReceiptRequestModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public VehicleType VehicleType { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public decimal AmRate { get; set; }

        public decimal PmRate { get; set; }

        public decimal MorningFeeAmount { get; set; }

        public decimal AfternoonFeeAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public TimeSpan AmTime { get; set; }

        public TimeSpan PmTime { get; set; }
    }
}

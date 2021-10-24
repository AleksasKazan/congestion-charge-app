using System;
using Contracts.Enums;

namespace Contracts.Entities
{
    public class Fee
    {
        public decimal AmRate { get; set; }
        public decimal PmRate { get; set; }

        public Tuple<decimal, decimal>  GetCharge(VehicleType vehicleType)
        {
            if (vehicleType == VehicleType.Car)
            {
                AmRate = 2.0M;
                PmRate = 2.5M;
            }
            else if (vehicleType == VehicleType.Motorbike)
            {
                AmRate = 1.0M;
                PmRate = 1.0M;
            }

            return Tuple.Create(AmRate, PmRate);
        }
    }
}

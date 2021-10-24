using System;
using Contracts.Enums;

namespace Persistence.Models.ReadModels
{
    public class TripReadModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public VehicleType VehicleType { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}

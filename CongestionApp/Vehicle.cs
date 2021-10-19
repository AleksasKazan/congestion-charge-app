using System;
using System.Collections.Generic;
using Contracts.Enums;

namespace CongestionApp
{
    public class Vehicle
    {
        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public decimal AmRate { get; set; }

        public decimal PmRate { get; set; }
    }

    public class Car : Vehicle
    {
        //public decimal AmRate => 2;

        //public new decimal AmRate => 2;

        //public decimal PmRate => 2.50M;
    }

    public class Motorbike : Vehicle
    {
        //public decimal AmRate { get => 1; }

        //public decimal PmRate => 1;
    }
}

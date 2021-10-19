using System;
using Contracts.Enums;
using System.Globalization;
using System.Collections.Generic;

namespace CongestionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var car = new Car
            {
                Name = "Car",
                Start = new DateTime(2008, 04, 24, 11, 32, 0),
                End = new DateTime(2008, 04, 24, 14, 42, 0),
                AmRate = 2,
                PmRate = 2.50M
            };

            var motorbike = new Motorbike
            {
                Name = "Motorbike",
                Start = new DateTime(2008, 04, 24, 17, 00, 0),
                End = new DateTime(2008, 04, 24, 22, 11, 0),
                AmRate = 1,
                PmRate = 1
            };

            var van = new Car
            {
                Name = "Van",
                Start = new DateTime(2008, 04, 25, 10, 23, 0),
                End = new DateTime(2008, 04, 28, 09, 02, 0),
                AmRate = 2,
                PmRate = 2.50M
            };

            FeeCalculator(car);
            FeeCalculator(motorbike);
            FeeCalculator(van);

            //List<Vehicle> vehicles = new List<Vehicle>();
            //vehicles.Add(car);
            //vehicles.Add(motorbike);
            //vehicles.Add(van);

            //VehicleType vehicle;
            //DateTime vehicle.Start;
            //DateTime vehicle.End;

            //Console.WriteLine("Welcome to Congestion charge app");

            //while (true)
            //{
            //    Console.WriteLine("Select vehicle type please:");
            //    Console.WriteLine("1 - motorbike");
            //    Console.WriteLine("2 - car");
            //    Enum.TryParse(Console.ReadLine(), out vehicle);

            //    Console.WriteLine("Enter travel period vehicle.Start date and time please (dd/MM/yyyy HH:mm):");
            //    DateTime.TryParse(Console.ReadLine(), out vehicle.Start);

            //    Console.WriteLine("Enter travel period vehicle.End date and time please (dd/MM/yyyy HH:mm):");
            //    DateTime.TryParse(Console.ReadLine(), out vehicle.End);

            //    switch (vehicle)
            //    {
            //        case VehicleType.Motorbike:
            //            FeeCalculator(vehicle.Start, vehicle.End);
            //            break;

            //        case VehicleType.Car:
            //            FeeCalculator(vehicle.Start, vehicle.End);
            //            break;
            //        default:
            //            break;
            //    }
            //}
        }

        static void FeeCalculator(Vehicle vehicle)
        {
            TimeSpan am7 = TimeSpan.FromHours(7), pm12 = TimeSpan.FromHours(12), pm7 = TimeSpan.FromHours(19);
            TimeSpan amTime = TimeSpan.Zero;
            TimeSpan pmTime = TimeSpan.Zero;

            for (DateTime date = vehicle.Start.AddDays(1); date.Date < vehicle.End.Date; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }
                amTime += pm12 - am7;
                pmTime += pm7 - pm12;
            }

            if (vehicle.Start.Date == vehicle.End.Date
                && vehicle.Start.DayOfWeek != DayOfWeek.Saturday
                && vehicle.Start.DayOfWeek != DayOfWeek.Sunday)
            {
                if (vehicle.Start.TimeOfDay < am7                   
                    && vehicle.End.TimeOfDay > am7
                    && vehicle.End.TimeOfDay < pm12)
                {
                    amTime = vehicle.End.TimeOfDay - am7;
                }
                else if (vehicle.Start.TimeOfDay > am7
                    && vehicle.Start.TimeOfDay < pm12
                    && vehicle.End.TimeOfDay < pm7)
                {
                    amTime = pm12 - am7;
                    pmTime = vehicle.End.TimeOfDay - pm12;
                }
                if (vehicle.Start.TimeOfDay > am7
                    && vehicle.Start.TimeOfDay < pm12
                    && vehicle.End.TimeOfDay < pm12)
                {
                    amTime = vehicle.End.TimeOfDay - vehicle.Start.TimeOfDay;
                }
                else if (vehicle.Start.TimeOfDay > am7
                    && vehicle.Start.TimeOfDay < pm12
                    && vehicle.End.TimeOfDay > pm12
                    && vehicle.End.TimeOfDay < pm7)
                {
                    amTime = pm12 - vehicle.Start.TimeOfDay;
                    pmTime = vehicle.End.TimeOfDay - pm12;
                }
                if (vehicle.Start.TimeOfDay > pm12
                    && vehicle.Start.TimeOfDay < pm7
                    && vehicle.End.TimeOfDay < pm7)
                {
                    pmTime = vehicle.End.TimeOfDay - vehicle.Start.TimeOfDay;
                }
                else if (vehicle.Start.TimeOfDay > pm12
                    && vehicle.Start.TimeOfDay < pm7
                    && vehicle.End.TimeOfDay > pm7)
                {
                    pmTime = pm7 - vehicle.Start.TimeOfDay;
                }
            }
            else if (vehicle.Start.Date != vehicle.End.Date
                && vehicle.Start.DayOfWeek != DayOfWeek.Saturday
                && vehicle.Start.DayOfWeek != DayOfWeek.Sunday)
            {
                if (vehicle.Start.TimeOfDay < am7)
                {
                    amTime += pm12 - am7;
                    pmTime += pm7 - pm12;
                }
                else if (vehicle.Start.TimeOfDay > am7
                    && vehicle.Start.TimeOfDay < pm12)
                {
                    amTime += pm12 - vehicle.Start.TimeOfDay;
                    pmTime += pm7 - pm12;
                }
                else if (vehicle.Start.TimeOfDay > pm12
                    && vehicle.Start.TimeOfDay < pm7)
                {
                    pmTime += (pm7 - vehicle.Start.TimeOfDay);
                }

                if (vehicle.End.TimeOfDay > pm7)
                {
                    amTime += pm12 - am7;
                    pmTime += pm7 - pm12;
                }
                else if (vehicle.End.TimeOfDay > am7
                    && vehicle.End.TimeOfDay < pm12)
                {
                    amTime += vehicle.End.TimeOfDay - am7;
                }
                else if (vehicle.End.TimeOfDay > pm12
                    && vehicle.End.TimeOfDay < pm7)
                {
                    amTime += pm12 - am7;
                    pmTime += vehicle.End.TimeOfDay - pm12;
                }
            }
            decimal amCharge = (decimal)amTime.TotalHours * vehicle.AmRate;
            decimal pmCharge = (decimal)pmTime.TotalHours * vehicle.PmRate;
            decimal totalCharge = (Math.Floor(amCharge * 10) / 10) + (Math.Floor(pmCharge * 10) / 10);

            Console.WriteLine($"{vehicle.Name}: {vehicle.Start:dd/MM/yyyy HH:mm} - {vehicle.End:dd/MM/yyyy HH:mm}");
            Console.Write("Charge for ");
            Console.Write("{0:%h}h {0:%m}m", amTime);
            Console.WriteLine(" (AM rate): " + string.Format(new CultureInfo("en-GB"), "{0:C}", Math.Floor(amCharge * 10) / 10));
            Console.Write("Charge for ");
            Console.Write("{0:%h}h {0:%m}m", pmTime);
            Console.WriteLine(" (PM rate): " + string.Format(new CultureInfo("en-GB"), "{0:C}", Math.Floor(pmCharge * 10) / 10));
            Console.WriteLine("Total Charge: " + string.Format(new CultureInfo("en-GB"), "{0:C}", totalCharge));
            Console.WriteLine();
        }
    }
}

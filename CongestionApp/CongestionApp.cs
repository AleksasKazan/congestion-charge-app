using System;
using System.Threading.Tasks;
using Contracts.Enums;
using Domain.Services;
using Services;

namespace CongestionApp
{
    public class CongestionApp
    {
        private readonly IReceiptsService _receiptsService;
        private readonly IPrintService _printService;

        public CongestionApp(IReceiptsService receiptsService, IPrintService printService)
        {
            _receiptsService = receiptsService;
            _printService = printService;
        }

        public Task Start()
        {
            string name;
            VehicleType vehicleType;
            DateTime start;
            DateTime end;
            ConsoleKeyInfo cki;

            Console.WriteLine("Welcome to Congestion charge app");
            while (true)
            {
                Console.WriteLine("\nAvailable commands:");
                Console.WriteLine("1 - List all the receipts");
                Console.WriteLine("2 - Add a new travel");

                cki = Console.ReadKey();
                switch (cki.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();

                        var receipts =_receiptsService.GetAll();

                        foreach (var receipt in receipts)
                        {
                            _printService.Print(receipt);
                        }
                        break;

                    case ConsoleKey.D2:
                        Console.Clear();

                        Console.WriteLine("Enter vehicle name please:");
                        name = Console.ReadLine();

                        Console.WriteLine("Select vehicle type please:");
                        Console.WriteLine("0 - car");
                        Console.WriteLine("1 - motorbike");
                        Enum.TryParse(Console.ReadLine(), out vehicleType);

                        Console.WriteLine("Enter travel start date and time please (MM/dd/yyyy HH:mm):");
                        DateTime.TryParse(Console.ReadLine(), out start);

                        Console.WriteLine("Enter travel end date and time please (MM/dd/yyyy HH:mm):");
                        DateTime.TryParse(Console.ReadLine(), out end);

                        _receiptsService.Create(name, vehicleType, start, end);
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
    
using System;
using System.Globalization;
using Contracts.Models;

namespace Domain.Services
{
    public class PrintService : IPrintService
    {
        public void Print(ReceiptResponseModel receipt)
        {
            Console.WriteLine($"{receipt.Name}: {receipt.Start:dd/MM/yyyy HH:mm} - {receipt.End:dd/MM/yyyy HH:mm}");
            Console.Write("Charge for ");
            Console.Write("{0:%d}d {0:%h}h {0:%m}m", receipt.AmTime);
            Console.WriteLine(" (AM rate): " + string.Format(new CultureInfo("en-GB"), "{0:C}", receipt.MorningFeeAmount));
            Console.Write("Charge for ");
            Console.Write("{0:%d}d {0:%h}h {0:%m}m", receipt.PmTime);
            Console.WriteLine(" (PM rate): " + string.Format(new CultureInfo("en-GB"), "{0:C}", receipt.AfternoonFeeAmount));
            Console.WriteLine("Total Charge: " + string.Format(new CultureInfo("en-GB"), "{0:C}", receipt.TotalAmount));
            Console.WriteLine();
        }
    }
}

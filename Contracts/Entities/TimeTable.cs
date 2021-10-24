using System;
namespace Contracts.Entities
{
    public class TimeTable
    {
        public static TimeSpan Am7 { get; set; } = TimeSpan.FromHours(7);

        public static TimeSpan Pm12 { get; set; } = TimeSpan.FromHours(12);

        public static TimeSpan Pm7 { get; set; } = TimeSpan.FromHours(19);
    }
}

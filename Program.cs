using System;
using System.Diagnostics;

namespace NoahnFollowers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now + " - Starting up...");

            Stopwatch s = new Stopwatch();
            s.Start();

            while (s.Elapsed < TimeSpan.FromDays(365))
            {
                Twitter.MonitorFollowers();
            }

            s.Stop();
        }
    }
}


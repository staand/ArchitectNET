using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using ArchitectNET.Core;

namespace ArchitectNET.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const int n = 1000000000;
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < n; i++)
            {
                Guard.ArgumentNotNull(args, nameof(args));
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedTicks);
            Console.WriteLine(sw.ElapsedTicks / (double) n);
        }
    }
}
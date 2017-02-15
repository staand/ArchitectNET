using System.Diagnostics;
using ArchitectNET.Core;
using static System.Console;

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
            WriteLine(sw.ElapsedTicks);
            WriteLine(sw.ElapsedTicks / (double) n);
        }
    }
}
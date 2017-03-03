using System.Diagnostics;
using ArchitectNET.Core;
using ArchitectNET.Core.Collections.Support;
using ArchitectNET.Core.Dynamic;
using static System.Console;

namespace ArchitectNET.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var key1 = new UnorderedCompoundKey<int>(1, 2, 3, 4, 5);
            var key2 = new UnorderedCompoundKey<int>(1, 2, 4, 5, 3);
            var key3 = new UnorderedCompoundKey<int>(1, 2, 3, 4, 6);

            var b1 = key1.Equals(key2);
            var b2 = key2.Equals(key3);

            IType x = null;
            var b = x.IsInt64();
        }
    }
}
using System;
using System.Linq;

namespace Tester
{
    public class Tester
    {
        static void Main()
        {
            const string rna = "AUGGCCAUGGCGCCCAGAACUGAGAUCAAUAGUACCCGUAUUAACGGGUGA";
            Console.WriteLine(rna.Length);

            var pointer = 0;
            const int increment = 9;
            while (pointer < rna.Length)
            {
                var subset = rna.Skip(pointer).Take(increment).ToArray();
                pointer += increment;
                Console.WriteLine(new string(subset));
            }

            Console.ReadLine();
        }
    }
}

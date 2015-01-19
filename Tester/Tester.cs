using System;
using System.Linq;
using BCompute;

namespace Tester
{
    public class Tester
    {
        static void Main()
        {
            const string gandalfPath = @"M:\dev\rosalind\GuanineCytosineContent\rosalind_gc_orig.txt";
            const string macbookPath = @"C:\Users\rianjs\dev\Rosalind\GuanineCytosineContent\rosalind_gc.txt";
            const string active = gandalfPath;
            var parser = new FastaParser(active, AlphabetType.StrictDna);
            var results = parser.Parse().ToList();

            Console.WriteLine(results.Count());
            Console.ReadLine();
        }
    }
}

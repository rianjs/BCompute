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
            const string active = macbookPath;
            var parser = new FastaParser(new Uri(active), AlphabetType.StrictDna);
            var results = parser.Parse().ToList();

            foreach (var sequence in results)
            {
                Console.WriteLine(sequence.Tags.First());
                Console.WriteLine("{0:N0}", sequence.Sequence.Length);
            }

            Console.WriteLine(results.Count);
            Console.ReadLine();
        }
    }
}

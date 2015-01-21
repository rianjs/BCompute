using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BCompute;

namespace Tester
{
    public class Tester
    {
        static void Main()
        {
            const string gandalfPath = @"M:\dev\rosalind\GuanineCytosineContent\rosalind_gc_orig.txt";
            const string macbookPath = @"C:\Users\rianjs\dev\Rosalind\ConsensusMatrix\rosalind_cons.txt";
            const string active = macbookPath;
            var parser = new FastaParser(new Uri(active), AlphabetType.StrictDna);

            var builder = new ConsensusBuilder(parser.Parse());
            var consensus = builder.GetConsensusMatrix();

            Console.WriteLine(builder.ToString());
            Console.WriteLine(builder.GetConsensusString());

            var output = builder + Environment.NewLine + builder.GetConsensusString();

            File.WriteAllText(@"C:\Users\rianjs\dev\Rosalind\ConsensusMatrix\rosalind_output.txt", output);

            Console.ReadLine();
        }
    }
}

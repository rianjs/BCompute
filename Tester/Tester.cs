using System;
using System.Linq;
using BCompute;

namespace Tester
{
    public class Tester
    {
        static void Main()
        {
            const string sequence = "GATATATGCATATACTT";
            const string motif = "ATAT";

            var dna = new DnaSequence(sequence, AlphabetType.StrictDna);
            var indices = dna.FindMotif(motif);

            foreach (var index in indices)
            {
                Console.Write("{0} ", index + 1);
            }

            Console.ReadLine();
        }
    }
}

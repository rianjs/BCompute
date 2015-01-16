using System;
using System.Collections.Generic;

namespace BCompute
{
    public class RnaSequence : NucleotideSequence
    {
        public RnaSequence(string rawBasePairs, AlphabetType alphabet, GeneticCode geneticCode = GeneticCode.Standard)
            : base(rawBasePairs, alphabet, geneticCode)
        {
            switch (alphabet)
            {
                case AlphabetType.AmbiguousRna:
                    break;
                case AlphabetType.StrictRna:
                    break;
                default:
                    throw new ArgumentException(String.Format(InvalidAlphabetForSequenceType, alphabet, GetType()));
            }
        }

        internal static RnaSequence FastRnaSequence(string safeSequence, AlphabetType alphabet, GeneticCode geneticCode, Dictionary<Nucleotide, long> symbolCounts)
        {
            return new RnaSequence(safeSequence, alphabet, geneticCode, symbolCounts);
        }

        internal RnaSequence(string safeSequence, AlphabetType alphabet, GeneticCode geneticCode, Dictionary<Nucleotide, long> symbolCounts)
            : base(safeSequence, alphabet, geneticCode, symbolCounts) { }

        public override NucleotideSequence Transcribe()
        {
            var alphabet = ActiveAlphabet == AlphabetType.StrictRna ? AlphabetType.StrictDna : AlphabetType.AmbiguousDna;
            var newSymbolCounts = new Dictionary<Nucleotide, long>(SymbolCounts);
            var count = SymbolCounts[Nucleotide.Uracil];
            newSymbolCounts.Remove(Nucleotide.Uracil);
            newSymbolCounts.Add(Nucleotide.Thymine, count);
            var newSequence = Sequence.Replace((char)Nucleotide.Uracil, (char)Nucleotide.Thymine);
            return DnaSequence.FastDnaSequence(newSequence, alphabet, GeneticCode, newSymbolCounts);
        }
    }
}

using System;
using System.Collections.Generic;

namespace BCompute
{
    public class DnaSequence : NucleotideSequence
    {
        public DnaSequence(string rawBasePairs, AlphabetType alphabet, GeneticCode geneticCode = GeneticCode.Standard)
            : base(rawBasePairs, alphabet, geneticCode)
        {
            switch (alphabet)
            {
                case AlphabetType.AmbiguousDna:
                    break;
                case AlphabetType.StrictDna:
                    break;
                default:
                    throw new ArgumentException(String.Format(InvalidAlphabetForSequenceType, alphabet, GetType()));
            }
        }

        internal static DnaSequence FastDnaSequence(string safeSequence, AlphabetType alphabet, GeneticCode geneticCode,
            Dictionary<Nucleotide, long> symbolCounts)
        {
            return new DnaSequence(safeSequence, alphabet, geneticCode, symbolCounts);
        }

        internal DnaSequence(string safeSequence, AlphabetType alphabet, GeneticCode geneticCode, Dictionary<Nucleotide, long> symbolCounts)
            : base(safeSequence, alphabet, geneticCode, symbolCounts) { }

        public override NucleotideSequence Transcribe()
        {
            var alphabet = ActiveAlphabet == AlphabetType.StrictDna ? AlphabetType.StrictRna : AlphabetType.AmbiguousRna;
            var newSymbolCounts = new Dictionary<Nucleotide, long>(SymbolCounts);
            var count = SymbolCounts[Nucleotide.Thymine];
            newSymbolCounts.Remove(Nucleotide.Thymine);
            newSymbolCounts.Add(Nucleotide.Uracil, count);
            var newSequence = Sequence.Replace((char) Nucleotide.Thymine, (char) Nucleotide.Uracil);
            return RnaSequence.FastRnaSequence(newSequence, alphabet, GeneticCode, newSymbolCounts);
        }
    }
}

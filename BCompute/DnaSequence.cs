using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.InteropServices;
using BCompute.Data.Alphabets;
using BCompute.Data.GeneticCode;

namespace BCompute
{
    public class DnaSequence : NucleotideSequence
    {
        public DnaSequence(string rawBasePairs, AlphabetType alphabet, GeneticCode geneticCode) : base(rawBasePairs, alphabet, geneticCode) { }

        public static RnaSequence ConvertToRna(DnaSequence dnaSequence)
        {
            var newSequence = new char[dnaSequence.Sequence.Length];

            for(var i = 0; i < dnaSequence.Sequence.Length; i++)
            {
                char newLetter;
                switch (dnaSequence.Sequence[i])
                {
                    case 't':
                        newLetter = 'u';
                        break;
                    case 'T':
                        newLetter = 'U';
                        break;
                    default:
                        newLetter = dnaSequence.Sequence[i];
                        break;
                }
                newSequence[i] = newLetter;
            }
            return new RnaSequence(new string(newSequence));
        }

        public static ImmutableHashSet<char> GetAllowedNucleotides
        {
            get
            {
                //ToDo: Fix this
                return new HashSet<char> { 'A', 'T', 'G', 'C' }.ToImmutableHashSet();
            }
        }

        public long ThymineCount
        {
            get { return CodeCounts['T']; }
        }

        internal static DnaSequence FastDnaSequence(string safeSequence, AlphabetType alphabet, GeneticCode geneticCode,
            Dictionary<Nucleotide, long> symbolCounts)
        {
            return new DnaSequence(safeSequence, alphabet, geneticCode, symbolCounts);
        }

        internal DnaSequence(string safeSequence, AlphabetType alphabet, GeneticCode geneticCode, Dictionary<Nucleotide, long> symbolCounts)
            : base(safeSequence, alphabet, geneticCode, symbolCounts)
        {
            //Convert the symbol counts...
            //Get the complementary symbol (T -> A)
            //Get the count of its complement
            //Fill in the new dictionary(complement, complementCount)

            var newSymbolCounts = new Dictionary<Nucleotide, long>(symbolCounts.Count);
            foreach (var symbol in symbolCounts)
            {
                var complement = NucleotideAlphabet.ComplementTable[symbol.Key];
                var complementCount = symbolCounts[complement];
                newSymbolCounts.Add(complement, complementCount);
            }
            SymbolCounts = newSymbolCounts;
        }
    }
}

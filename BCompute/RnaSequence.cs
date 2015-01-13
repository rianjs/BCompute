using System.Collections.Generic;
using System.Collections.Immutable;
using BCompute.Data.Alphabets;
using BCompute.Data.GeneticCode;

namespace BCompute
{
    public class RnaSequence : NucleotideSequence
    {
        public RnaSequence(string rawBasePairs) : base(rawBasePairs) { }

        protected override sealed ImmutableDictionary<char, char> BasePairComplements
        {
            get { return Maps.RnaComplements; }
        }

        public override sealed ISet<char> AllowedCodes
        {
            get { return GetAllowedNucleotides; }
        }

        public static DnaSequence ConvertToDna(RnaSequence rnaSequence)
        {
            var newSequence = new char[rnaSequence.Sequence.Length];

            for (var i = 0; i < rnaSequence.Sequence.Length; i++)
            {
                char newLetter;
                switch (rnaSequence.Sequence[i])
                {
                    case 'u':
                        newLetter = 't';
                        break;
                    case 'U':
                        newLetter = 'T';
                        break;
                    default:
                        newLetter = rnaSequence.Sequence[i];
                        break;
                }
                newSequence[i] = newLetter;
            }
            return new DnaSequence(new string(newSequence));
        }

        public static ImmutableHashSet<char> GetAllowedNucleotides
        {
            get
            {
                //ToDo: Fix this
                return new HashSet<char> { 'A', 'U', 'G', 'C' }.ToImmutableHashSet();
            }
        }

        public long UracilCount
        {
            get { return CodeCounts['U']; }
        }

        internal static RnaSequence FastRnaSequence(string safeSequence, AlphabetType alphabet, GeneticCode geneticCode, Dictionary<Nucleotide, long> symbolCounts)
        {
            return new RnaSequence(safeSequence, alphabet, geneticCode, symbolCounts);
        }

        internal RnaSequence(string safeSequence, AlphabetType alphabet, GeneticCode geneticCode, Dictionary<Nucleotide, long> symbolCounts)
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

using System.Collections.Generic;
using System.Collections.Immutable;
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
    }
}

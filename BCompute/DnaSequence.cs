using System.Collections.Generic;
using System.Collections.Immutable;

namespace BCompute
{
    public class DnaSequence : NucleotideSequence
    {
        public DnaSequence(string rawBasePairs) : base(rawBasePairs) { }

        protected override sealed ImmutableDictionary<char, char> BasePairComplements
        {
            get { return Maps.DnaComplements; }
        }

        public override sealed ISet<char> AllowedCodes
        {
            get { return GetAllowedNucleotides; }
        }

        public static RnaSequence ConvertToRna(DnaSequence dnaSequence)
        {
            var convertedNucleotides = dnaSequence.Sequence.Replace("T", "U");
            return new RnaSequence(convertedNucleotides);
        }

        public static ImmutableHashSet<char> GetAllowedNucleotides
        {
            get
            {
                return new HashSet<char> { 'A', 'T', 'G', 'C' }.ToImmutableHashSet();
            }
        }

        public long ThymineCount
        {
            get { return CodeCounts['T']; }
        }
    }
}

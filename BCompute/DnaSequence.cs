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

        public override sealed ImmutableHashSet<char> AllowedNucleotides
        {
            get { return GetAllowedNucleotides; }
        }

        public static RnaSequence ConvertToRna(DnaSequence dnaSequence)
        {
            var convertedNucleotides = dnaSequence.BasePairs.Replace("T", "U");
            return new RnaSequence(convertedNucleotides);
        }

        public static ImmutableHashSet<char> GetAllowedNucleotides
        {
            get
            {
                return new HashSet<char> { 'A', 'T', 'G', 'C' }.ToImmutableHashSet();
            }
        }

        public ulong ThymineCount
        {
            get { return NucleotideCounts['T']; }
        }
    }
}

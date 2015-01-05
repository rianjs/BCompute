using System.Collections.Generic;
using System.Collections.Immutable;

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
            var convertedNucleotides = rnaSequence.Sequence.Replace("U", "T");
            return new DnaSequence(convertedNucleotides);
        }

        public static ImmutableHashSet<char> GetAllowedNucleotides
        {
            get
            {
                return new HashSet<char> { 'A', 'U', 'G', 'C' }.ToImmutableHashSet();
            }
        }

        public long UracilCount
        {
            get { return CodeCounts['U']; }
        }
    }
}

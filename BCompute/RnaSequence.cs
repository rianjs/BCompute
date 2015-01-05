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

        public override sealed ImmutableHashSet<char> AllowedNucleotides
        {
            get { return GetAllowedNucleotides; }
        }

        public static DnaSequence ConvertToDna(RnaSequence rnaSequence)
        {
            var convertedNucleotides = rnaSequence.BasePairs.Replace("U", "T");
            return new DnaSequence(convertedNucleotides);
        }

        public static ImmutableHashSet<char> GetAllowedNucleotides
        {
            get
            {
                return new HashSet<char> { 'A', 'U', 'G', 'C' }.ToImmutableHashSet();
            }
        }

        public ulong UracilCount
        {
            get { return NucleotideCounts['U']; }
        }

        public AminoSequence GetAminoSequence()
        {
            var aminoSequence = AminoSequence.ConvertRnaToAminoSequence(this);
            return aminoSequence;
        }
    }
}

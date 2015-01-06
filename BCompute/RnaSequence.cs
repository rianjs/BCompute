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
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace BCompute
{
    public class AminoSequence : ISequence
    {
        public const char StopTranslation = '*';
        public const char SkipNucleotides = '-';
        public string Sequence { get; private set; }
        public RnaSequence CodingRna { get; private set; }
        private Dictionary<char, long> _codeCounts;
        public IDictionary<char, long> CodeCounts { get { return _codeCounts; } }

        internal AminoSequence(RnaSequence codingRna, string normalizedAminos, Dictionary<char, long> acidCounts)
        {
            CodingRna = codingRna;
            Sequence = normalizedAminos;
            _codeCounts = acidCounts;
        }

        internal AminoSequence(RnaSequence rna)
        {
            CodingRna = rna;
            Sequence = ConvertRnaSequenceToAminoAcidSequence(rna.Sequence);
        }

        private string ConvertRnaSequenceToAminoAcidSequence(string nucleotides)
        {
            var list = new List<char>(nucleotides.Length / 2);
            _codeCounts = new Dictionary<char, long>(Maps.CodonToAmino.Count + 2);

            var index = 0;
            while (index < nucleotides.Length)
            {
                var nucleotide = nucleotides[index];
                switch (Char.ToUpperInvariant(nucleotide))
                {
                    case Maps.StopTranslation:
                        index++;
                        break;
                    case Maps.SkipNucleotides:
                        index++;
                        break;
                    default:
                        var maybeCodon = String.Format("{0}{1}{2}", nucleotide, nucleotides[++index], nucleotides[++index]).ToUpperInvariant();
                        if (!Maps.CodonToAmino.ContainsKey(maybeCodon))
                        {
                            throw new ArgumentException(String.Format("{0} is not a valid codon", maybeCodon));
                        }
                        list.AddRange(maybeCodon.ToCharArray());
                        break;
                }
                list.Add(nucleotide);
                _codeCounts[nucleotide]++;
            }
            return String.Join(String.Empty, list.ToArray());
        }

        public static AminoSequence GetAminoSequenceFromDna(DnaSequence dnaSequence)
        {
            var rna = DnaSequence.ConvertToRna(dnaSequence);
            return new AminoSequence(rna);
        }

        public static AminoSequence GetAminoSequenceFromRna(string rna)
        {
            var rnaSequence = new RnaSequence(rna);
            return new AminoSequence(rnaSequence);
        }

        private static ImmutableHashSet<char> _allowedSequenceCharacters;
        public static ImmutableHashSet<char> AllowedSequenceCharacters
        {
            get
            {
                if (_allowedSequenceCharacters == null || _allowedSequenceCharacters.Count == 0)
                {
                    var tempTable = new HashSet<char>(Maps.CodonToAmino.Values) { Maps.SkipNucleotides };
                    _allowedSequenceCharacters = tempTable.ToImmutableHashSet();
                }
                return _allowedSequenceCharacters;
            }
        }

        private static string ConvertNucleotidesToAminos(string nucleotides)
        {
            return String.Empty;
        }

        public ISet<char> AllowedCodes
        {
            get { throw new NotImplementedException(); }
        }
    }
}

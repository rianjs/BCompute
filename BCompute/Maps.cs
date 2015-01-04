using System.Collections.Generic;
using System.Collections.Immutable;

namespace BCompute
{
    internal sealed class Maps
    {
        public const char StopTranslation = '*';
        private static ImmutableDictionary<char, char> _dnaComplements;
        public static ImmutableDictionary<char, char> DnaComplements
        {
            get
            {
                if (_dnaComplements == null || _dnaComplements.Count == 0)
                {
                    var dnaComplements = new Dictionary<char, char>
                    {
                        {'A', 'T'},
                        {'G', 'C'},
                        {'T', 'A'},
                        {'C', 'G'}
                    };
                    _dnaComplements = dnaComplements.ToImmutableDictionary();
                }
                return _dnaComplements;
            }
        }

        private static ImmutableDictionary<char, char> _rnaComplements;
        public static ImmutableDictionary<char, char> RnaComplements
        {
            get
            {
                if (_rnaComplements == null || _rnaComplements.Count == 0)
                {
                    var rnaComplements = new Dictionary<char, char>
                    {
                        {'A', 'U'},
                        {'G', 'C'},
                        {'U', 'A'},
                        {'C', 'G'}
                    };
                    _rnaComplements = rnaComplements.ToImmutableDictionary();
                }
                return _rnaComplements;
            }
        }

        private static ImmutableDictionary<string, char> _codonToAminoAcid;

        public static ImmutableDictionary<string, char> CodonToAmino
        {
            get
            {
                if (_codonToAminoAcid == null || _codonToAminoAcid.Count == 0)
                {
                    var codons = new Dictionary<string, char>
                    {
                        {"UUU", 'F'},
                        {"CUU", 'L'},
                        {"AUU", 'I'},
                        {"GUU", 'V'},
                        {"UUC", 'F'},
                        {"CUC", 'L'},
                        {"AUC", 'I'},
                        {"GUC", 'V'},
                        {"UUA", 'L'},
                        {"CUA", 'L'},
                        {"AUA", 'I'},
                        {"GUA", 'V'},
                        {"UUG", 'L'},
                        {"CUG", 'L'},
                        {"AUG", 'M'},
                        {"GUG", 'V'},
                        {"UCU", 'S'},
                        {"CCU", 'P'},
                        {"ACU", 'T'},
                        {"GCU", 'A'},
                        {"UCC", 'S'},
                        {"CCC", 'P'},
                        {"ACC", 'T'},
                        {"GCC", 'A'},
                        {"UCA", 'S'},
                        {"CCA", 'P'},
                        {"ACA", 'T'},
                        {"GCA", 'A'},
                        {"UCG", 'S'},
                        {"CCG", 'P'},
                        {"ACG", 'T'},
                        {"GCG", 'A'},
                        {"UAU", 'Y'},
                        {"CAU", 'H'},
                        {"AAU", 'N'},
                        {"GAU", 'D'},
                        {"UAC", 'Y'},
                        {"CAC", 'H'},
                        {"AAC", 'N'},
                        {"GAC", 'D'},
                        {"UAA", StopTranslation},
                        {"CAA", 'Q'},
                        {"AAA", 'K'},
                        {"GAA", 'E'},
                        {"UAG", StopTranslation},
                        {"CAG", 'Q'},
                        {"AAG", 'K'},
                        {"GAG", 'E'},
                        {"UGU", 'C'},
                        {"CGU", 'R'},
                        {"AGU", 'S'},
                        {"GGU", 'G'},
                        {"UGC", 'C'},
                        {"CGC", 'R'},
                        {"AGC", 'S'},
                        {"GGC", 'G'},
                        {"UGA", StopTranslation},
                        {"CGA", 'R'},
                        {"AGA", 'R'},
                        {"GGA", 'G'},
                        {"UGG", 'W'},
                        {"CGG", 'R'},
                        {"AGG", 'R'},
                        {"GGG", 'G'},
                    };
                    _codonToAminoAcid = codons.ToImmutableDictionary();
                }
                return _codonToAminoAcid;
            }
        }
    }
}

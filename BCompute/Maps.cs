using System.Collections.Generic;
using System.Collections.Immutable;

namespace BCompute
{
    internal sealed class Maps
    {
        public const char StopTranslation = '*';
        public const char SkipNucleotides = '-';
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
                        {"UUU", (char)AminoAcid.Phenylalanine},
                        {"CUU", (char)AminoAcid.Leucine},
                        {"AUU", (char)AminoAcid.Isoleucine},
                        {"GUU", (char)AminoAcid.Valine},
                        {"UUC", (char)AminoAcid.Phenylalanine},
                        {"CUC", (char)AminoAcid.Leucine},
                        {"AUC", (char)AminoAcid.Isoleucine},
                        {"GUC", (char)AminoAcid.Valine},
                        {"UUA", (char)AminoAcid.Leucine},
                        {"CUA", (char)AminoAcid.Leucine},
                        {"AUA", (char)AminoAcid.Isoleucine},
                        {"GUA", (char)AminoAcid.Valine},
                        {"UUG", (char)AminoAcid.Leucine},
                        {"CUG", (char)AminoAcid.Leucine},
                        {"AUG", (char)AminoAcid.Methionine},
                        {"GUG", (char)AminoAcid.Valine},
                        {"UCU", (char)AminoAcid.Serine},
                        {"CCU", (char)AminoAcid.Proline},
                        {"ACU", (char)AminoAcid.Threonine},
                        {"GCU", (char)AminoAcid.Alanine},
                        {"UCC", (char)AminoAcid.Serine},
                        {"CCC", (char)AminoAcid.Proline},
                        {"ACC", (char)AminoAcid.Threonine},
                        {"GCC", (char)AminoAcid.Alanine},
                        {"UCA", (char)AminoAcid.Serine},
                        {"CCA", (char)AminoAcid.Proline},
                        {"ACA", (char)AminoAcid.Threonine},
                        {"GCA", (char)AminoAcid.Alanine},
                        {"UCG", (char)AminoAcid.Serine},
                        {"CCG", (char)AminoAcid.Proline},
                        {"ACG", (char)AminoAcid.Threonine},
                        {"GCG", (char)AminoAcid.Alanine},
                        {"UAU", (char)AminoAcid.Tyrosine},
                        {"CAU", (char)AminoAcid.Histidine},
                        {"AAU", (char)AminoAcid.Asparagine},
                        {"GAU", (char)AminoAcid.Aspartate},
                        {"UAC", (char)AminoAcid.Tyrosine},
                        {"CAC", (char)AminoAcid.Histidine},
                        {"AAC", (char)AminoAcid.Asparagine},
                        {"GAC", (char)AminoAcid.Aspartate},
                        {"UAA", StopTranslation},
                        {"CAA", (char)AminoAcid.Glutamine},
                        {"AAA", (char)AminoAcid.Lysine},
                        {"GAA", (char)AminoAcid.Glutamate},
                        {"UAG", StopTranslation},
                        {"CAG", (char)AminoAcid.Glutamine},
                        {"AAG", (char)AminoAcid.Lysine},
                        {"GAG", (char)AminoAcid.Glutamate},
                        {"UGU", (char)AminoAcid.Cysteine},
                        {"CGU", (char)AminoAcid.Arginine},
                        {"AGU", (char)AminoAcid.Serine},
                        {"GGU", (char)AminoAcid.Glycine},
                        {"UGC", (char)AminoAcid.Cysteine},
                        {"CGC", (char)AminoAcid.Arginine},
                        {"AGC", (char)AminoAcid.Serine},
                        {"GGC", (char)AminoAcid.Glycine},
                        {"UGA", StopTranslation},
                        {"CGA", (char)AminoAcid.Arginine},
                        {"AGA", (char)AminoAcid.Arginine},
                        {"GGA", (char)AminoAcid.Glycine},
                        {"UGG", (char)AminoAcid.Tryptophan},
                        {"CGG", (char)AminoAcid.Arginine},
                        {"AGG", (char)AminoAcid.Arginine},
                        {"GGG", (char)AminoAcid.Glycine},
                    };
                    _codonToAminoAcid = codons.ToImmutableDictionary();
                }
                return _codonToAminoAcid;
            }
        }
    }
}

using System.Collections.Generic;

namespace BCompute.Data.GeneticCodes
{
    public static class InvertebrateMitochondria
    {
        public const int NcbiTranslationTable = 5;
        private static Dictionary<string, AminoAcid> _transcriptionTable;
        public static IDictionary<string, AminoAcid> TranscriptionTable
        {
            get
            {
                if (_transcriptionTable == null)
                {
                    var codons = new Dictionary<string, AminoAcid>
                    {
                        {"UUU", AminoAcid.Phenylalanine},
                        {"CUU", AminoAcid.Leucine},
                        {"AUU", AminoAcid.Isoleucine},
                        {"GUU", AminoAcid.Valine},
                        {"UUC", AminoAcid.Phenylalanine},
                        {"CUC", AminoAcid.Leucine},
                        {"AUC", AminoAcid.Isoleucine},
                        {"GUC", AminoAcid.Valine},
                        {"UUA", AminoAcid.Leucine},
                        {"CUA", AminoAcid.Leucine},
                        {"AUA", AminoAcid.Methionine},
                        {"GUA", AminoAcid.Valine},
                        {"UUG", AminoAcid.Leucine},
                        {"CUG", AminoAcid.Leucine},
                        {"AUG", AminoAcid.Methionine},
                        {"GUG", AminoAcid.Valine},
                        {"UCU", AminoAcid.Serine},
                        {"CCU", AminoAcid.Proline},
                        {"ACU", AminoAcid.Threonine},
                        {"GCU", AminoAcid.Alanine},
                        {"UCC", AminoAcid.Serine},
                        {"CCC", AminoAcid.Proline},
                        {"ACC", AminoAcid.Threonine},
                        {"GCC", AminoAcid.Alanine},
                        {"UCA", AminoAcid.Serine},
                        {"CCA", AminoAcid.Proline},
                        {"ACA", AminoAcid.Threonine},
                        {"GCA", AminoAcid.Alanine},
                        {"UCG", AminoAcid.Serine},
                        {"CCG", AminoAcid.Proline},
                        {"ACG", AminoAcid.Threonine},
                        {"GCG", AminoAcid.Alanine},
                        {"UAU", AminoAcid.Tyrosine},
                        {"CAU", AminoAcid.Histidine},
                        {"AAU", AminoAcid.Asparagine},
                        {"GAU", AminoAcid.Aspartate},
                        {"UAC", AminoAcid.Tyrosine},
                        {"CAC", AminoAcid.Histidine},
                        {"AAC", AminoAcid.Asparagine},
                        {"GAC", AminoAcid.Aspartate},
                        {"UAA", AminoAcid.Stop},
                        {"CAA", AminoAcid.Glutamine},
                        {"AAA", AminoAcid.Lysine},
                        {"GAA", AminoAcid.Glutamate},
                        {"UAG", AminoAcid.Stop},
                        {"CAG", AminoAcid.Glutamine},
                        {"AAG", AminoAcid.Lysine},
                        {"GAG", AminoAcid.Glutamate},
                        {"UGU", AminoAcid.Cysteine},
                        {"CGU", AminoAcid.Arginine},
                        {"AGU", AminoAcid.Serine},
                        {"GGU", AminoAcid.Glycine},
                        {"UGC", AminoAcid.Cysteine},
                        {"CGC", AminoAcid.Arginine},
                        {"AGC", AminoAcid.Serine},
                        {"GGC", AminoAcid.Glycine},
                        {"UGA", AminoAcid.Tryptophan},
                        {"CGA", AminoAcid.Arginine},
                        {"AGA", AminoAcid.Serine},
                        {"GGA", AminoAcid.Glycine},
                        {"UGG", AminoAcid.Tryptophan},
                        {"CGG", AminoAcid.Arginine},
                        {"AGG", AminoAcid.Serine},
                        {"GGG", AminoAcid.Glycine},
                    };
                    _transcriptionTable = codons;
                }
                return _transcriptionTable;
            }
        }
    }
}

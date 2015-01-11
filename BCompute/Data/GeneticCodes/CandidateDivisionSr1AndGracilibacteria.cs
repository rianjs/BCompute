using System.Collections.Generic;
using BCompute.Data.Alphabets;

namespace BCompute.Data.GeneticCodes
{
    public static class CandidateDivisionSr1AndGracilibacteria
    {
        public const int NcbiTranslationTable = (int)GeneticCode.CandidateDivisionSr1AndGracilibacteria;

        private static Dictionary<string, ProteinType> _transcriptionTable;
        public static IDictionary<string, ProteinType> TranscriptionTable
        {
            get
            {
                if (_transcriptionTable == null)
                {
                    var codons = new Dictionary<string, ProteinType>
                    {
                        {"UUU", ProteinType.Phenylalanine},
                        {"CUU", ProteinType.Leucine},
                        {"AUU", ProteinType.Isoleucine},
                        {"GUU", ProteinType.Valine},
                        {"UUC", ProteinType.Phenylalanine},
                        {"CUC", ProteinType.Leucine},
                        {"AUC", ProteinType.Isoleucine},
                        {"GUC", ProteinType.Valine},
                        {"UUA", ProteinType.Leucine},
                        {"CUA", ProteinType.Leucine},
                        {"AUA", ProteinType.Isoleucine},
                        {"GUA", ProteinType.Valine},
                        {"UUG", ProteinType.Leucine},
                        {"CUG", ProteinType.Leucine},
                        {"AUG", ProteinType.Methionine},
                        {"GUG", ProteinType.Valine},
                        {"UCU", ProteinType.Serine},
                        {"CCU", ProteinType.Proline},
                        {"ACU", ProteinType.Threonine},
                        {"GCU", ProteinType.Alanine},
                        {"UCC", ProteinType.Serine},
                        {"CCC", ProteinType.Proline},
                        {"ACC", ProteinType.Threonine},
                        {"GCC", ProteinType.Alanine},
                        {"UCA", ProteinType.Serine},
                        {"CCA", ProteinType.Proline},
                        {"ACA", ProteinType.Threonine},
                        {"GCA", ProteinType.Alanine},
                        {"UCG", ProteinType.Serine},
                        {"CCG", ProteinType.Proline},
                        {"ACG", ProteinType.Threonine},
                        {"GCG", ProteinType.Alanine},
                        {"UAU", ProteinType.Tyrosine},
                        {"CAU", ProteinType.Histidine},
                        {"AAU", ProteinType.Asparagine},
                        {"GAU", ProteinType.Aspartate},
                        {"UAC", ProteinType.Tyrosine},
                        {"CAC", ProteinType.Histidine},
                        {"AAC", ProteinType.Asparagine},
                        {"GAC", ProteinType.Aspartate},
                        {"UAA", ProteinType.Stop},
                        {"CAA", ProteinType.Glutamine},
                        {"AAA", ProteinType.Lysine},
                        {"GAA", ProteinType.Glutamate},
                        {"UAG", ProteinType.Stop},
                        {"CAG", ProteinType.Glutamine},
                        {"AAG", ProteinType.Lysine},
                        {"GAG", ProteinType.Glutamate},
                        {"UGU", ProteinType.Cysteine},
                        {"CGU", ProteinType.Arginine},
                        {"AGU", ProteinType.Serine},
                        {"GGU", ProteinType.Glycine},
                        {"UGC", ProteinType.Cysteine},
                        {"CGC", ProteinType.Arginine},
                        {"AGC", ProteinType.Serine},
                        {"GGC", ProteinType.Glycine},
                        {"UGA", ProteinType.Glycine},
                        {"CGA", ProteinType.Arginine},
                        {"AGA", ProteinType.Arginine},
                        {"GGA", ProteinType.Glycine},
                        {"UGG", ProteinType.Tryptophan},
                        {"CGG", ProteinType.Arginine},
                        {"AGG", ProteinType.Arginine},
                        {"GGG", ProteinType.Glycine},
                    };
                    _transcriptionTable = codons;
                }
                return _transcriptionTable;
            }
        }

    }
}

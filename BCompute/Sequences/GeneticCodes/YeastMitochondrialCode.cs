using System.Collections.Generic;

namespace BCompute
{
    public static class YeastMitochondrialCode
    {
        //http://www.ncbi.nlm.nih.gov/Taxonomy/Utils/wprintgc.cgi

        public const int NcbiTranslationTable = (int)GeneticCode.YeastMitochondrial;
        public static IDictionary<string, AminoAcid> RnaTranslationTable
        {
            get
            {
                return new Dictionary<string, AminoAcid>
                {
                    {"UUU", AminoAcid.Phenylalanine},
                    {"CUU", AminoAcid.Threonine},
                    {"AUU", AminoAcid.Isoleucine},
                    {"GUU", AminoAcid.Valine},
                    {"UUC", AminoAcid.Phenylalanine},
                    {"CUC", AminoAcid.Threonine},
                    {"AUC", AminoAcid.Isoleucine},
                    {"GUC", AminoAcid.Valine},
                    {"UUA", AminoAcid.Leucine},
                    {"CUA", AminoAcid.Threonine},
                    {"AUA", AminoAcid.Methionine},
                    {"GUA", AminoAcid.Valine},
                    {"UUG", AminoAcid.Leucine},
                    {"CUG", AminoAcid.Threonine},
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
                    {"AGC", AminoAcid.Serine},
                    {"GGC", AminoAcid.Glycine},
                    {"UGA", AminoAcid.Tryptophan},
                    {"AGA", AminoAcid.Arginine},
                    {"GGA", AminoAcid.Glycine},
                    {"UGG", AminoAcid.Tryptophan},
                    {"CGG", AminoAcid.Arginine},
                    {"AGG", AminoAcid.Arginine},
                    {"GGG", AminoAcid.Glycine},
                };
            }
        }
    }
}

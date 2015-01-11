﻿using System.Collections.Generic;
using BCompute.Data.Alphabets;

namespace BCompute.Data.GeneticCodes
{
    public static class YeastMitochondrialCode
    {
        //http://www.ncbi.nlm.nih.gov/Taxonomy/Utils/wprintgc.cgi

        public const int NcbiTranslationTable = (int)GeneticCode.YeastMitochondrial;
        private static Dictionary<string, Protein> _transcriptionTable;
        public static IDictionary<string, Protein> TranscriptionTable
        {
            get
            {
                if (_transcriptionTable == null)
                {
                    var codons = new Dictionary<string, Protein>
                    {
                        {"UUU", Protein.Phenylalanine},
                        {"CUU", Protein.Threonine},
                        {"AUU", Protein.Isoleucine},
                        {"GUU", Protein.Valine},
                        {"UUC", Protein.Phenylalanine},
                        {"CUC", Protein.Threonine},
                        {"AUC", Protein.Isoleucine},
                        {"GUC", Protein.Valine},
                        {"UUA", Protein.Leucine},
                        {"CUA", Protein.Threonine},
                        {"AUA", Protein.Methionine},
                        {"GUA", Protein.Valine},
                        {"UUG", Protein.Leucine},
                        {"CUG", Protein.Threonine},
                        {"AUG", Protein.Methionine},
                        {"GUG", Protein.Valine},
                        {"UCU", Protein.Serine},
                        {"CCU", Protein.Proline},
                        {"ACU", Protein.Threonine},
                        {"GCU", Protein.Alanine},
                        {"UCC", Protein.Serine},
                        {"CCC", Protein.Proline},
                        {"ACC", Protein.Threonine},
                        {"GCC", Protein.Alanine},
                        {"UCA", Protein.Serine},
                        {"CCA", Protein.Proline},
                        {"ACA", Protein.Threonine},
                        {"GCA", Protein.Alanine},
                        {"UCG", Protein.Serine},
                        {"CCG", Protein.Proline},
                        {"ACG", Protein.Threonine},
                        {"GCG", Protein.Alanine},
                        {"UAU", Protein.Tyrosine},
                        {"CAU", Protein.Histidine},
                        {"AAU", Protein.Asparagine},
                        {"GAU", Protein.Aspartate},
                        {"UAC", Protein.Tyrosine},
                        {"CAC", Protein.Histidine},
                        {"AAC", Protein.Asparagine},
                        {"GAC", Protein.Aspartate},
                        {"UAA", Protein.Stop},
                        {"CAA", Protein.Glutamine},
                        {"AAA", Protein.Lysine},
                        {"GAA", Protein.Glutamate},
                        {"UAG", Protein.Stop},
                        {"CAG", Protein.Glutamine},
                        {"AAG", Protein.Lysine},
                        {"GAG", Protein.Glutamate},
                        {"UGU", Protein.Cysteine},
                        {"CGU", Protein.Arginine},
                        {"AGU", Protein.Serine},
                        {"GGU", Protein.Glycine},
                        {"UGC", Protein.Cysteine},
                        {"AGC", Protein.Serine},
                        {"GGC", Protein.Glycine},
                        {"UGA", Protein.Tryptophan},
                        {"AGA", Protein.Arginine},
                        {"GGA", Protein.Glycine},
                        {"UGG", Protein.Tryptophan},
                        {"CGG", Protein.Arginine},
                        {"AGG", Protein.Arginine},
                        {"GGG", Protein.Glycine},
                    };
                    _transcriptionTable = codons;
                }
                return _transcriptionTable;
            }
        }
    }
}

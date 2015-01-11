using System;
using System.Collections.Generic;
using BCompute.Interfaces;

namespace BCompute.Data.Alphabets
{
    internal class NucleotideAlphabet : INucleotideAlphabet
    {
        public GeneticCode.GeneticCode GeneticCode { get; private set; }
        public ISet<Nucleotide> AllowedSymbols { get; private set; }
        public IDictionary<Nucleotide, Nucleotide> ComplementTable { get; private set; }
        public IDictionary<Nucleotide, Nucleotide> TranscriptionTable { get; private set; }
        public IDictionary<string, AminoAcid> TranslationTable { get; private set; }
        public ISet<Nucleotide> GcContentSymbols { get; private set; }

        public NucleotideAlphabet(AlphabetType nucleotideAlphabet, GeneticCode.GeneticCode geneticCode)
        {
            if (nucleotideAlphabet == AlphabetType.ExtendedProtein || nucleotideAlphabet == AlphabetType.StandardProtein)
            {
                throw new ArgumentException(String.Format(NucleotideAlphabetDataProvider.InvalidNucleotideAlphabet, nucleotideAlphabet));
            }

            GeneticCode = geneticCode;
            AllowedSymbols = NucleotideAlphabetDataProvider.GetAllowedSymbols(nucleotideAlphabet);
            ComplementTable = NucleotideAlphabetDataProvider.GetComplementTable(nucleotideAlphabet);
            TranscriptionTable = NucleotideAlphabetDataProvider.GetTranscriptionTable(nucleotideAlphabet);
            TranslationTable = NucleotideAlphabetDataProvider.GetTranslationTable(geneticCode, nucleotideAlphabet);
            GcContentSymbols = NucleotideAlphabetDataProvider.GcContentSymbols(nucleotideAlphabet);
        }
    }
}

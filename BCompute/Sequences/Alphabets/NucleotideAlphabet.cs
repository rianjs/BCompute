using System;
using System.Collections.Generic;

namespace BCompute
{
    public class NucleotideAlphabet : INucleotideAlphabet
    {
        public GeneticCode GeneticCode { get; private set; }
        public ISet<Nucleotide> AllowedSymbols { get; private set; }
        public IDictionary<Nucleotide, Nucleotide> ComplementTable { get; private set; }
        public IDictionary<Nucleotide, Nucleotide> TranscriptionTable { get; private set; }
        public IDictionary<string, AminoAcid> TranslationTable { get; private set; }
        public ISet<Nucleotide> GcContentSymbols { get; private set; }

        public NucleotideAlphabet(AlphabetType nucleotideAlphabet, GeneticCode geneticCode)
        {
            if (nucleotideAlphabet == AlphabetType.ExtendedProtein || nucleotideAlphabet == AlphabetType.StandardProtein)
            {
                throw new ArgumentException(String.Format(AlphabetDataProvider.InvalidNucleotideAlphabet, nucleotideAlphabet));
            }

            GeneticCode = geneticCode;
            AllowedSymbols = AlphabetDataProvider.GetAllowedNucleotideSymbols(nucleotideAlphabet);
            ComplementTable = AlphabetDataProvider.GetComplementTable(nucleotideAlphabet);
            TranscriptionTable = AlphabetDataProvider.GetTranscriptionTable(nucleotideAlphabet);
            TranslationTable = AlphabetDataProvider.GetTranslationTable(geneticCode, nucleotideAlphabet);
            GcContentSymbols = AlphabetDataProvider.GcContentSymbols(nucleotideAlphabet);
        }
    }
}

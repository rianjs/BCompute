using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCompute.Data.GeneticCodes;
using BCompute.Interfaces;

namespace BCompute.Data.Alphabets
{
    internal class NucleotideAlphabet : INucleotideAlphabet
    {
        public GeneticCode GeneticCode { get; private set; }
        public ISet<Nucleotide> AllowedSymbols { get; private set; }
        public IDictionary<Nucleotide, Nucleotide> ComplementTable { get; private set; }
        public IDictionary<Nucleotide, Nucleotide> TranscriptionTable { get; private set; }
        public IDictionary<Nucleotide, Nucleotide> TranslationTable { get; private set; }
        public ISet<Nucleotide> GcContentSymbols { get; private set; }

        public NucleotideAlphabet(AlphabetType alphabet, GeneticCode geneticCode)
        {
            if (alphabet == AlphabetType.ExtendedProtein || alphabet == AlphabetType.StandardProtein)
            {
                throw new ArgumentException(String.Format("{0} is not a nucleotide alphabet", alphabet));
            }
            GeneticCode = geneticCode;
            AllowedSymbols = NucleotideAlphabetDataProvider.GetAllowedSymbols(alphabet);
        }
    }
}

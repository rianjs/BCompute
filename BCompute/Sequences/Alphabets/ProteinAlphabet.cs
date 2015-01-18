using System;
using System.Collections.Generic;

namespace BCompute
{
    public class ProteinAlphabet : IProteinAlphabet
    {
        public const string InvalidProteinAlphabet = "{0} is not a protein alphabet";
        private readonly AlphabetType _activeAlphabet;
        private readonly GeneticCode _geneticCode;

        public ProteinAlphabet(AlphabetType proteinAlphabet, GeneticCode geneticCode = GeneticCode.Standard)
        {
            switch (proteinAlphabet)
            {
                case AlphabetType.ExtendedProtein:
                    break;
                case AlphabetType.StandardProtein:
                    break;
                default:
                    throw new ArgumentException(String.Format(InvalidProteinAlphabet, proteinAlphabet));
            }
            _activeAlphabet = proteinAlphabet;
            _geneticCode = geneticCode;
        }

        public GeneticCode GeneticCode
        {
            get { return _geneticCode; }
        }

        private HashSet<AminoAcid> _allowedSymbols; 
        public ISet<AminoAcid> AllowedSymbols
        {
            get { return _allowedSymbols ?? (_allowedSymbols = new HashSet<AminoAcid>(AlphabetDataProvider.GetAllowedProteinSymbols(_activeAlphabet))); }
        }

        private Dictionary<string, AminoAcid> _translationTable;
        public IDictionary<string, AminoAcid> TranslationTable
        {
            get
            {
                return _translationTable ?? (_translationTable = new Dictionary<string, AminoAcid>(AlphabetDataProvider.GetTranslationTable(_geneticCode, _activeAlphabet)));
            }
        }
    }
}

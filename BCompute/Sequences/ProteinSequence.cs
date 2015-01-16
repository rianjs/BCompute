using System;
using System.Collections.Generic;

namespace BCompute
{
    public class ProteinSequence : IProteinSequence
    {
        public GeneticCode GeneticCode { get; private set; }
        public AlphabetType ActiveAlphabet { get; private set; }
        public ISet<AminoAcid> AllowedSymbols { get; private set; }
        public IDictionary<string, AminoAcid> TranslationTable { get; private set; }
        public string Sequence { get; private set; }
        private ProteinAlphabet _proteinAlphabet;

        public ProteinSequence(string rawSequence, AlphabetType proteinAlphabet, GeneticCode geneticCode = GeneticCode.Standard)
        {
            switch (proteinAlphabet)
            {
                case AlphabetType.ExtendedProtein:
                    break;
                case AlphabetType.StandardProtein:
                    break;
                default:
                    throw new ArgumentException(String.Format(ProteinAlphabet.InvalidProteinAlphabet, proteinAlphabet));
            }

            _proteinAlphabet = new ProteinAlphabet(proteinAlphabet, geneticCode);
            ActiveAlphabet = proteinAlphabet;

        }



        public long AminoAcidCount(AminoAcid aminoAcid)
        {
            throw new System.NotImplementedException();
        }

        public bool Equals(ProteinSequence aminoSequence, bool matchCase)
        {
            throw new System.NotImplementedException();
        }
    }
}

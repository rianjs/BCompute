using System;
using System.Collections.Generic;
using BCompute.Interfaces;
using GeneticCode = BCompute.Data.GeneticCodes.GeneticCode;

namespace BCompute.Data.Alphabets
{
    internal class ExtendedProtein : IProteinAlphabet
    {
        private HashSet<Protein> _allowedSymbols;
        public ISet<Protein> AllowedSymbols
        {
            get
            {
                var foo = (Protein[]) Enum.GetValues(typeof (Protein));
                return _allowedSymbols ?? (_allowedSymbols = new HashSet<Protein>(foo));
            }
        }

        public GeneticCode GeneticCode { get; private set; }

        public IDictionary<Protein, IEnumerable<string>> ReverseTranscriptionTable { get; private set; }
    }
}

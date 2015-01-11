using System.Collections.Generic;
using BCompute.Interfaces;
using GeneticCode = BCompute.Data.GeneticCodes.GeneticCode;

namespace BCompute.Data.Alphabets
{
    internal class StandardProtein : IProteinAlphabet
    {
        private HashSet<Protein> _allowedSymbols;
        public ISet<Protein> AllowedSymbols
        {
            get
            {
                return _allowedSymbols ??
                       (_allowedSymbols =
                           new HashSet<Protein>
                           {
                               Protein.Alanine,
                               Protein.Cysteine,
                               Protein.Aspartate,
                               Protein.Glutamate,
                               Protein.Phenylalanine,
                               Protein.Glycine,
                               Protein.Histidine,
                               Protein.Isoleucine,
                               Protein.Lysine,
                               Protein.Leucine,
                               Protein.Methionine,
                               Protein.Asparagine,
                               Protein.Proline,
                               Protein.Glutamine,
                               Protein.Arginine,
                               Protein.Serine,
                               Protein.Threonine,
                               Protein.Valine,
                               Protein.Tryptophan,
                               Protein.Tyrosine,
                               Protein.Stop,
                               Protein.Gap,
                           });
            }
        }

        public GeneticCode GeneticCode { get; private set; }

        public IDictionary<Protein, IEnumerable<string>> ReverseTranscriptionTable { get; private set; }
    }
}

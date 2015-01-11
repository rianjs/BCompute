using System.Collections.Generic;
using BCompute.Interfaces;
using GeneticCode = BCompute.Data.GeneticCodes.GeneticCode;

namespace BCompute.Data.Alphabets
{
    internal class StandardProtein : IProteinSequence
    {
        private HashSet<ProteinType> _allowedSymbols;
        public ISet<ProteinType> AllowedSymbols
        {
            get
            {
                return _allowedSymbols ??
                       (_allowedSymbols =
                           new HashSet<ProteinType>
                           {
                               ProteinType.Alanine,
                               ProteinType.Cysteine,
                               ProteinType.Aspartate,
                               ProteinType.Glutamate,
                               ProteinType.Phenylalanine,
                               ProteinType.Glycine,
                               ProteinType.Histidine,
                               ProteinType.Isoleucine,
                               ProteinType.Lysine,
                               ProteinType.Leucine,
                               ProteinType.Methionine,
                               ProteinType.Asparagine,
                               ProteinType.Proline,
                               ProteinType.Glutamine,
                               ProteinType.Arginine,
                               ProteinType.Serine,
                               ProteinType.Threonine,
                               ProteinType.Valine,
                               ProteinType.Tryptophan,
                               ProteinType.Tyrosine,
                               ProteinType.Stop,
                               ProteinType.Gap,
                           });
            }
        }

        public GeneticCode GeneticCode { get; private set; }

        public IDictionary<ProteinType, IEnumerable<string>> ReverseTranscriptionTable { get; private set; }
    }
}

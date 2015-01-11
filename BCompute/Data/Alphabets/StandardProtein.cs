using System.Collections.Generic;
using BCompute.Interfaces;
using GeneticCode = BCompute.Data.GeneticCode.GeneticCode;

namespace BCompute.Data.Alphabets
{
    internal class StandardProtein : IProteinSequence
    {
        private HashSet<AminoAcid> _allowedSymbols;
        public ISet<AminoAcid> AllowedSymbols
        {
            get
            {
                return _allowedSymbols ??
                       (_allowedSymbols =
                           new HashSet<AminoAcid>
                           {
                               AminoAcid.Alanine,
                               AminoAcid.Cysteine,
                               AminoAcid.Aspartate,
                               AminoAcid.Glutamate,
                               AminoAcid.Phenylalanine,
                               AminoAcid.Glycine,
                               AminoAcid.Histidine,
                               AminoAcid.Isoleucine,
                               AminoAcid.Lysine,
                               AminoAcid.Leucine,
                               AminoAcid.Methionine,
                               AminoAcid.Asparagine,
                               AminoAcid.Proline,
                               AminoAcid.Glutamine,
                               AminoAcid.Arginine,
                               AminoAcid.Serine,
                               AminoAcid.Threonine,
                               AminoAcid.Valine,
                               AminoAcid.Tryptophan,
                               AminoAcid.Tyrosine,
                               AminoAcid.Stop,
                               AminoAcid.Gap,
                           });
            }
        }

        public GeneticCode GeneticCode { get; private set; }

        public IDictionary<AminoAcid, IEnumerable<string>> ReverseTranscriptionTable { get; private set; }
    }
}

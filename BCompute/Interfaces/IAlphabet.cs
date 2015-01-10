using System.Collections.Generic;
using BCompute.Data.GeneticCodes;

namespace BCompute.Interfaces
{
    interface IAlphabet
    {
        /// <summary>
        /// Provides the set of allowed symbols
        /// </summary>
        ISet<Nucleotide> AllowedSymbols { get; }

        /// <summary>
        /// The NCBI genetic code triplet mappings, which governs which transcription table is used for transcribing and back transcribing RNA and DNA
        /// </summary>
        GeneticCode GeneticCode { get; }
    }
}

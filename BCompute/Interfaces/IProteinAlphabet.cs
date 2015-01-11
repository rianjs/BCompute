using System.Collections.Generic;
using BCompute.Data.Alphabets;
using BCompute.Data.GeneticCodes;

namespace BCompute.Interfaces
{
    internal interface IProteinAlphabet
    {
        /// <summary>
        /// Provides the set of allowed symbols
        /// </summary>
        ISet<Protein> AllowedSymbols { get; }

        /// <summary>
        /// The NCBI genetic code triplet mappings, which governs which transcription table is used for transcribing and back transcribing RNA and DNA
        /// </summary>
        GeneticCode GeneticCode { get; }

        /// <summary>
        /// Mapping of proteins to all possible triplets as specified by the GeneticCode
        /// </summary>
        IDictionary<Protein, IEnumerable<string>> ReverseTranscriptionTable { get; }
    }
}

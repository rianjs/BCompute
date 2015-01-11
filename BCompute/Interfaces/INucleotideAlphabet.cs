using System.Collections.Generic;

namespace BCompute.Interfaces
{
    interface INucleotideAlphabet
    {
        /// <summary>
        /// Mapping of codon triplets to proteins. (Works for DNA, too.)
        /// </summary>
        IDictionary<Nucleotide, Nucleotide> TranslationTable { get; }

        /// <summary>
        /// Mapping of nucleotide complements
        /// </summary>
        IDictionary<Nucleotide, Nucleotide> ComplementTable { get; }

        /// <summary>
        /// Mapping of DNA to RNA and RNA to DNA transciptions
        /// </summary>
        IDictionary<Nucleotide, Nucleotide> TranscriptionTable { get; }

        /// <summary>
        /// Contains the set of symbols used to calculate the GC content of a nucleotide sequence
        /// </summary>
        ISet<Nucleotide> GcContentSymbols { get; }
    }
}

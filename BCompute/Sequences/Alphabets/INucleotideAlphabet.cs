using System.Collections.Generic;

namespace BCompute
{
    interface INucleotideAlphabet
    {
        /// <summary>
        /// The NCBI genetic code triplet mappings, which governs which transcription table is used for transcribing and back transcribing RNA and DNA
        /// </summary>
        GeneticCode GeneticCode { get; }

        /// <summary>
        /// The set of symbols that are allowed in the sequence's alphabet
        /// </summary>
        ISet<Nucleotide> AllowedSymbols { get; }

        /// <summary>
        /// Mapping of nucleotide complements
        /// </summary>
        IDictionary<Nucleotide, Nucleotide> ComplementTable { get; }
        
        /// <summary>
        /// Mapping of DNA to RNA and RNA to DNA transciptions
        /// </summary>
        IDictionary<Nucleotide, Nucleotide> TranscriptionTable { get; }

        /// <summary>
        /// Mapping of codon triplets to proteins. (Works for DNA, too.)
        /// </summary>
        IDictionary<string, AminoAcid> TranslationTable { get; }

        /// <summary>
        /// Contains the set of symbols used to calculate the GC content of a nucleotide sequence
        /// </summary>
        ISet<Nucleotide> GcContentSymbols { get; }
    }
}

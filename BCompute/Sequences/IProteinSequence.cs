using System.Collections.Generic;

namespace BCompute
{
    public interface IProteinSequence
    {
        /// <summary>
        /// The NCBI genetic code triplet mappings, which governs which transcription table is used for transcribing and back transcribing RNA and DNA
        /// </summary>
        GeneticCode GeneticCode { get; }

        /// <summary>
        /// The alphabet type governing the sequence's behavior
        /// </summary>
        AlphabetType ActiveAlphabet { get; }

        /// <summary>
        /// The set of symbols that are allowed in the sequence's alphabet
        /// </summary>
        ISet<AminoAcid> AllowedSymbols { get; }

        /// <summary>
        /// Mapping of codon triplets to proteins. (Works for DNA, too.)
        /// </summary>
        IDictionary<string, AminoAcid> TranslationTable { get; }

        /// <summary>
        /// Returns the protein string
        /// </summary>
        string Sequence { get; }

        /// <summary>
        /// Returns the number of times the specified nucleotide appears in the sequence data
        /// </summary>
        /// <param name="aminoAcid"></param>
        /// <returns></returns>
        long AminoAcidCount(AminoAcid aminoAcid);

        /// <summary>
        /// Returns true if this contents of the nucleotide sequence is the same as a comparison nucleotide sequence value. Alphabets, and genetic codes are NOT ignored
        /// </summary>
        /// <param name="aminoSequence"></param>
        /// <returns></returns>
        bool Equals(ProteinSequence aminoSequence, bool matchCase);
    }
}

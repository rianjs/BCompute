using System.Collections.Generic;

namespace BCompute
{
    public interface IProteinSequence
    {
        /// <summary>
        /// The set of symbols that are allowed in the sequence's alphabet
        /// </summary>
        ISet<AminoAcid> AllowedSymbols { get; }

        /// <summary>
        /// Mapping of codon triplets to proteins. (Works for DNA, too.)
        /// </summary>
        IDictionary<string, AminoAcid> TranslationTable { get; }

        /// <summary>
        /// Returns the number of times the specified nucleotide appears in the sequence data
        /// </summary>
        /// <param name="aminoAcid"></param>
        /// <returns></returns>
        long AminoCount(AminoAcid aminoAcid);
    }
}

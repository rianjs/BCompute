using System.Collections.Generic;

namespace BCompute
{
    public interface ISequence
    {
        /// <summary>
        /// The NCBI genetic code triplet mappings, which governs which translation table is used for translation
        /// </summary>        
        GeneticCode GeneticCode { get; }

        /// <summary>
        /// The alphabet type governing the sequence's behavior
        /// </summary>
        AlphabetType ActiveAlphabet { get; }

        /// <summary>
        /// Returns the raw string sequence
        /// </summary>
        string Sequence { get; }

        /// <summary>
        /// Returns true if this contents of the nucleotide sequence is the same as a comparison nucleotide sequence value. Alphabets, and genetic codes are NOT ignored
        /// </summary>
        /// <param name="nucleotideSequence"></param>
        /// <returns></returns>
        bool Equals(ISequence nucleotideSequence, bool matchCase);

        /// <summary>
        /// Returns all of the indices of the sequence where the motif may be found
        /// </summary>
        /// <param name="motif"></param>
        /// <returns></returns>
        IEnumerable<int> FindMotif(string motif);

        /// <summary>
        /// The metadata tags associated with the sequence
        /// </summary>
        ISet<string> Tags { get; } 
    }
}

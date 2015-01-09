using System.Collections.Generic;

namespace BCompute
{
    public interface ISequence
    {
        /// <summary>
        /// Returns the sequence string
        /// </summary>
        string Sequence { get; }

        /// <summary>
        /// Returns the number of gaps that are present in the sequence
        /// </summary>
        int GapCount { get; }

        /// <summary>
        /// Returns the number of indeterminate sections of the sequence
        /// </summary>
        int AnyBaseCount { get; }

        /// <summary>
        /// Returns a set of the allowable characters in the sequence for the sequence type
        /// </summary>
        ISet<char> AllowedCodes { get; }

        /// <summary>
        /// Returns the metadata tags associated with the sequence
        /// </summary>
        ISet<string> Tags { get; }
        
        /// <summary>
        /// Returns a Dictionary of all sequence characters and their counts
        /// </summary>
        IDictionary<char, long> CodeCounts { get; }

        /// <summary>
        /// Equality semantics are VALUE-oriented, checking whether the types and nucleotide acid chains are the same.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Equals(object obj);
    }
}
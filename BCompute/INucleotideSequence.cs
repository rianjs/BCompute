using System.Collections.Generic;
using System.Collections.Immutable;

namespace BCompute
{
    public interface INucleotideSequence {
        string BasePairs { get; }
        ImmutableHashSet<char> AllowedNucleotides { get; }
        IDictionary<char, ulong> NucleotideCounts { get; }
        string Complement { get; }
        string ReverseComplement { get; }
        long CalculateHammingDistance(NucleotideSequence nucleotideSequence);

        /// <summary>
        /// Equality semantics are VALUE-oriented, checking whether the types and nucleotide acid chains are the same.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Equals(object obj);
    }
}
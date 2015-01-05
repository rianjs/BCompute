using System.Collections.Generic;

namespace BCompute
{
    public interface ISequence
    {
        string Sequence { get; }
        ISet<char> AllowedCodes { get; }
        IDictionary<char, long> CodeCounts { get; }

        /// <summary>
        /// Equality semantics are VALUE-oriented, checking whether the types and nucleotide acid chains are the same.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Equals(object obj);
    }
}
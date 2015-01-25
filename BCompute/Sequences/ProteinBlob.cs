using System.Collections.Generic;

namespace BCompute
{
    internal class ProteinBlob
    {
        public readonly string Sequence;
        public readonly Dictionary<AminoAcid, long> AminoCounts;

        /// <summary>
        /// Immutable class representing a protein sequence and its associated metadata
        /// </summary>
        /// <param name="sequenceData"></param>
        /// <param name="aminoCountData"></param>
        public ProteinBlob(string sequenceData, Dictionary<AminoAcid, long> aminoCountData)
        {
            Sequence = sequenceData;
            AminoCounts = aminoCountData;
        }
    }
}

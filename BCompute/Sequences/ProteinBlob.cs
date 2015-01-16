using System.Collections.Generic;

namespace BCompute
{
    internal class ProteinBlob
    {
        public readonly string Sequence;
        public readonly Dictionary<AminoAcid, long> AminoCounts;

        public ProteinBlob(string sequenceData, Dictionary<AminoAcid, long> aminoCountData)
        {
            Sequence = sequenceData;
            AminoCounts = aminoCountData;
        }
    }
}

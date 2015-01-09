using System.Collections.Generic;

namespace BCompute.Data.GeneticCodes
{
    public static class ThraustochytriumMitochondrial
    {
        public const int NcbiTranslationTable = 23;

        public static IDictionary<string, AminoAcid> TranscriptionTable
        {
            get { return StandardGeneticCode.TranscriptionTable; }
        }
    }
}

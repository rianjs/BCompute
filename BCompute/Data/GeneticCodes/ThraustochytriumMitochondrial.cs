using System.Collections.Generic;
using BCompute.Data.Alphabets;

namespace BCompute.Data.GeneticCodes
{
    public static class ThraustochytriumMitochondrial
    {
        public const int NcbiTranslationTable = (int)GeneticCode.ThraustochytriumMitochondrial;

        public static IDictionary<string, Protein> TranscriptionTable
        {
            get { return StandardGeneticCode.TranscriptionTable; }
        }
    }
}

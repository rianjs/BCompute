using System.Collections.Generic;
using BCompute.Data.Alphabets;

namespace BCompute.Data.GeneticCode
{
    public static class ThraustochytriumMitochondrial
    {
        public const int NcbiTranslationTable = (int)GeneticCode.ThraustochytriumMitochondrial;

        public static IDictionary<string, AminoAcid> RnaTranslationTable
        {
            get { return StandardGeneticCode.RnaTranslationTable; }
        }
    }
}

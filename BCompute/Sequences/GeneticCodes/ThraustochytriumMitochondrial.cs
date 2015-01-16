using System.Collections.Generic;

namespace BCompute
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

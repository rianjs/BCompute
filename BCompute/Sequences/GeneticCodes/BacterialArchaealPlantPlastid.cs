using System.Collections.Generic;

namespace BCompute
{
    public static class BacterialArchaealPlantPlastid
    {
        public const int NcbiTranslationTable = (int)GeneticCode.BacterialArchaealPlantPlastid;

        public static IDictionary<string, AminoAcid> RnaTranslationTable
        {
            get { return StandardGeneticCode.RnaTranslationTable; }
        } 
    }
}

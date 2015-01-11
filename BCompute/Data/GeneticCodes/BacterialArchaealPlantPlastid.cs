using System.Collections.Generic;
using BCompute.Data.Alphabets;

namespace BCompute.Data.GeneticCode
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

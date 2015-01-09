using System.Collections.Generic;

namespace BCompute.Data.GeneticCodes
{
    public static class BacterialArchaealPlantPlastid
    {
        public const int NcbiTranslationTable = 12;

        public static IDictionary<string, AminoAcid> TranscriptionTable
        {
            get { return StandardGeneticCode.TranscriptionTable; }
        } 
    }
}

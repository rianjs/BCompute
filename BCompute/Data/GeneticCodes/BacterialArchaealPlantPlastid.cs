using System.Collections.Generic;
using BCompute.Data.Alphabets;

namespace BCompute.Data.GeneticCodes
{
    public static class BacterialArchaealPlantPlastid
    {
        public const int NcbiTranslationTable = (int)GeneticCode.BacterialArchaealPlantPlastid;

        public static IDictionary<string, Protein> TranscriptionTable
        {
            get { return StandardGeneticCode.TranscriptionTable; }
        } 
    }
}

using System.Collections.Generic;
using BCompute.Data.GeneticCodes;

namespace BCompute.Interfaces
{
    interface IAlphabet
    {
        ISet<Nucleotide> AllowedSymbols { get; }
        GeneticCode GeneticCode { get; }
    }
}

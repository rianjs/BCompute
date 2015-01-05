using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCompute
{
    interface INucleotideSequence : ISequence
    {
        string Complement { get; }
        string ReverseComplement { get; }
        long CalculateHammingDistance(NucleotideSequence nucleotideSequence);
    }
}

namespace BCompute
{
    interface INucleotideSequence : ISequence
    {
        string Complement { get; }
        string ReverseComplement { get; }
        long CalculateHammingDistance(NucleotideSequence nucleotideSequence);
    }
}

namespace BCompute
{
    internal class PopulationState
    {
        public readonly long Mature;
        public readonly long Immature;
        public readonly long Died;
        public readonly long Total;

        public PopulationState(long mature, long immature, long died)
        {
            Mature = mature;
            Immature = immature;
            Died = died;
            Total = Mature + Immature;
        }
    }
}

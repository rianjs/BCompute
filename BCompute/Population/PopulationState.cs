namespace BCompute
{
    public class PopulationState
    {
        public readonly long Mature;
        public readonly long Immature;
        public readonly long Died;
        public readonly long Total;

        /// <summary>
        /// Immutable memoization object representing the snapshot of the population at a given point in time when modeling organisms that reproduce sexually.
        /// </summary>
        /// <param name="mature"></param>
        /// <param name="immature"></param>
        /// <param name="died"></param>
        public PopulationState(long mature, long immature, long died)
        {
            Mature = mature;
            Immature = immature;
            Died = died;
            Total = Mature + Immature;
        }
    }
}

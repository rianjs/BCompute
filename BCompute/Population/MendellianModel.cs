using System;
using System.Collections.Generic;

namespace BCompute
{
    public class MendellianModel
    {
        public int TotalPopulation { get; private set; }
        private readonly Dictionary<ParentalPair, double> _parents;
        private readonly Dictionary<Genotype, double> _childAlleleProbabilities;
        public MendellianModel(int dominantCount, int heterozygousCount, int recessiveCount)
        {
            if (dominantCount < 0 || heterozygousCount < 0 || recessiveCount < 0)
            {
                throw new ArgumentException("Population counts cannot be negative");
            }

            TotalPopulation = dominantCount + heterozygousCount + recessiveCount;

            if (TotalPopulation < 2)
            {
                throw new ArgumentException("Two or more parents are required for sexual reproduction");
            }

            var populationAfterRemoval = TotalPopulation - 1;
            _parents = new Dictionary<ParentalPair, double>
            {
                {ParentalPair.DominantDominant, ((double) dominantCount / TotalPopulation) * (dominantCount - 1) / populationAfterRemoval},
                {ParentalPair.DominantHetero, ((double) dominantCount / TotalPopulation) * ((double) heterozygousCount / populationAfterRemoval) * 2},
                {ParentalPair.DominantRecessive, ((double) dominantCount / TotalPopulation) * ((double) recessiveCount / populationAfterRemoval) * 2},
                {ParentalPair.HeteroHetero, ((double) heterozygousCount / TotalPopulation) * (heterozygousCount - 1) / populationAfterRemoval},
                {ParentalPair.HeteroRecessive, ((double) heterozygousCount / TotalPopulation) * recessiveCount / populationAfterRemoval * 2},
                {ParentalPair.RecessiveRecessive, ((double) recessiveCount / TotalPopulation) * (recessiveCount - 1) / populationAfterRemoval}
            };

            _childAlleleProbabilities = new Dictionary<Genotype, double>
            {
                {Genotype.Dominant,       (_parents[ParentalPair.DominantDominant])
                                                  + (_parents[ParentalPair.HeteroHetero] * 0.25d)
                                                  + (_parents[ParentalPair.DominantHetero] * 0.5d)},

                {Genotype.Heterozygous,             (_parents[ParentalPair.DominantRecessive])
                                                  + (_parents[ParentalPair.DominantHetero] * 0.5d)
                                                  + (_parents[ParentalPair.HeteroHetero] * 0.5d)
                                                  + (_parents[ParentalPair.HeteroRecessive] * 0.5d)},

                {Genotype.Recessive,      (_parents[ParentalPair.RecessiveRecessive])
                                                  + (_parents[ParentalPair.HeteroHetero] * 0.25d)
                                                  + (_parents[ParentalPair.HeteroRecessive] * 0.5d)}
            };
        }

        public double AlleleProbability(Genotype genotype)
        {
            return Math.Round(_childAlleleProbabilities[genotype], Constants.RoundingPrecision);
        }

        public IDictionary<ParentalPair, double> Parents { get { return _parents; } }
    }
}

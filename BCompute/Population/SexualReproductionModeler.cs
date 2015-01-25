using System;
using System.Collections.Generic;
using System.Linq;

namespace BCompute
{
    /// <summary>
    /// Models the population growth or decline over a specified number of generations
    /// </summary>
    public class SexualReproductionModeler
    {
        public int Lifespan { get; private set; }
        public int OffspringPairsPerGeneration { get; private set; }
        public int MaxReproductiveAge { get; private set; }
        public int InitialPopulation { get; private set; }

        public SexualReproductionModeler(int initialPopulation, int offspringPairsPerGeneration, int lifespan, int maxReproductiveAge)
        {
            #region Guard clauses
            if (initialPopulation < 1)
            {
                throw new ArgumentException("Initial population cannot be less than 1");
            }

            if (maxReproductiveAge <= 1)
            {
                throw new ArgumentException("Maximum reproductive age cannot occur before maturity");
            }

            if (offspringPairsPerGeneration < 1)
            {
                throw new ArgumentException("Offspring pairs per generation cannot be less than 1");
            }
            #endregion

            InitialPopulation = initialPopulation;
            OffspringPairsPerGeneration = offspringPairsPerGeneration;
            Lifespan = lifespan;
            MaxReproductiveAge = maxReproductiveAge;
        }

        public SexualReproductionModeler(int initialPopulation, int offspringPairsPerGeneration, int lifespan)
            : this(initialPopulation, offspringPairsPerGeneration, lifespan, lifespan) { }

        private List<PopulationState> _populationPerGeneration;
        public long GetPopulationCount(int generations)
        {
            if (generations < 1)
            {
                throw new ArgumentException("Cannot get the population count of a negative number of generations");
            }

            _populationPerGeneration = new List<PopulationState>(generations) {new PopulationState(0, InitialPopulation, 0)};
            var zeroBasedLifespan = Lifespan - 1;
            var cycleCount = 1;
            while (cycleCount < generations)
            {
                var died = (cycleCount < zeroBasedLifespan) ? 0 : _populationPerGeneration[cycleCount - zeroBasedLifespan].Immature;
                var previousElement = _populationPerGeneration[cycleCount - 1];
                var mature = previousElement.Mature + previousElement.Immature - previousElement.Died;
                var immature = previousElement.Mature * OffspringPairsPerGeneration;
                var nextState = new PopulationState(mature, immature, died);
                _populationPerGeneration.Add(nextState);
                cycleCount++;
            }

            return _populationPerGeneration.Last().Total;
        }
    }
}

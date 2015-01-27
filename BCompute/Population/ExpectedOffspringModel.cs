using System;
using System.Collections.Generic;

namespace BCompute
{
    public class ExpectedOffspringModel
    {
        public int TotalPopulation { get; private set; }
        private readonly Dictionary<ParentalPair, double> _parents;
        private readonly Dictionary<Genotype, double> _childAlleleProbabilities;

        /// <summary>
        /// Instantiate an allele model by specifying parental pair explicitly
        /// </summary>
        /// <param name="doubleDominant">Number of dual dominant parental parental pairs</param>
        /// <param name="dominantHetero">Number of dominant + heterozygous parental pairs</param>
        /// <param name="dominantRecessive">Number of dominant + recessive parental paire</param>
        /// <param name="doubleHetero">Number of dual heterozygous parental pairs</param>
        /// <param name="heteroRecessive">Number of heterozygous + recessive parental pairs</param>
        /// <param name="doubleRecessive">Number of dual recessive parental pairs</param>
        public ExpectedOffspringModel(int doubleDominant, int dominantHetero, int dominantRecessive, int doubleHetero, int heteroRecessive, int doubleRecessive)
        {
            if (doubleDominant < 0 || dominantHetero < 0 || dominantRecessive < 0 || doubleHetero < 0 || heteroRecessive < 0 || doubleRecessive < 0)
            {
                throw new ArgumentException("Parental pair counts cannot be less than 0");
            }

            var totalPairs = doubleDominant + dominantHetero + dominantRecessive + doubleHetero + heteroRecessive + doubleRecessive;

            if (totalPairs < 1)
            {
                throw new ArgumentException("Must have at least one parental pair to reproduce");
            }

            TotalPopulation = totalPairs * 2;
            _parents = new Dictionary<ParentalPair, double>
            {
                {ParentalPair.DominantDominant, (doubleDominant == 0) ? 0 : (double) doubleDominant / totalPairs},
                {ParentalPair.DominantHetero, (dominantHetero == 0) ? 0 : (double) dominantHetero / totalPairs},
                {ParentalPair.DominantRecessive, (dominantRecessive == 0) ? 0 : (double) dominantRecessive / totalPairs},
                {ParentalPair.HeteroHetero, (doubleHetero == 0) ? 0 : (double) doubleHetero / totalPairs},
                {ParentalPair.HeteroRecessive, (heteroRecessive == 0) ? 0 : (double) heteroRecessive / totalPairs},
                {ParentalPair.RecessiveRecessive, (doubleRecessive == 0) ? 0 : (double) doubleRecessive / totalPairs}
            };

            _childAlleleProbabilities = new Dictionary<Genotype, double>
            {
                {Genotype.Dominant,       (_parents[ParentalPair.DominantDominant])
                                        + (_parents[ParentalPair.HeteroHetero] * 0.25d)
                                        + (_parents[ParentalPair.DominantHetero] * 0.5d)},

                {Genotype.Heterozygous,   (_parents[ParentalPair.DominantRecessive])
                                        + (_parents[ParentalPair.DominantHetero] * 0.5d)
                                        + (_parents[ParentalPair.HeteroHetero] * 0.5d)
                                        + (_parents[ParentalPair.HeteroRecessive] * 0.5d)},

                {Genotype.Recessive,      (_parents[ParentalPair.RecessiveRecessive])
                                        + (_parents[ParentalPair.HeteroHetero] * 0.25d)
                                        + (_parents[ParentalPair.HeteroRecessive] * 0.5d)}
            };
        }

        public IDictionary<ParentalPair, double> Parents { get { return _parents; } }

        /// <summary>
        /// Returns the number of offspring that will have the specified genotype to 6 decimal places
        /// </summary>
        /// <param name="genotype"></param>
        /// <param name="offspringPerCouple"></param>
        /// <returns></returns>
        public double ExpectedOffspring(Genotype genotype, float offspringPerCouple)
        {
            if (offspringPerCouple < 1)
            {
                throw new ArgumentException("Expected offspring per pair cannot be less than 1");
            }

            var reproductiveAdults = TotalPopulation / 2;
            var totalOffspring = offspringPerCouple * reproductiveAdults;
            var answer = _childAlleleProbabilities[genotype] * totalOffspring;
            return Math.Round(answer, Constants.RoundingPrecision);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace BCompute
{
    public class Population
    {
        public uint HomozygousCount { get; private set; }
        public uint HeterozygousCount { get; private set; }
        public uint RecessiveCount { get; private set; }
        public Population(uint homozygousCount, uint heterozygousCount, uint recessiveCount)
        {
            HomozygousCount = homozygousCount;
            HeterozygousCount = heterozygousCount;
            RecessiveCount = recessiveCount;
        }

        public uint TotalPopulation
        {
            get { return HomozygousCount + HeterozygousCount + RecessiveCount; }
        }

        public double GetChildAlleleProbability(Genotype genotype)
        {
            ComputeChildAlleleProbabilities();
            return _childAlleleProbabilities[genotype];
        }

        public ImmutableDictionary<string, double> ParentalProbabilities
        {
            //p(Parent A) = ratio of each Genotype relative to the population
            //p(Parent B) = ratio of each Genotype left in the population pool
            //  If Parent B is the same genotype as Parent A:
            //      (Parent A genotype count / TotalPopulation) * ((Parent A genotype count - 1) / (TotalPopulation - 1))
            //  If Parent B is of a different genotype:
            //      ((Parent A genotype count / TotalPopulation) * (Parent B genotype count) / (TotalPopulation - 1)) * 2
            //      We by 2 in the latter case, because order is significant
            get
            {
                var parentBPopulation = TotalPopulation - 1;
                return new Dictionary<string, double>
                {
                    {"DDDD", ((double) HomozygousCount / TotalPopulation) * (HomozygousCount - 1) / parentBPopulation},
                    {"DDDd", ((double) HomozygousCount / TotalPopulation) * ((double) HeterozygousCount / parentBPopulation) * 2},
                    {"DdDd", ((double) HeterozygousCount / TotalPopulation) * (HeterozygousCount - 1) / parentBPopulation},
                    {"Dddd", ((double) HeterozygousCount / TotalPopulation) * RecessiveCount / parentBPopulation * 2},
                    {"dddd", ((double) RecessiveCount / TotalPopulation) * (RecessiveCount - 1) / parentBPopulation},
                    {"DDdd", ((double) HomozygousCount / TotalPopulation) * ((double)RecessiveCount / parentBPopulation) * 2},
                }.ToImmutableDictionary();
            }
        }

        private Dictionary<Genotype, double> _childAlleleProbabilities;
        private void ComputeChildAlleleProbabilities()
        {
            if (_childAlleleProbabilities == null)
            {
                _childAlleleProbabilities = new Dictionary<Genotype, double>(Enum.GetNames(typeof(Genotype)).Length);

                /******************************************************************************
                 * Probability of child alleles, given allele configuration of parent A and B:
                 * 
                 *    Parent: A    B    p(Child allele configuration)
                 * Child(DD):
                 *          (DD + DD) = 100%
                 *          (Dd + Dd) = 25%
                 *          (DD + Dd) = 50%
                 * 
                 * Child(Dd):
                 *          (DD + dd) = 100%
                 *          (DD + Dd) = 50%
                 *          (Dd + Dd) = 50%
                 *          (Dd + dd) = 50%
                 *          
                 * Child(dd):
                 *          (dd + dd) = 100%
                 *          (Dd + Dd) = 25%
                 *          (Dd + dd) = 50%
                 ******************************************************************************/

                _childAlleleProbabilities[Genotype.HomozygousDominant] = (ParentalProbabilities["DDDD"] * 1.0d)
                                                                         + (ParentalProbabilities["DdDd"] * 0.25d)
                                                                         + (ParentalProbabilities["DDDd"] * 0.5d);

                _childAlleleProbabilities[Genotype.Heterozygous] = (ParentalProbabilities["DDdd"] * 1.0d)
                                                                   + (ParentalProbabilities["DDDd"] * 0.5d)
                                                                   + (ParentalProbabilities["DdDd"] * 0.5d)
                                                                   + (ParentalProbabilities["Dddd"] * 0.5d);

                _childAlleleProbabilities[Genotype.HomozygousRecessive] = (ParentalProbabilities["dddd"] * 1.0d)
                                                                          + (ParentalProbabilities["DdDd"] * 0.25d)
                                                                          + (ParentalProbabilities["Dddd"] * 0.5d);
            }
        }
    }
}

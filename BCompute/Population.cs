using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace BCompute
{
    public class Population
    {
        public uint HomoDominantCount { get; private set; }
        public uint HeteroCount { get; private set; }
        public uint HomoRecessiveCount { get; private set; }
        public Population(uint homoDominantCount, uint heteroCount, uint homoRecessiveCount)
        {
            HomoDominantCount = homoDominantCount;
            HeteroCount = heteroCount;
            HomoRecessiveCount = homoRecessiveCount;
        }

        public uint TotalPopulation
        {
            get { return HomoDominantCount + HeteroCount + HomoRecessiveCount; }
        }

        public double GetChildAlleleProbability(Genotype genotype)
        {
            ComputeChildAlleleProbabilities();
            return _childAlleleProbabilities[genotype];
        }

        public ImmutableDictionary<string, double> ParentalProbabilities
        {
            //Compute Parent A = ratio of each Genotype relative to the population

            //Parent B:
            //  CurrentPopulation = TotalPopulation - 1
            //  Parent B probability = count(that group - 1) / CurrentPopulation
            get
            {
                var parentBPopulation = TotalPopulation - 1;
                return new Dictionary<string, double>
                {
                    {"DDDD", ((double)HomoDominantCount / TotalPopulation) * (HomoDominantCount - 1) / parentBPopulation},
                    {"DDDd", ((double) HomoDominantCount / TotalPopulation) * HeteroCount / parentBPopulation},
                    {"DdDd", ((double) HeteroCount / TotalPopulation) * (HeteroCount - 1) / parentBPopulation},
                    {"Dddd", ((double) HeteroCount / TotalPopulation) * HomoRecessiveCount / parentBPopulation},
                    {"dddd", ((double) HomoRecessiveCount / TotalPopulation) * (HomoRecessiveCount - 1) / parentBPopulation},
                    {"DDdd", ((double) HomoDominantCount / TotalPopulation) * HeteroCount / parentBPopulation},
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

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace BCompute.UnitTests
{
    [TestFixture]
    public class PopulationUnitTests
    {
        [Test, TestCaseSource("ParentalSetProbability_TestCases")]
        public double ParentalSetProbability(int homoDominantPopulation, int heteroPopulation, int homoRecessivePopulation)
        {
            var pop = new Population(homoDominantPopulation, heteroPopulation, homoRecessivePopulation);
            var parentalProbabilitySum = pop.ParentalProbabilities.Values.Sum();
            return parentalProbabilitySum;
        }

        public IEnumerable<ITestCaseData> ParentalSetProbability_TestCases()
        {
            yield return new TestCaseData(2, 2, 2).Returns(1.0d).SetName("Equal numbers of each genotype returns 1.0");
            yield return new TestCaseData(5, 4, 3).Returns(1.0d).SetName("Unequal numbers of each genotype returns 1.0");
            yield return new TestCaseData(2, 3, 2).Returns(1.0d).SetName("Unequal numbers of each genotype returns 1.0");
            yield return new TestCaseData(0, 1, 2).Returns(1.0d).SetName("0 subtype returns 1.0");
            yield return new TestCaseData(0, 0, 0).Returns(Double.NaN).SetName("Divide by 0 returns NaN");
        }

        [Test, TestCaseSource("ChildAlleleProbability_TestCases")]
        public double ChildAlleleManifestationProbabilityTests(int homoDominantPopulation, int heteroPopulation, int homoRecessivePopulation, IEnumerable<Genotype> genotypes)
        {
            var sum = genotypes.Sum(genotype => new Population(homoDominantPopulation, heteroPopulation, homoRecessivePopulation).GetChildAlleleProbability(genotype));
            //Only for unit testing...
            return Math.Round(sum, 5);
        }

        public IEnumerable<ITestCaseData> ChildAlleleProbability_TestCases()
        {
            yield return new TestCaseData(2, 2, 2, new List<Genotype> { Genotype.HomozygousDominant, Genotype.Heterozygous })
                .Returns(0.78333).SetName("Small test case: percentage of offspring that will express a dominant allele (homo + hetero)");
            yield return new TestCaseData(27, 21, 30, new List<Genotype> { Genotype.HomozygousDominant, Genotype.Heterozygous })
                .Returns(0.73277).SetName("Large test case: percentage of offspring that will express a dominant allele (homo + hetero)");
            yield return new TestCaseData(16, 20, 23, new List<Genotype> { Genotype.HomozygousDominant, Genotype.Heterozygous })
                .Returns(0.68995).SetName("Large test case: percentage of offspring that will express a dominant allele (homo + hetero)");
        }
    }
}

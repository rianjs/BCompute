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
        public double ParentalSetProbability(uint homoDominantPopulation, uint heteroPopulation, uint homoRecessivePopulation)
        {
            var pop = new Population(homoDominantPopulation, heteroPopulation, homoRecessivePopulation);
            var parentalProbabilitySum = pop.ParentalProbabilities.Values.Sum();
            return parentalProbabilitySum;
        }

        public IEnumerable<ITestCaseData> ParentalSetProbability_TestCases()
        {
            const uint zero = 0;
            const uint one = 1;
            const uint two = 2;
            const uint three = 3;
            const uint four = 4;
            const uint five = 5;

            yield return new TestCaseData(two, two, two).Returns(1.0d).SetName("Equal numbers of each genotype returns 1.0");
            yield return new TestCaseData(five, four, three).Returns(1.0d).SetName("Unequal numbers of each genotype returns 1.0");
            yield return new TestCaseData(two, three, two).Returns(1.0d).SetName("Unequal numbers of each genotype returns 1.0");
            yield return new TestCaseData(zero, one, two).Returns(1.0d).SetName("Zero subtype returns 1.0");
            yield return new TestCaseData(zero, zero, zero).Returns(Double.NaN).SetName("Divide by zero returns NaN");
        }

        [Test, TestCaseSource("ChildAlleleProbability_TestCases")]
        public double ChildAllelManifestationProbabilityTests(uint homoDominantPopulation, uint heteroPopulation, uint homoRecessivePopulation, IEnumerable<Genotype> genotypes)
        {
            var sum = genotypes.Sum(genotype => new Population(homoDominantPopulation, heteroPopulation, homoRecessivePopulation).GetChildAlleleProbability(genotype));
            //Only for unit testing...
            return Math.Round(sum, 5);
        }

        public IEnumerable<ITestCaseData> ChildAlleleProbability_TestCases()
        {
            const uint two = 2;
            yield return new TestCaseData(two, two, two, new List<Genotype> { Genotype.HomozygousDominant, Genotype.Heterozygous })
                .Returns(0.78333).SetName("Small test case: percentage of offspring that will express a dominant allele (homo + hetero)");
            yield return new TestCaseData((uint)27, (uint)21, (uint)30, new List<Genotype> { Genotype.HomozygousDominant, Genotype.Heterozygous })
                .Returns(0.73277).SetName("Large test case: percentage of offspring that will express a dominant allele (homo + hetero)");
            yield return new TestCaseData((uint)16, (uint)20, (uint)23, new List<Genotype> { Genotype.HomozygousDominant, Genotype.Heterozygous })
                .Returns(0.68995).SetName("Large test case: percentage of offspring that will express a dominant allele (homo + hetero)");
        }
    }
}

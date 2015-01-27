using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace BCompute.UnitTests
{
    [TestFixture]
    public class MendellianModelTests
    {
        [Test, TestCaseSource("ParentalSetProbability_TestCases")]
        public double ParentalSetProbability(int homoDominantPopulation, int heteroPopulation, int homoRecessivePopulation)
        {
            var pop = new MendellianModel(homoDominantPopulation, heteroPopulation, homoRecessivePopulation);
            var parentalProbabilitySum = pop.Parents.Values.Sum();
            return Math.Round(parentalProbabilitySum, Constants.RoundingPrecision);
        }

        public IEnumerable<ITestCaseData> ParentalSetProbability_TestCases()
        {
            yield return new TestCaseData(2, 2, 2).Returns(1.0d).SetName("Equal numbers of each genotype returns 1.0");
            yield return new TestCaseData(5, 4, 3).Returns(1.0d).SetName("Unequal numbers of each genotype returns 1.0");
            yield return new TestCaseData(2, 3, 2).Returns(1.0d).SetName("Unequal numbers of each genotype returns 1.0");
            yield return new TestCaseData(0, 1, 2).Returns(1.0d).SetName("0 subtype returns 1.0");
            yield return new TestCaseData(0, 0, 0).Throws(typeof(ArgumentException)).SetName("Zero population throws exception");
        }

        [Test, TestCaseSource("ChildAlleleProbability_TestCases")]
        public double ChildAlleleManifestationProbabilityTests(int homoDominantPopulation, int heteroPopulation, int homoRecessivePopulation, IEnumerable<Genotype> genotypes)
        {
            var sum = genotypes.Sum(genotype => new MendellianModel(homoDominantPopulation, heteroPopulation, homoRecessivePopulation).AlleleProbability(genotype));
            return Math.Round(sum, Constants.RoundingPrecision);
        }

        public IEnumerable<ITestCaseData> ChildAlleleProbability_TestCases()
        {
            yield return new TestCaseData(2, 2, 2, new List<Genotype> { Genotype.HomozygousDominant, Genotype.Heterozygous })
                .Returns(0.783334).SetName("Small test case: percentage of offspring that will express a dominant allele (homo + hetero)");
            yield return new TestCaseData(27, 21, 30, new List<Genotype> { Genotype.HomozygousDominant, Genotype.Heterozygous })
                .Returns(0.732767).SetName("Large test case: percentage of offspring that will express a dominant allele (homo + hetero)");
            yield return new TestCaseData(16, 20, 23, new List<Genotype> { Genotype.HomozygousDominant, Genotype.Heterozygous })
                .Returns(0.689948).SetName("Large test case: percentage of offspring that will express a dominant allele (homo + hetero)");
        }
    }
}

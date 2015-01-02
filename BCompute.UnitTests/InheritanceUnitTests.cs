using System.Collections.Generic;
using NUnit.Framework;

namespace BCompute.UnitTests
{
    [TestFixture]
    public class InheritanceUnitTests
    {
        [Test, TestCaseSource("ParentalSetProbability_TestCases")]
        public double ParentalSetProbability(uint homoDominantPopulation, uint heteroPopulation, uint homoRecessivePopulation, Genotype genotype)
        {
            return new PhenotypePopulation(homoDominantPopulation, heteroPopulation, homoRecessivePopulation).GetParentalSetProbability(genotype);
        }

        public IEnumerable<ITestCaseData> ParentalSetProbability_TestCases()
        {
            const double smallUniformPopulationParentalProbability = (2.0d / 6.0d) * (1.0d / 5.0d);
            const uint small = 2;

            yield return new TestCaseData(small, small, small, Genotype.HomozygousDominant).Returns(smallUniformPopulationParentalProbability)
                .SetName("HomoDominant: 2x2x2 test cases all return the same number");
            yield return new TestCaseData(small, small, small, Genotype.Heterozygous).Returns(smallUniformPopulationParentalProbability)
                .SetName("Hetero: 2x2x2 test cases all return the same number");
            yield return new TestCaseData(small, small, small, Genotype.HomozygousRecessive).Returns(smallUniformPopulationParentalProbability)
                .SetName("HomoRecessive: 2x2x2 test cases all return the same number");
        }

        [Test, TestCaseSource("ChildAlleleManifestationProbability_TestCases")]
        public double ChildAllelManifestationProbabilityTests(uint homoDominantPopulation, uint heteroPopulation, uint homoRecessivePopulation, Genotype genotype)
        {
            return new PhenotypePopulation(homoDominantPopulation, heteroPopulation, homoRecessivePopulation).GetChildAlleleProbability(genotype);
        }

        public IEnumerable<ITestCaseData> ChildAlleleManifestationProbability_TestCases()
        {
            const uint small = 2;
            yield return new TestCaseData(small, small, small).Returns(0.78333);
        }
    }
}

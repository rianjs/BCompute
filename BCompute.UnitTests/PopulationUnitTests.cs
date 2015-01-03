using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace BCompute.UnitTests
{
    [TestFixture]
    public class PopulationUnitTests
    {
        [Test, TestCaseSource("ParentalSetProbability_TestCases")]
        public void ParentalSetProbability(uint homoDominantPopulation, uint heteroPopulation, uint homoRecessivePopulation)
        {
            var pop = new Population(homoDominantPopulation, heteroPopulation, homoRecessivePopulation);
            var parentalProbabilitySum = pop.ParentalProbabilities.Values.Sum();
            Assert.AreEqual(1.00, parentalProbabilitySum);
        }

        public IEnumerable<ITestCaseData> ParentalSetProbability_TestCases()
        {
            //const double smallUniformPopulationParentalProbability = (2.0d / 6.0d) * (1.0d / 5.0d);
            const uint small = 2;
            const uint lessSmall = 3;

            yield return new TestCaseData(small, small, small).SetName("Equal numbers of each genotype returns 1.00");
            yield return new TestCaseData(small, lessSmall, small).SetName("Unequal numbers of each genotype returns 1.00");
        }

        [Test, TestCaseSource("ChildAlleleProbability_TestCases")]
        public double ChildAllelManifestationProbabilityTests(uint homoDominantPopulation, uint heteroPopulation, uint homoRecessivePopulation, Genotype genotype)
        {
            return new Population(homoDominantPopulation, heteroPopulation, homoRecessivePopulation).GetChildAlleleProbability(genotype);
        }

        public IEnumerable<ITestCaseData> ChildAlleleProbability_TestCases()
        {
            const uint small = 2;
            yield return new TestCaseData(small, small, small).Returns(0.78333);
        }
    }
}

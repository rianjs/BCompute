using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace BCompute.UnitTests
{
    public class ExpectedOffspringTests
    {
        [Test, TestCaseSource("ParentalProbability_TestCases")]
        public double ParentalSetProbability(int doubleDominant, int dominantHetero, int dominantRecessive, int doubleHetero, int heteroRecessive, int doubleRecessive)
        {
            var model = new ExpectedOffspringModel(doubleDominant, doubleHetero, dominantRecessive, doubleHetero, heteroRecessive, doubleRecessive);
            var parentalProbabilitySum = model.Parents.Values.Sum();
            return Math.Round(parentalProbabilitySum, Constants.RoundingPrecision);
        }

        public IEnumerable<ITestCaseData> ParentalProbability_TestCases()
        {
            yield return new TestCaseData(1, 0, 0, 1, 0, 1).Returns(1.0d).SetName("Small sampling of parental pairs returns 1.0");
            yield return new TestCaseData(19843, 16233, 18989, 19312, 16213, 17310).Returns(1.0d).SetName("Large sampling of parental pairs returns 1.0");
            yield return new TestCaseData(-1, 0, 1, 2, 3, 4).Throws(typeof(ArgumentException)).SetName("Negative population number throws exception");
            yield return new TestCaseData(0, 0, 0, 0, 0, 0).Throws(typeof(ArgumentException)).SetName("Zero parental pairs across the board throws exception");
        }

        [Test, TestCaseSource("ExpectedOffspring_TestCases")]
        public double ExpectedOffspring_Tests(int doubleDominant, int dominantHetero, int dominantRecessive, int doubleHetero, int heteroRecessive,
            int doubleRecessive, Genotype genotype, float offspringPerPair)
        {
            var model = new ExpectedOffspringModel( doubleDominant,  dominantHetero,  dominantRecessive,  doubleHetero,  heteroRecessive, doubleRecessive);
            return model.ExpectedOffspring(genotype, offspringPerPair);
        }

        public IEnumerable<ITestCaseData> ExpectedOffspring_TestCases()
        {
            yield return new TestCaseData(1, 0, 0, 1, 0, 1, Genotype.Heterozygous, 2).Returns(1.0).SetName("Heterozygous portion of Rosalind sample ");
            yield return new TestCaseData(1, 0, 0, 1, 0, 1, Genotype.HomozygousDominant, 2).Returns(2.5).SetName("Dominant portion of Rosalind sample ");
            yield return new TestCaseData(1, 0, 0, 1, 0, 1, Genotype.HomozygousRecessive, 2).Returns(2.5).SetName("Recessive portion of Rosalind sample ");
            yield return new TestCaseData(19843, 16233, 18989, 19312, 16213, 17310, Genotype.Heterozygous, 2).Returns(89736.0).SetName("Heterozygous portion of Rosalind test ");
            yield return new TestCaseData(19843, 16233, 18989, 19312, 16213, 17310, Genotype.HomozygousDominant, 2).Returns(65575.0).SetName("Dominant portion of Rosalind test ");
            yield return new TestCaseData(19843, 16233, 18989, 19312, 16213, 17310, Genotype.HomozygousRecessive, 2).Returns(60489.0).SetName("Recessive portion of Rosalind test ");
        }
    }
}

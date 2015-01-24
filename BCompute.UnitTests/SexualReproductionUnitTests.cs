using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BCompute.UnitTests
{
    public class SexualReproductionUnitTests
    {
        [Test, TestCaseSource("GetPopulationCount_TestCases")]
        public long GetPopulationCount_Tests(int initialPopulation, int offspring, int lifespan, int generations)
        {
            var model = new SexualReproductionModeler(initialPopulation, offspring, lifespan);
            return model.GetPopulationCount(generations);
        }

        public IEnumerable<ITestCaseData> GetPopulationCount_TestCases()
        {
            yield return new TestCaseData(1, 3, int.MaxValue, 5).Returns(19).SetName("Immortal rabbits, small number of generations");
            yield return new TestCaseData(1, 1, 3, 6).Returns(4).SetName("Mortal rabbits, small number of generations");
            yield return new TestCaseData(1, 3, int.MaxValue, 35).Returns(1323839213083).SetName("Immortal rabbits, large number of generations");
            yield return new TestCaseData(1, 1, 18, 87).Returns(676172117379838466).SetName("Mortal rabbits, large number of generations");

            yield return new TestCaseData(-1, 1, 18, 87).Throws(typeof(ArgumentException)).SetName("Negative initial population throws exception");
            yield return new TestCaseData(1, -1, 18, 87).Throws(typeof(ArgumentException)).SetName("Negative offspring per generation throws exception");
            yield return new TestCaseData(1, 1, -18, 87).Throws(typeof(ArgumentException)).SetName("Negative lifespan throws exception");
            yield return new TestCaseData(100, 1, 18, 1).Returns(100).SetName("Population count of first generation is the initial population");
        }
    }
}

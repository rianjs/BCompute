using System.Collections.Generic;
using NUnit.Framework;

namespace BCompute.UnitTests
{
    [TestFixture]
    public class RecurrenceUnitTests
    {
        [Test, TestCaseSource("RecurrenceGrowth_TestCases")]
        public static long Growth_Tests(int generations, int growthPerGeneration, int survivalGenerations)
        {
            var answer = ReprodutiveModeling.Growth(generations, growthPerGeneration, survivalGenerations);
            return answer;
        }

        public static IEnumerable<ITestCaseData> RecurrenceGrowth_TestCases()
        {
            yield return new TestCaseData(35, 3, int.MaxValue).Returns(1323839213083).SetName("Fibonacci Rosalind problem solution");
            yield return new TestCaseData(5, 3, int.MaxValue).Returns(19).SetName("Fibonacci Rosalind sample data");
        }
    }
}

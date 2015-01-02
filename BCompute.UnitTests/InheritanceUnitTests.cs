using System.Collections.Generic;
using NUnit.Framework;

namespace BCompute.UnitTests
{
    [TestFixture]
    public class InheritanceUnitTests
    {
        public double MendellianInheritanceTests(int homozygousDominant, int heterozygousDominant, int homozygousRecessive)
        {
            
        }

        public IEnumerable<ITestCaseData> MendellianInheritance_TestCases()
        {
            yield return new TestCaseData(2, 2, 2).Returns(0.78333);
        }
    }
}

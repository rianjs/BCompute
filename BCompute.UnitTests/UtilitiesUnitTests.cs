using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace BCompute.UnitTests
{
    [TestFixture]
    public class UtilitiesUnitTests
    {
        [Test, TestCaseSource("FindMotif_TestCases")]
        public void FindMotif_Tests(string needle, string haystack, IEnumerable<int> expected)
        {
            var actual = Utilities.FindMotif(needle, haystack).ToList();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        public IEnumerable<ITestCaseData> FindMotif_TestCases()
        {
            yield return new TestCaseData("ATAT", "GATATATGCATATACTT", new List<int>{1, 3, 9});
        } 
    }
}

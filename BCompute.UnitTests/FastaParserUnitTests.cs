using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace BCompute.UnitTests
{
    [TestFixture]
    public class FastaParserUnitTests
    {
        private const string _label = "Rosalind_4172";
        private const string _normalizedNucleotides =
            "GAAAGCTGGCGGACCATCGGAATATCAAAAATCAATGTGCTCGCGGTGGAGAATTGCGATGGCAGTAGTTAAGCTGATCCATACTGAACATGCACGGGCATAGCAAAATTCCAGAAGCCT";

        private const string _singleLineFasta = @">Rosalind_4172
GAAAGCTGGCGGACCATCGGAATATCAAAAATCAATGTGCTCGCGGTGGAGAATTGCGATGGCAGTAGTTAAGCTGATCCATACTGAACATGCACGGGCATAGCAAAATTCCAGAAGCCT";

        private const string _multilineFastaWithLabel = @">Rosalind_4172
GAAAGCTGGCGGACCATCGGAATATCAAAAATCAATGTGCTCGCGGTGGAGAATTGCGAT
GGCAGTAGTTAAGCTGATCCATACTGAACATGCACGGGCATAGCAAAATTCCAGAAGCCT";

        private const string _multilineFastaWithMetadataAndTrailingPipes = @">Rosalind_4172|Hello world|HODOR|@MetadataFTW!||Derp |No
GAAAGCTGGCGGACCATCGGAATATCAAAAATCAATGTGCTCGCGGTGGAGAATTGCGAT
GGCAGTAGTTAAGCTGATCCATACTGAACATGCACGGGCATAGCAAAATTCCAGAAGCCT";

        private const string _crossPlatformMultiLineN = ">Rosalind_4172|Hello world|HODOR|@MetadataFTW!||Derp |No\nGAAAGCTGGCGGACCATCGGAATATCAAAAATCAATGTGCTCGCGGTGGAGAATTGCGAT\nGGCAGTAGTTAAGCTGATCCATACTGAACATGCACGGGCATAGCAAAATTCCAGAAGCCT";
        private const string _crossPlatformMultiLineR = ">Rosalind_4172|Hello world|HODOR|@MetadataFTW!||Derp |No\rGAAAGCTGGCGGACCATCGGAATATCAAAAATCAATGTGCTCGCGGTGGAGAATTGCGAT\rGGCAGTAGTTAAGCTGATCCATACTGAACATGCACGGGCATAGCAAAATTCCAGAAGCCT";

        [Test, TestCaseSource("GenerateFastaSequence_TestCases")]
        public void GenerateFastaSequenceTests(string rawSequence, AlphabetType sequenceAlphabet, ISequence expected)
        {
            var parser = new FastaParser(rawSequence, sequenceAlphabet);
            var actual = parser.Parse().First();

            Assert.AreEqual(expected.GetType(), actual.GetType());
            CollectionAssert.AreEquivalent(expected.Tags, actual.Tags);
            Assert.AreEqual(expected.Sequence, actual.Sequence);
            Assert.AreEqual(expected.ActiveAlphabet, actual.ActiveAlphabet);
            
        }

        public IEnumerable<ITestCaseData> GenerateFastaSequence_TestCases()
        {
            var singleLineNoTags = new DnaSequence(_normalizedNucleotides, AlphabetType.StrictDna, GeneticCode.Standard, new List<string> { _label });
            var multilineWithLabel = new DnaSequence(_normalizedNucleotides, AlphabetType.StrictDna, GeneticCode.Standard, new List<string> { _label });
            var multilineWithLabelAndTags = new DnaSequence(_normalizedNucleotides, AlphabetType.StrictDna, GeneticCode.Standard,
                                                            new List<string> { _label, "Hello world", "HODOR", "@MetadataFTW!", "Derp", "No" });

            yield return new TestCaseData(_singleLineFasta, AlphabetType.StrictDna, singleLineNoTags).SetName("Single line FASTA sequence with label");
            yield return new TestCaseData(_multilineFastaWithLabel, AlphabetType.StrictDna, multilineWithLabel).SetName("Multiline FASTA with label");
            yield return new TestCaseData(_multilineFastaWithMetadataAndTrailingPipes, AlphabetType.StrictDna, multilineWithLabelAndTags)
                                         .SetName("Multiline FASTA with label, tags, and empty trailing pipes");

            yield return new TestCaseData(_crossPlatformMultiLineN, AlphabetType.StrictDna, multilineWithLabelAndTags)
                                         .SetName("Multiline FASTA with label, tags, and empty trailing pipes");
            yield return new TestCaseData(_crossPlatformMultiLineR, AlphabetType.StrictDna, multilineWithLabelAndTags)
                                         .SetName("Multiline FASTA with label, tags, and empty trailing pipes");
        }
    }
}

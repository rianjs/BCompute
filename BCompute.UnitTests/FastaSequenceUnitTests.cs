using System.Collections.Generic;
using NUnit.Framework;

namespace BCompute.UnitTests
{
    [TestFixture]
    public class FastaSequenceUnitTests
    {
        private const string _label = "Rosalind_4172";
        private const string _normalizedNucleotides =
            "GAAAGCTGGCGGACCATCGGAATATCAAAAATCAATGTGCTCGCGGTGGAGAATTGCGATGGCAGTAGTTAAGCTGATCCATACTGAACATGCACGGGCATAGCAAAATTCCAGAAGCCT";

        private const string _singleLineFasta = @">Rosalind_4172
GAAAGCTGGCGGACCATCGGAATATCAAAAATCAATGTGCTCGCGGTGGAGAATTGCGATGGCAGTAGTTAAGCTGATCCATACTGAACATGCACGGGCATAGCAAAATTCCAGAAGCCT";

        private const string _multilineFastaWithLabel = @">Rosalind_4172
GAAAGCTGGCGGACCATCGGAATATCAAAAATCAATGTGCTCGCGGTGGAGAATTGCGAT
GGCAGTAGTTAAGCTGATCCATACTGAACATGCACGGGCATAGCAAAATTCCAGAAGCCT";

        private const string _multilineFastaWithMetadataAndTrailingPipes = @">Rosalind_4172|Hello world|HODOR|@MetadataFTW!|||
GAAAGCTGGCGGACCATCGGAATATCAAAAATCAATGTGCTCGCGGTGGAGAATTGCGAT
GGCAGTAGTTAAGCTGATCCATACTGAACATGCACGGGCATAGCAAAATTCCAGAAGCCT";

        [Test, TestCaseSource("GenerateFastaSequence_TestCases")]
        public void GenerateFastaSequenceTests(string rawData, FastaSequence expected)
        {
            var actual = FastaSequence.GenerateFastaSequence(rawData);
            Assert.AreEqual(expected, actual);
        }

        public IEnumerable<ITestCaseData> GenerateFastaSequence_TestCases()
        {
            var dnaSequence = NucleotideSequence.GenerateNucleotideSequence(_normalizedNucleotides);

            yield return new TestCaseData(_singleLineFasta, new FastaSequence(_label, dnaSequence, null)).SetName("Single line FASTA sequence with label");
            yield return new TestCaseData(_multilineFastaWithLabel, new FastaSequence(_label, dnaSequence, null)).SetName("Multiline FASTA with label");
            yield return
                new TestCaseData(_multilineFastaWithMetadataAndTrailingPipes,
                    new FastaSequence(_label, dnaSequence, new List<string> { "Hello world", "HODOR", "@MetadataFTW!" })).SetName(
                        "Multiline FASTA with metadata and extra trailing pipes");
        }
    }
}

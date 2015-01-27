using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BCompute.UnitTests
{
    [TestFixture]
    public class ProteinSequenceUnitTests
    {
        private const string _standardProtein = "ACDE";
        private const string _extendedProtein = "ABJHG";
        
        [Test, TestCaseSource("Constructor_TestCases")]
        public int Constructor_Tests(string sequence, AlphabetType alphabet)
        {
            var protein = new ProteinSequence(sequence, alphabet, GeneticCode.Standard);
            return protein.Sequence.Length;
        }

        public IEnumerable<ITestCaseData> Constructor_TestCases()
        {
            yield return new TestCaseData(_standardProtein, AlphabetType.StandardProtein).Returns(_standardProtein.Length).SetName("Standard protein with standard alphabet");
            yield return new TestCaseData(_extendedProtein, AlphabetType.StandardProtein).Throws(typeof(ArgumentException)).SetName("Extended protein with standard fails");
            yield return new TestCaseData(_standardProtein, AlphabetType.ExtendedProtein).Returns(_standardProtein.Length).SetName("Standard protein with extended alphabet");
            yield return new TestCaseData(_extendedProtein, AlphabetType.ExtendedProtein).Returns(_extendedProtein.Length).SetName("Extended protein with extended alphabet");
            yield return new TestCaseData("ACUG", AlphabetType.StandardProtein).Throws(typeof(ArgumentException)).SetName("Sequence with RNA characters (U) with standard alphabet fails");
            yield return new TestCaseData("ACUG", AlphabetType.ExtendedProtein).Returns(4).SetName("Sequence with RNA characters (U) with extended alphabet");
        }

        [Test, TestCaseSource("ProteinSequenceTranslation_TestCases")]
        public string ProteinSequence_Tests(NucleotideSequence seq)
        {
            var translation = seq.Translate();
            return translation.Sequence;
        }

        public IEnumerable<ITestCaseData> ProteinSequenceTranslation_TestCases()
        {
            //{"AUU", AminoAcid.Isoleucine},        ATT
            //{"GUU", AminoAcid.Valine},            GTT
            //{"UUC", AminoAcid.Phenylalanine},     TTG

            yield return new TestCaseData(new DnaSequence("ACC", AlphabetType.StrictDna)).Returns("T").SetName("ACC returns T");

            const string strictDna = "ATTGTTAAC";
            yield return new TestCaseData(new DnaSequence(strictDna, AlphabetType.StrictDna)).Returns("IVN").SetName("Strict DNA using strict triplet letters");
            yield return new TestCaseData(new DnaSequence(strictDna, AlphabetType.AmbiguousDna)).Returns("IVN").SetName("Ambiguous DNA using strict triplet letters");
            yield return new TestCaseData(new DnaSequence(strictDna.Substring(0, strictDna.Length - 1), AlphabetType.StrictDna)).Throws(typeof(ArgumentException)).SetName("Strict DNA using 8 letters fails");
            yield return new TestCaseData(new DnaSequence(strictDna.Substring(0, strictDna.Length - 1), AlphabetType.AmbiguousDna)).Returns("IVX").SetName("Ambiguous DNA using 8 letters returns a single unknown");

            const string gappedDna = "ATTGTT-AAC";
            yield return new TestCaseData(new DnaSequence(gappedDna, AlphabetType.AmbiguousDna)).Returns("IV-N").SetName("Ambiguous DNA with a gap, but using strict triplet letters");
            yield return new TestCaseData(new DnaSequence(gappedDna.Substring(0, gappedDna.Length - 1), AlphabetType.AmbiguousDna)).Returns("IV-X").SetName("Ambiguous DNA using 8 letters returns a single unknown");

            yield return new TestCaseData(new DnaSequence(gappedDna, AlphabetType.AmbiguousDna)).Returns("IV-N").SetName("Ambiguous DNA with a gap, but using strict triplet letters");

            const string strictRna = "AUUGUUUUC";
            yield return new TestCaseData(new RnaSequence(strictRna, AlphabetType.StrictRna)).Returns("IVF").SetName("Strict RNA using strict triplet letters");
            yield return new TestCaseData(new RnaSequence(strictRna, AlphabetType.StrictRna)).Returns("IVF").SetName("Ambiguous RNA using strict triplet letters");

            const string gappedRna = "AUUGUU-UUC";
            yield return new TestCaseData(new RnaSequence(gappedRna, AlphabetType.AmbiguousRna)).Returns("IV-F").SetName("Ambiguous, gapped RNA");
            yield return new TestCaseData(new RnaSequence(gappedRna.Substring(0, gappedRna.Length - 1), AlphabetType.AmbiguousRna)).Returns("IV-X").SetName("Ambiguous, gapped RNA using 8 letters returns a gap followed by an unknown");

            const string allAmbiguousNucleotides = "WWWSS";
            yield return new TestCaseData(new RnaSequence(allAmbiguousNucleotides, AlphabetType.AmbiguousRna)).Returns("X").SetName("All ambiguous RNA returns X");
            yield return new TestCaseData(new DnaSequence(allAmbiguousNucleotides, AlphabetType.AmbiguousDna)).Returns("X").SetName("All ambiguous DNA returns X");

            const string ambiguousGapped = allAmbiguousNucleotides + "---" + allAmbiguousNucleotides;
            yield return new TestCaseData(new RnaSequence(ambiguousGapped, AlphabetType.AmbiguousRna)).Returns("X-X").SetName("Ambiguous + gap + ambiguous RNA returns X-X");
            yield return new TestCaseData(new DnaSequence(ambiguousGapped, AlphabetType.AmbiguousDna)).Returns("X-X").SetName("Ambiguous + gap + ambiguous DNA returns X-X");

            const string rosalindTest = "AUGGCCAUGGCGCCCAGAACUGAGAUCAAUAGUACCCGUAUUAACGGGUGA";
            yield return new TestCaseData(new RnaSequence(rosalindTest, AlphabetType.StrictRna)).Returns("MAMAPRTEINSTRING.").SetName("Rosalind.info prot sample data test");
        }


        [Test, TestCaseSource("Tag_TestCases")]
        public void Tag_Tests(ProteinSequence incoming, IEnumerable<string> incomingTags)
        {
            var expected = new HashSet<string>(incomingTags);
            CollectionAssert.AreEquivalent(incoming.Tags, expected);
        }

        public IEnumerable<ITestCaseData> Tag_TestCases()
        {
            var tags = new HashSet<string> { "Foo", "Bar", "Baz" };
            var standardAlphabetWithTags = new ProteinSequence("ACTCTTCAGC", AlphabetType.StandardProtein, GeneticCode.Standard, tags);
            yield return new TestCaseData(standardAlphabetWithTags, tags).SetName("Standard protein alphabet with tags");

            var extendedAlphabetWithTags = new ProteinSequence("AUCUAGCGCGUA", AlphabetType.ExtendedProtein, GeneticCode.Standard, tags);
            yield return new TestCaseData(extendedAlphabetWithTags, tags).SetName("Extended protein alphabet with tags");
        }

        [Test, TestCaseSource("Equality_TestCases")]
        public bool EqualityTests(ProteinSequence a, ProteinSequence b)
        {
            return a.Equals(b);
        }

        public IEnumerable<ITestCaseData> Equality_TestCases()
        {
            const string standard = "ACTGC";
            const string extended = "ACUGC";
            var standardStandard = new ProteinSequence(standard, AlphabetType.StandardProtein, GeneticCode.Standard);
            var standardExtended = new ProteinSequence(standard, AlphabetType.ExtendedProtein, GeneticCode.Standard);
            var extendedExtended = new ProteinSequence(extended, AlphabetType.ExtendedProtein, GeneticCode.Standard);
            var standardDiffStandard = new ProteinSequence(standard.Substring(0, standard.Length - 2), AlphabetType.StandardProtein, GeneticCode.Standard);
            var extendedDiffExtended = new ProteinSequence(extended.Substring(0, extended.Length - 2), AlphabetType.ExtendedProtein, GeneticCode.Standard);

            yield return new TestCaseData(standardStandard, standardStandard).Returns(true).SetName("Standard with standard");
            yield return new TestCaseData(standardStandard, standardExtended).Returns(false).SetName("Standard with extended protein returns false");
            yield return new TestCaseData(standardStandard, standardDiffStandard).Returns(false).SetName("Two different standard protein sequences return false");
            yield return new TestCaseData(extendedExtended, extendedDiffExtended).Returns(false).SetName("Two different extended protein sequences return false");
        }
    }
}

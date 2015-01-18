using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BCompute.UnitTests
{
    [TestFixture]
    public class ProteinSequenceUnitTests
    {
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
    }
}

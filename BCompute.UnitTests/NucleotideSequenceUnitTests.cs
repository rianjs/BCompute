using System;
using System.Collections.Generic;
using BCompute.Data.Alphabets;
using BCompute.Data.GeneticCode;
using NUnit.Framework;

namespace BCompute.UnitTests
{
    [TestFixture]
    public class NucleotideSequenceUnitTests
    {
        #region Strict DNA nucleotide sequences and complements
        private const string _strictDna = "ACGTAGCTAGCTGAAAACGT";
        private const string _strictDnaComplement = "TGCATCGATCGACTTTTGCA";
        private const string _strictDnaReverseComplement = "ACGTTTTCAGCTAGCTACGT";
        #endregion

        #region Ambiguous DNA nucleotide sequences and complements
        private const string _ambiguousDna = "ABGTAGCTAVCTGAAAACGT";
        private const string _ambiguousDnaComplement = "TVCATCGATBGACTTTTGCA";
        private const string _ambiguousDnaReverseComplement = "ACGTTTTCAGBTAGCTACVT";
        #endregion

        #region Strict RNA nucleotide sequences and complements
        private const string _strictRna = "ACGUAGCUAGCUGAAAACGU";
        private const string _strictRnaComplement = "UGCAUCGAUCGACUUUUGCA";
        private const string _strictRnaReverseComplement = "ACGUUUUCAGCUAGCUACGU";
        #endregion

        #region Ambiguous RNA nucleotide sequences and complements
        private const string _ambiguousRna = "BCGUAGCUAGCUGAAAACGV";
        private const string _ambiguousRnaComplement = "VGCAUCGAUCGACUUUUGCB";
        private const string _ambiguousRnaReverseComplement = "BCGUUUUCAGCUAGCUACGV";
        #endregion


        private const string _indeterminateFragment = "AGCGCGCGCCCGGGCCA";

        [Test, TestCaseSource("DnaSequence_SanityChecking_TestCases")]
        public void DnaSequence_SanityChecking(string nucleotides, AlphabetType alphabet)
        {
            var dna = new DnaSequence(nucleotides, alphabet);
            Assert.IsInstanceOf<DnaSequence>(dna);
        }

        public IEnumerable<ITestCaseData> DnaSequence_SanityChecking_TestCases()
        {
            //AMbiguous Rna with Strict Dna alphabet throws
            //Ambiguous Rna with Ambiguous Rna alphabet
            //Ambiguous fragmet with strict dna
            //Ambiguous fragment with ambiguous dna
            //Ambiguous fragment with strict rna
            //Ambiguous fragment with ambiguous rna
            //empty string throws exception
            //Numbers instead of nucleotides throws exception
            yield return new TestCaseData(_strictDna, AlphabetType.StrictDna).SetName("Strict DNA sequence with strict DNA alphabet");
            yield return new TestCaseData(_strictDna, AlphabetType.AmbiguousDna).SetName("Strict DNA sequence with ambiguous DNA alphabet");
            yield return new TestCaseData(_ambiguousDna, AlphabetType.StrictDna).SetName("Ambiguous DNA sequence with strict DNA alphabet throws exception").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_ambiguousDna, AlphabetType.AmbiguousDna).SetName("Ambiguous DNA sequence with ambiguous DNA alphabet");
            yield return new TestCaseData(_strictRna, AlphabetType.StrictDna).SetName("Strict Rna with Strict Dna alphabet throws exception").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_ambiguousDna, AlphabetType.StrictDna).SetName("Ambiguous RNA sequence with strict DNA alphabet throws exception").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_strictRna, AlphabetType.AmbiguousDna).SetName("Strict RNA sequence with ambiguous DNA alphabet throws exception").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_strictRna, AlphabetType.AmbiguousDna).SetName("Strict RNA with Ambiguous Dna alphabet throws exception").Throws(typeof(ArgumentException));

            //yield return new TestCaseData(_strictDna).SetName("Strict DNA sequence with ambiguous DNA alphabet");
            //yield return new TestCaseData(_strictRna).SetName("RNA nucleotide sequence fails").Throws(typeof(ArgumentException));
            //yield return new TestCaseData("123456798").SetName("Numbers instead of nucleotide acids").Throws(typeof(ArgumentException));
            //yield return new TestCaseData(String.Empty).SetName("Empty string instead of nucleotide acids").Throws(typeof(ArgumentException));
        }

        //[Test, TestCaseSource("RnaSequence_SanityChecking_TestCases")]
        //public void RnaSequence_SanityChecking(string nucleotides)
        //{
        //    var rna = new RnaSequence(nucleotides);
        //    Assert.IsInstanceOf<RnaSequence>(rna);
        //}

        //public IEnumerable<ITestCaseData> RnaSequence_SanityChecking_TestCases()
        //{
        //    yield return new TestCaseData(_strictRna).SetName("Normal RNA sequence");
        //    yield return new TestCaseData(_strictDna).SetName("DNA nucleotide sequence fails").Throws(typeof(ArgumentException));
        //    yield return new TestCaseData("123456798").SetName("Numbers instead of nucleotide acids").Throws(typeof(ArgumentException));
        //    yield return new TestCaseData(String.Empty).SetName("Empty string instead of nucleotide acids").Throws(typeof(ArgumentException));
        //}

        [Test, TestCaseSource("Equality_TestCases")]
        public bool EqualityTests(NucleotideSequence a, NucleotideSequence b)
        {
            return a.Equals(b);
        }

        //public IEnumerable<ITestCaseData> Equality_TestCases()
        //{
        //    var dnaA = new DnaSequence(_strictDna);
        //    var dnaB = new DnaSequence(_strictDna);
        //    yield return new TestCaseData(dnaA, dnaB).Returns(true).SetName("Two DNA objects with the same sequence");
        //    yield return new TestCaseData(dnaA, new DnaSequence(_strictDnaComplement)).Returns(false).SetName("Two DNA objects with different sequences");

        //    var rnaA = new RnaSequence(_strictRna);
        //    var rnaB = new RnaSequence(_strictRna);
        //    yield return new TestCaseData(rnaA, rnaB).Returns(true).SetName("Two RNA objects with the same sequence");
        //    yield return new TestCaseData(rnaA, new RnaSequence(_strictRnaComplement)).Returns(false).SetName("Two RNA objects with different sequences");

        //    yield return new TestCaseData(rnaA, dnaA).Returns(false).SetName("A DNA and an RNA object are not equal");
        //    yield return new TestCaseData(new RnaSequence(_indeterminateFragment), new DnaSequence(_indeterminateFragment)).Returns(false).SetName("An RNA object and a DNA object with the same fragment are not equal");
        //}

        //[Test, TestCaseSource("GenerateNucleotideSequence_TestCases")]
        //public void GenerateNucleotideSequenceTest(string nucleotides, NucleotideSequence expected)
        //{
        //    var actual = NucleotideSequence.GenerateNucleotideSequence(nucleotides);
        //    Assert.AreEqual(expected, actual);
        //}

        //public IEnumerable<ITestCaseData> GenerateNucleotideSequence_TestCases()
        //{
        //    yield return new TestCaseData(_strictDna, new DnaSequence(_strictDna)).SetName("DNA nucleotides with DNA type");
        //    yield return new TestCaseData(_strictRna, new RnaSequence(_strictRna)).SetName("RNA nucleotides with RNA type");
        //}

        //[Test, TestCaseSource("ComputeHammingDistance_TestCases")]
        //public long ComputeHammingDistanceTest(NucleotideSequence a, NucleotideSequence b)
        //{
        //    return NucleotideSequence.CalculateHammingDistance(a, b);
        //}

        //public IEnumerable<ITestCaseData> ComputeHammingDistance_TestCases()
        //{
        //    var dnaA = new DnaSequence(_strictDna);
        //    var dnaB = new DnaSequence(_strictDna);
        //    var compDnaA = new DnaSequence(_strictDnaComplement);

        //    yield return new TestCaseData(dnaA, dnaB).Returns(0).SetName("Same DNA sequences return 0");
        //    yield return new TestCaseData(dnaA, compDnaA).Returns(20).SetName("DNA sequences off by 20 characters return 20");


        //    var rnaA = new RnaSequence(_strictRna);
        //    var rnaB = new RnaSequence(_strictRna);
        //    var compRnaA = new RnaSequence(_strictRnaComplement);

        //    yield return new TestCaseData(rnaA, rnaB).Returns(0).SetName("Same RNA sequences return 0");
        //    yield return new TestCaseData(rnaA, compRnaA).Returns(20).SetName("RNA sequences off by 20 characters return 20");

        //    yield return
        //        new TestCaseData(rnaA, dnaA).Throws(typeof (ArgumentException))
        //            .SetName("Computing the Hamming distance between and RNA strand and a DNA strand throws and exception");
        //}

        //[Test, TestCaseSource("ComplementAndReverseComplement_TestCases")]
        //public void ComplementAndReverseComplementTests(NucleotideSequence actual, NucleotideSequence expectedComplement, NucleotideSequence expectedReverseComplement)
        //{
        //    var computedComplement = NucleotideSequence.GenerateNucleotideSequence(actual.Complement);
        //    var computedReverseComplement = NucleotideSequence.GenerateNucleotideSequence(actual.ReverseComplement);

        //    Assert.AreEqual(expectedComplement, computedComplement);
        //    Assert.AreEqual(computedReverseComplement, expectedReverseComplement);
        //}

        //public IEnumerable<ITestCaseData> ComplementAndReverseComplement_TestCases()
        //{
        //    var dna = new DnaSequence(_strictDna);
        //    var dnaComplement = new DnaSequence(_strictDnaComplement);
        //    var dnaReverseComplement = new DnaSequence(_strictDnaReverseComplement);
        //    yield return new TestCaseData(dna, dnaComplement, dnaReverseComplement).SetName("DNA sequence complement and reverse complement");

        //    var rna = new RnaSequence(_strictRna);
        //    var rnaComplement = new RnaSequence(_strictRnaComplement);
        //    var rnaReverseComplement = new RnaSequence(_strictRnaReverseComplement);
        //    yield return new TestCaseData(rna, rnaComplement, rnaReverseComplement).SetName("RNA sequence complement and reverse complement");
        //}
    }
}

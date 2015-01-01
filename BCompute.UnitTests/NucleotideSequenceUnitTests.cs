using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BCompute.UnitTests
{
    [TestFixture]
    public class NucleotideSequenceUnitTests
    {
        private const string _dnaNucleotides = "ACGTAGCTAGCTGAAAACGT";
        private const string _dnaComplement = "TGCATCGATCGACTTTTGCA";
        private const string _dnaReverseComplement = "ACGTTTTCAGCTAGCTACGT";

        private const string _rnaNucleotides = "ACGUAGCUAGCUGAAAACGU";
        private const string _rnaComplement = "UGCAUCGAUCGACUUUUGCA";
        private const string _rnaReverseComplement = "ACGUUUUCAGCUAGCUACGU";

        private const string _ambiguousFragment = "GCGCGCGCCCGGGCC";

        [Test, TestCaseSource("DnaSequence_SanityChecking_TestCases")]
        public void DnaSequence_SanityChecking(string nucleotides)
        {
            var dna = new DnaSequence(nucleotides);
            Assert.IsInstanceOf<DnaSequence>(dna);
        }

        public IEnumerable<ITestCaseData> DnaSequence_SanityChecking_TestCases()
        {
            yield return new TestCaseData(_dnaNucleotides).SetName("Normal DNA sequence");
            yield return new TestCaseData(_rnaNucleotides).SetName("RNA nucleotide sequence fails").Throws(typeof(ArgumentException));
            yield return new TestCaseData("123456798").SetName("Numbers instead of nucleotide acids").Throws(typeof(ArgumentException));
            yield return new TestCaseData(String.Empty).SetName("Empty string instead of nucleotide acids").Throws(typeof(ArgumentException));
        }

        [Test, TestCaseSource("RnaSequence_SanityChecking_TestCases")]
        public void RnaSequence_SanityChecking(string nucleotides)
        {
            var rna = new RnaSequence(nucleotides);
            Assert.IsInstanceOf<RnaSequence>(rna);
        }

        public IEnumerable<ITestCaseData> RnaSequence_SanityChecking_TestCases()
        {
            yield return new TestCaseData(_rnaNucleotides).SetName("Normal RNA sequence");
            yield return new TestCaseData(_dnaNucleotides).SetName("DNA nucleotide sequence fails").Throws(typeof(ArgumentException));
            yield return new TestCaseData("123456798").SetName("Numbers instead of nucleotide acids").Throws(typeof(ArgumentException));
            yield return new TestCaseData(String.Empty).SetName("Empty string instead of nucleotide acids").Throws(typeof(ArgumentException));
        }

        [Test, TestCaseSource("Equality_TestCases")]
        public bool EqualityTests(NucleotideSequence a, NucleotideSequence b)
        {
            return a.Equals(b);
        }

        public IEnumerable<ITestCaseData> Equality_TestCases()
        {
            var dnaA = new DnaSequence(_dnaNucleotides);
            var dnaB = new DnaSequence(_dnaNucleotides);
            yield return new TestCaseData(dnaA, dnaB).Returns(true).SetName("Two DNA objects with the same sequence");
            yield return new TestCaseData(dnaA, new DnaSequence(_dnaComplement)).Returns(false).SetName("Two DNA objects with different sequences");

            var rnaA = new RnaSequence(_rnaNucleotides);
            var rnaB = new RnaSequence(_rnaNucleotides);
            yield return new TestCaseData(rnaA, rnaB).Returns(true).SetName("Two RNA objects with the same sequence");
            yield return new TestCaseData(rnaA, new RnaSequence(_rnaComplement)).Returns(false).SetName("Two RNA objects with different sequences");

            yield return new TestCaseData(rnaA, dnaA).Returns(false).SetName("A DNA and an RNA object are not equal");
            yield return new TestCaseData(new RnaSequence(_ambiguousFragment), new DnaSequence(_ambiguousFragment)).Returns(false).SetName("An RNA object and a DNA object with the same fragment are not equal");
        }

        [Test, TestCaseSource("GenerateNucleotideSequence_TestCases")]
        public void GenerateNucleotideSequenceTest(string nucleotides, NucleotideSequence expected)
        {
            var actual = NucleotideSequence.GenerateNucleotideSequence(nucleotides);
            Assert.AreEqual(expected, actual);
        }

        public IEnumerable<ITestCaseData> GenerateNucleotideSequence_TestCases()
        {
            yield return new TestCaseData(_dnaNucleotides, new DnaSequence(_dnaNucleotides)).SetName("DNA nucleotides with DNA type");
            yield return new TestCaseData(_rnaNucleotides, new RnaSequence(_rnaNucleotides)).SetName("RNA nucleotides with RNA type");
        }

        [Test, TestCaseSource("ComputeHammingDistance_TestCases")]
        public long ComputeHammingDistanceTest(NucleotideSequence a, NucleotideSequence b)
        {
            return NucleotideSequence.ComputeHammingDistance(a, b);
        }

        public IEnumerable<ITestCaseData> ComputeHammingDistance_TestCases()
        {
            var dnaA = new DnaSequence(_dnaNucleotides);
            var dnaB = new DnaSequence(_dnaNucleotides);
            var compDnaA = new DnaSequence(_dnaComplement);

            yield return new TestCaseData(dnaA, dnaB).Returns(0).SetName("Same DNA sequences return 0");
            yield return new TestCaseData(dnaA, compDnaA).Returns(20).SetName("DNA sequences off by 20 characters return 20");


            var rnaA = new RnaSequence(_rnaNucleotides);
            var rnaB = new RnaSequence(_rnaNucleotides);
            var compRnaA = new RnaSequence(_rnaComplement);

            yield return new TestCaseData(rnaA, rnaB).Returns(0).SetName("Same RNA sequences return 0");
            yield return new TestCaseData(rnaA, compRnaA).Returns(20).SetName("RNA sequences off by 20 characters return 20");

            yield return
                new TestCaseData(rnaA, dnaA).Throws(typeof (ArgumentException))
                    .SetName("Computing the Hamming distance between and RNA strand and a DNA strand throws and exception");
        }

        [Test, TestCaseSource("ComplementAndReverseComplement_TestCases")]
        public void ComplementAndReverseComplementTests(NucleotideSequence actual, NucleotideSequence expectedComplement, NucleotideSequence expectedReverseComplement)
        {
            var computedComplement = NucleotideSequence.GenerateNucleotideSequence(actual.Complement);
            var computedReverseComplement = NucleotideSequence.GenerateNucleotideSequence(actual.ReverseComplement);

            Assert.AreEqual(expectedComplement, computedComplement);
            Assert.AreEqual(computedReverseComplement, expectedReverseComplement);
        }

        public IEnumerable<ITestCaseData> ComplementAndReverseComplement_TestCases()
        {
            var dna = new DnaSequence(_dnaNucleotides);
            var dnaComplement = new DnaSequence(_dnaComplement);
            var dnaReverseComplement = new DnaSequence(_dnaReverseComplement);
            yield return new TestCaseData(dna, dnaComplement, dnaReverseComplement).SetName("DNA sequence complement and reverse complement");

            var rna = new RnaSequence(_rnaNucleotides);
            var rnaComplement = new RnaSequence(_rnaComplement);
            var rnaReverseComplement = new RnaSequence(_rnaReverseComplement);
            yield return new TestCaseData(rna, rnaComplement, rnaReverseComplement).SetName("RNA sequence complement and reverse complement");
        }
    }
}

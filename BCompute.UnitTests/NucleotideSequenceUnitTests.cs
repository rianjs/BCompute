using System;
using System.Collections.Generic;
using BCompute.Data.Alphabets;
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
            #region Mixing and matching ambiguous and strict DNA and RNA alphabets with ambiguous and strict DNA and RNA sequences
            yield return new TestCaseData(_strictDna, AlphabetType.StrictDna).SetName("Strict DNA sequence with strict DNA alphabet");
            yield return new TestCaseData(_strictDna, AlphabetType.AmbiguousDna).SetName("Strict DNA sequence with ambiguous DNA alphabet");
            yield return new TestCaseData(_ambiguousDna, AlphabetType.StrictDna).SetName("Ambiguous DNA sequence with strict DNA alphabet fails").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_ambiguousDna, AlphabetType.AmbiguousDna).SetName("Ambiguous DNA sequence with ambiguous DNA alphabet");
            yield return new TestCaseData(_strictRna, AlphabetType.StrictDna).SetName("Strict Rna with Strict Dna alphabet fails").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_ambiguousDna, AlphabetType.StrictDna).SetName("Ambiguous RNA sequence with strict DNA alphabet fails").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_strictRna, AlphabetType.AmbiguousDna).SetName("Strict RNA with Ambiguous Dna alphabet fails").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_ambiguousRna, AlphabetType.AmbiguousDna).SetName("Ambiguous RNA with Ambiguous Dna alphabet fails").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_ambiguousRna, AlphabetType.StrictDna).SetName("Ambiguous RNA with Strict Dna alphabet fails").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_strictRna, AlphabetType.AmbiguousDna).SetName("Strict RNA with Ambiguous Dna alphabet fails").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_strictRna, AlphabetType.StrictRna).SetName("Strict RNA with Strict RNA alphabet").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_ambiguousRna, AlphabetType.AmbiguousRna).SetName("Ambiguous RNA with Ambiguous RNA alphabet fails").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_strictRna, AlphabetType.AmbiguousRna).SetName("Strict RNA with Ambiguous RNA alphabet fails").Throws(typeof(ArgumentException));
            #endregion

            #region Ambiguous fragment testing
            yield return new TestCaseData(_indeterminateFragment, AlphabetType.StrictDna).SetName("Ambiguous fragment with Strict DNA alphabet");
            yield return new TestCaseData(_indeterminateFragment, AlphabetType.AmbiguousDna).SetName("Ambiguous fragment with Ambiguous DNA alphabet");
            yield return new TestCaseData(_indeterminateFragment, AlphabetType.StrictRna).SetName("Ambiguous fragment with Strict RNA alphabet").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_indeterminateFragment, AlphabetType.AmbiguousRna).SetName("Ambiguous fragment with Ambiguous DNA alphabet").Throws(typeof(ArgumentException));
            #endregion

            #region Nonspecific tests
            yield return new TestCaseData("acgtc123456798", AlphabetType.AmbiguousDna).SetName("Numbers instead of nucleotide acids fails").Throws(typeof(ArgumentException));
            yield return new TestCaseData(String.Empty, AlphabetType.AmbiguousDna).SetName("Empty string fails").Throws(typeof(ArgumentException));
            #endregion
        }

        [Test, TestCaseSource("RnaSequence_SanityChecking_TestCases")]
        public void RnaSequence_SanityChecking(string nucleotides, AlphabetType alphabet)
        {
            var rna = new RnaSequence(nucleotides, alphabet);
            Assert.IsInstanceOf<RnaSequence>(rna);
        }

        public IEnumerable<ITestCaseData> RnaSequence_SanityChecking_TestCases()
        {
            #region Mixing and matching ambiguous and strict DNA and RNA alphabets with ambiguous and strict DNA and RNA sequences
            yield return new TestCaseData(_strictDna, AlphabetType.StrictDna).SetName("Strict DNA sequence with strict DNA alphabet").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_strictDna, AlphabetType.AmbiguousDna).SetName("Strict DNA sequence with ambiguous DNA alphabet").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_ambiguousDna, AlphabetType.StrictDna).SetName("Ambiguous DNA sequence with strict DNA alphabet fails").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_ambiguousDna, AlphabetType.AmbiguousDna).SetName("Ambiguous DNA sequence with ambiguous DNA alphabet").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_strictRna, AlphabetType.StrictDna).SetName("Strict Rna with Strict Dna alphabet fails").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_ambiguousDna, AlphabetType.StrictDna).SetName("Ambiguous RNA sequence with strict DNA alphabet fails").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_strictRna, AlphabetType.AmbiguousDna).SetName("Strict RNA sequence with ambiguous DNA alphabet fails").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_ambiguousRna, AlphabetType.AmbiguousDna).SetName("Ambiguous RNA with Ambiguous Dna alphabet fails").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_ambiguousRna, AlphabetType.StrictDna).SetName("Ambiguous RNA with Strict Dna alphabet fails").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_strictRna, AlphabetType.AmbiguousDna).SetName("Strict RNA with Ambiguous Dna alphabet fails").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_strictRna, AlphabetType.StrictRna).SetName("Strict RNA with Strict RNA alphabet");
            yield return new TestCaseData(_ambiguousRna, AlphabetType.AmbiguousRna).SetName("Ambiguous RNA with Ambiguous RNA alphabet fails");
            yield return new TestCaseData(_strictRna, AlphabetType.AmbiguousRna).SetName("Strict RNA with Ambiguous RNA alphabet fails");
            #endregion

            #region Ambiguous fragment testing
            yield return new TestCaseData(_indeterminateFragment, AlphabetType.StrictDna).SetName("Ambiguous fragment with Strict DNA alphabet").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_indeterminateFragment, AlphabetType.AmbiguousDna).SetName("Ambiguous fragment with Ambiguous DNA alphabet").Throws(typeof(ArgumentException));
            yield return new TestCaseData(_indeterminateFragment, AlphabetType.StrictRna).SetName("Ambiguous fragment with Strict RNA alphabet");
            yield return new TestCaseData(_indeterminateFragment, AlphabetType.AmbiguousRna).SetName("Ambiguous fragment with Ambiguous DNA alphabet");
            #endregion

            #region Nonspecific tests
            yield return new TestCaseData("acgtc123456798", AlphabetType.AmbiguousDna).SetName("Numbers instead of nucleotide acids fails").Throws(typeof(ArgumentException));
            yield return new TestCaseData(String.Empty, AlphabetType.AmbiguousDna).SetName("Empty string fails").Throws(typeof(ArgumentException));
            #endregion
        }

        [Test, TestCaseSource("Equality_TestCases")]
        public bool EqualityTests(NucleotideSequence a, NucleotideSequence b)
        {
            return a.Equals(b);
        }

        private static readonly DnaSequence _strictDnaSequence = new DnaSequence(_strictDna, AlphabetType.StrictDna);
        private static readonly DnaSequence _strictDnaWithAmbiguousAlphabet = new DnaSequence(_strictDna, AlphabetType.AmbiguousDna);
        private static readonly DnaSequence _ambiguousDnaSequence = new DnaSequence(_ambiguousDna, AlphabetType.AmbiguousDna);

        private static readonly RnaSequence _strictRnaSequence = new RnaSequence(_strictRna, AlphabetType.StrictRna);
        private static readonly RnaSequence _strictRnaWithAmbiguousAlphabet = new RnaSequence(_strictRna, AlphabetType.AmbiguousRna);
        private static readonly RnaSequence _ambiguousRnaSequence = new RnaSequence(_ambiguousRna, AlphabetType.AmbiguousRna);
        private static readonly DnaSequence _indeterminateDnaSequence = new DnaSequence(_indeterminateFragment, AlphabetType.StrictDna);
        private static readonly RnaSequence _indeterminateRnaSequence = new RnaSequence(_indeterminateFragment, AlphabetType.StrictRna);
        public IEnumerable<ITestCaseData> Equality_TestCases()
        {
            yield return new TestCaseData(_strictDnaSequence, _strictDnaWithAmbiguousAlphabet).Returns(true).SetName("DNA objects with different alphabets, but the same nucleotides are equal");
            yield return new TestCaseData(_strictDnaSequence, _ambiguousDnaSequence).Returns(false).SetName("Two DNA objects with different sequences");

            yield return new TestCaseData(_strictRnaSequence, _strictRnaWithAmbiguousAlphabet).Returns(true).SetName("Two RNA objects with the same sequence but different alphabets");
            yield return new TestCaseData(_strictRnaSequence, _ambiguousRnaSequence).Returns(false).SetName("Two RNA objects with different sequences");

            yield return new TestCaseData(_strictDnaSequence, _strictRnaSequence).Returns(false).SetName("A DNA and an RNA object are not equal");
            yield return new TestCaseData(_indeterminateDnaSequence, _indeterminateRnaSequence).Returns(false).SetName("An RNA object and a DNA object with the same fragment are not equal");
        }

        [Test, TestCaseSource("ComputeHammingDistance_TestCases")]
        public long ComputeHammingDistanceTest(NucleotideSequence a, NucleotideSequence b)
        {
            return NucleotideSequence.CalculateHammingDistance(a, b);
        }

        public IEnumerable<ITestCaseData> ComputeHammingDistance_TestCases()
        {
            var compDnaA = new DnaSequence(_strictDnaComplement, AlphabetType.StrictDna);

            yield return new TestCaseData(_strictDnaSequence, _strictDnaSequence).Returns(0).SetName("Same DNA sequences return 0");
            yield return new TestCaseData(_strictDnaSequence, compDnaA).Returns(20).SetName("DNA sequences off by 20 characters return 20");
            yield return new TestCaseData(_strictDnaSequence, _strictDnaWithAmbiguousAlphabet).SetName("DNA sequences with different alphabets fails").Throws(typeof(ArgumentException));

            var compRnaA = new RnaSequence(_strictRnaComplement, AlphabetType.StrictRna);

            yield return new TestCaseData(_strictRnaSequence, _strictRnaSequence).Returns(0).SetName("Same RNA sequences return 0");
            yield return new TestCaseData(_strictRnaSequence, compRnaA).Returns(20).SetName("RNA sequences off by 20 characters return 20");
            yield return new TestCaseData(_strictRnaSequence, _strictRnaWithAmbiguousAlphabet).SetName("RNA sequences with different alphabets fails").Throws(typeof(ArgumentException));

            yield return new TestCaseData(_strictDnaSequence, _strictRnaSequence).SetName("Comparing an RNA sequence to a DNA sequence fails").Throws(typeof(ArgumentException));
        }

        [Test, TestCaseSource("ComplementAndReverseComplement_TestCases")]
        public void ComplementAndReverseComplementTests(NucleotideSequence actual, NucleotideSequence expectedComplement, NucleotideSequence expectedReverseComplement)
        {
            var computedComplement = actual.Complement;
            var computedReverseComplement = actual.ReverseComplement;

            Assert.AreEqual(expectedComplement.Sequence, computedComplement.Sequence);
            CollectionAssert.AreEquivalent(expectedComplement.SymbolCounts, computedComplement.SymbolCounts);
            Assert.AreEqual(computedReverseComplement.Sequence, expectedReverseComplement.Sequence);
        }

        public IEnumerable<ITestCaseData> ComplementAndReverseComplement_TestCases()
        {
            var strictDnaComplement = new DnaSequence(_strictDnaComplement, AlphabetType.StrictDna);
            var strictDnaReverseComplement = new DnaSequence(_strictDnaReverseComplement, AlphabetType.StrictDna);
            yield return new TestCaseData(_strictDnaSequence, strictDnaComplement, strictDnaReverseComplement).SetName("DNA sequence complement and reverse complement");

            var rna = new RnaSequence(_strictRna, AlphabetType.StrictRna);
            var rnaComplement = new RnaSequence(_strictRnaComplement, AlphabetType.StrictRna);
            var rnaReverseComplement = new RnaSequence(_strictRnaReverseComplement, AlphabetType.StrictRna);
            yield return new TestCaseData(rna, rnaComplement, rnaReverseComplement).SetName("RNA sequence complement and reverse complement");
        }
    }
}

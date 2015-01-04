using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BCompute
{
    public abstract class NucleotideSequence : INucleotideSequence
    {
        public string BasePairs { get; protected set; }
        public abstract ImmutableHashSet<char> AllowedNucleotides { get; }
        protected abstract ImmutableDictionary<char, char> BasePairComplements { get; }

        internal NucleotideSequence(string rawBasePairs)
        {
            rawBasePairs = rawBasePairs.Trim();
            InitializeBasePairs(rawBasePairs);
        }

        protected void InitializeBasePairs(string incomingBasePairs)
        {
            if (String.IsNullOrWhiteSpace(incomingBasePairs))
            {
                throw new ArgumentException("Empty nucleotide sequence!");
            }

            _nucleotideCounts = new Dictionary<char, ulong>(AllowedNucleotides.Count);
            foreach (var basePair in AllowedNucleotides)
            {
                _nucleotideCounts.Add(basePair, 0);
            }

            foreach (var basePair in incomingBasePairs)
            {
                var upper = Char.ToUpperInvariant(basePair);
                if (!AllowedNucleotides.Contains(basePair))
                {
                    throw new ArgumentException(basePair + " is not a recognized nucleotide");
                }
                _nucleotideCounts[upper]++;
            }
            BasePairs = incomingBasePairs;
        }

        private Dictionary<char, ulong> _nucleotideCounts;
        public IDictionary<char, ulong> NucleotideCounts
        {
            get
            {
                return _nucleotideCounts;
            }
        }

        public long CalculateHammingDistance(NucleotideSequence nucleotideSequence)
        {
            return ComputeHammingDistance(this, nucleotideSequence);
        }

        private string _complement;
        public string Complement
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_complement))
                {
                    GenerateComplement();
                }

                return _complement;
            }
        }

        private string _reverseComplement;
        public string ReverseComplement
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_reverseComplement))
                {
                    var complement = Complement;
                    _reverseComplement = new string(complement.Reverse().ToArray());
                }
                return _reverseComplement;
            }
        }

        protected void GenerateComplement()
        {
            var complement = new char[BasePairs.Length];
            for (var i = 0; i < BasePairs.Length; i++)
            {
                complement[i] = BasePairComplements[BasePairs[i]];
            }

            _complement = new string(complement);
        }

        public static int ComputeHammingDistance(NucleotideSequence a, NucleotideSequence b)
        {
            if (a.GetType() != b.GetType())
            {
                throw new ArgumentException(String.Format("Sequence types do not match. ({0} vs {1})", a.GetType(), b.GetType()));
            }

            if (a.BasePairs.Length != b.BasePairs.Length)
            {
                throw new ArgumentException("Strands are of unequal length!");
            }

            return a.BasePairs.Where((t, i) => t != b.BasePairs[i]).Count();
        }

        public static IEnumerable<NucleotideSequence> GenerateNucleotideSequences(IEnumerable<string> rawSequences)
        {
            var internalRawSequences = rawSequences as IList<string> ?? rawSequences.ToList();
            var nucleotideSequences = new List<NucleotideSequence>(internalRawSequences.Count);
            nucleotideSequences.AddRange(internalRawSequences.Select(GenerateNucleotideSequence));
            return nucleotideSequences;
        }

        public static NucleotideSequence GenerateNucleotideSequence(string nucleotides)
        {
            foreach (var nucleotide in nucleotides)
            {
                switch (Char.ToUpperInvariant(nucleotide))
                {
                    case 'T':
                        return new DnaSequence(nucleotides);
                    case 'U':
                        return new RnaSequence(nucleotides);
                }
            }
            throw new ArgumentException("Ambiguous nucleotide sequence. It didn't have thymine or uracil");
        }

        public double GcPercentage
        {
            get
            {
                var gcCount = NucleotideCounts['G'] + NucleotideCounts['C'];
                return (double) gcCount / BasePairs.Length;
            }
        }

        public ulong GuanineCount
        {
            get { return NucleotideCounts['G']; }
        }

        public ulong CytosineCount
        {
            get { return NucleotideCounts['C']; }
        }

        public ulong AdenineCount
        {
            get { return NucleotideCounts['A']; }
        }

        /// <summary>
        /// Equality semantics are VALUE-oriented, checking whether the types and nucleotide acid chains are the same.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var typedComparison = (INucleotideSequence) obj;
            if (GetType() != typedComparison.GetType())
            {
                return false;
            }

            if (BasePairs.Length != typedComparison.BasePairs.Length)
            {
                return false;
            }

            return String.Equals(BasePairs, typedComparison.BasePairs, StringComparison.OrdinalIgnoreCase);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BCompute
{
    public abstract class NucleotideSequence : INucleotideSequence
    {
        public string Sequence { get; protected set; }
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

            _codeCounts = new Dictionary<char, long>(AllowedCodes.Count);
            foreach (var basePair in AllowedCodes)
            {
                _codeCounts.Add(basePair, 0);
            }

            foreach (var basePair in incomingBasePairs)
            {
                var upper = Char.ToUpperInvariant(basePair);
                if (!AllowedCodes.Contains(basePair))
                {
                    throw new ArgumentException(basePair + " is not a recognized nucleotide");
                }
                _codeCounts[upper]++;
            }
            Sequence = incomingBasePairs;
        }

        private Dictionary<char, long> _codeCounts;
        public IDictionary<char, long> CodeCounts
        {
            get
            {
                return _codeCounts;
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
            var complement = new char[Sequence.Length];
            for (var i = 0; i < Sequence.Length; i++)
            {
                complement[i] = BasePairComplements[Sequence[i]];
            }

            _complement = new string(complement);
        }

        public static int ComputeHammingDistance(NucleotideSequence a, NucleotideSequence b)
        {
            if (a.GetType() != b.GetType())
            {
                throw new ArgumentException(String.Format("Sequence types do not match. ({0} vs {1})", a.GetType(), b.GetType()));
            }

            if (a.Sequence.Length != b.Sequence.Length)
            {
                throw new ArgumentException("Strands are of unequal length!");
            }

            return a.Sequence.Where((t, i) => t != b.Sequence[i]).Count();
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
                var gcCount = CodeCounts['G'] + CodeCounts['C'];
                return (double) gcCount / Sequence.Length;
            }
        }

        public long GuanineCount
        {
            get { return CodeCounts['G']; }
        }

        public long CytosineCount
        {
            get { return CodeCounts['C']; }
        }

        public long AdenineCount
        {
            get { return CodeCounts['A']; }
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

            var typedComparison = (ISequence) obj;
            if (GetType() != typedComparison.GetType())
            {
                return false;
            }

            if (Sequence.Length != typedComparison.Sequence.Length)
            {
                return false;
            }

            return String.Equals(Sequence, typedComparison.Sequence, StringComparison.OrdinalIgnoreCase);
        }

        private Dictionary<char, char> _nucleotideComplements;
        public IDictionary<char, char> NucleotideComplements
        {
            //Defined at http://www.chem.qmul.ac.uk/iubmb/misc/naseq.html#tab2
            get
            {
                if (_nucleotideComplements == null || _nucleotideComplements.Count < Enum.GetNames(typeof(Nucleotide)).Length)
                {
                    _nucleotideComplements = new Dictionary<char, char>
                    {
                        {(char) Nucleotide.Adenine, (char) Nucleotide.Thymine},         //A <=> T
                        {(char) Nucleotide.Thymine, (char) Nucleotide.Adenine},
                        {(char) Nucleotide.NotAdenine, (char) Nucleotide.NotThymine},   //B <=> V
                        {(char) Nucleotide.NotThymine, (char) Nucleotide.NotAdenine},
                        {(char) Nucleotide.Cytosine, (char) Nucleotide.Guanine},        //C <=> G
                        {(char) Nucleotide.Guanine, (char) Nucleotide.Cytosine},
                        {(char) Nucleotide.NotGuanine, (char) Nucleotide.NotCytosine},  //D <=> H
                        {(char) Nucleotide.NotCytosine, (char) Nucleotide.NotGuanine},
                        {(char) Nucleotide.Keto, (char) Nucleotide.Amino},              //K <=> M
                        {(char) Nucleotide.Amino, (char) Nucleotide.Keto},
                        {(char) Nucleotide.Strong, (char) Nucleotide.Strong},           //S <=> S
                        {(char) Nucleotide.Weak, (char) Nucleotide.Weak},               //W <=> W
                        {(char) Nucleotide.Unknown, (char) Nucleotide.Unknown},         //N <=> N
                        {(char) Nucleotide.Gap, (char) Nucleotide.Gap},                 //- <=> -
                    };
                }
                return _nucleotideComplements;
            }
        }

        public int GapCount { get; private set; }
        public int AnyBaseCount { get; private set; }
        public abstract ISet<char> AllowedCodes { get; }
        public ISet<string> Tags { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCompute.Data.Alphabets;
using BCompute.Data.GeneticCode;

namespace BCompute
{
    public abstract class NucleotideSequence : INucleotideSequence
    {
        private const double _defaultDoubleValue = -1.00d;
        private const int _defaultIntValue = -1;

        public NucleotideAlphabet NucleotideAlphabet { get; protected set; }
        public AlphabetType ActiveAlphabet { get; protected set; }
        public GeneticCode GeneticCode { get; protected set; }

        protected NucleotideSequence(string sequence, AlphabetType alphabet, GeneticCode geneticCode)
        {
            NucleotideAlphabet = new NucleotideAlphabet(alphabet, geneticCode);
            ActiveAlphabet = alphabet;
            GeneticCode = geneticCode;
            Sequence = sequence;        //ToDo: Make sure this has been sanity checked by the subclasses first?
        }

        protected NucleotideSequence(string sequence, AlphabetType alphabet, GeneticCode geneticCode, Dictionary<Nucleotide, long> symbolCounts)
        {
            NucleotideAlphabet = new NucleotideAlphabet(alphabet, geneticCode);
            ActiveAlphabet = alphabet;
            GeneticCode = geneticCode;
            Sequence = sequence;
        }

        private HashSet<Nucleotide> _allowedSymbols;
        public virtual ISet<Nucleotide> AllowedSymbols
        {
            get
            {
                if (_allowedSymbols == null)
                {
                    _allowedSymbols = new HashSet<Nucleotide>(NucleotideAlphabet.AllowedSymbols);
                }
                return _allowedSymbols;
            }
        }

        public virtual IDictionary<string, AminoAcid> TranslationTable
        {
            get { return NucleotideAlphabet.TranslationTable; }
        }

        public virtual INucleotideSequence Transcribe()
        {
            throw new NotImplementedException();
            //if this is a DNA sequence, we should return an RMA sequence

            //if this is an RNA sequence, we should return a DNA sequence
        }

        public Dictionary<Nucleotide, long> SymbolCounts { get; protected set; }
        public virtual long NucleotideCount(Nucleotide nucleotide)
        {
            if (!_allowedSymbols.Contains(nucleotide))
            {
                throw new ArgumentException(String.Format("{0} is not a valid nucleotide for a sequence with a {1} ActiveAlphabet", nucleotide, ActiveAlphabet));
            }
            return SymbolCounts[nucleotide];
        }    

        public string Sequence { get; protected set; }

        private double _gcPercentage = _defaultDoubleValue;
        public virtual double GcContentPercentage
        {
            get
            {
                if (_gcPercentage == _defaultDoubleValue)
                {
                    _gcPercentage = Math.Round(((double)GcCount / Sequence.Length), Constants.RoundingPrecision);
                }
                return _gcPercentage;
            }
        }

        public virtual ISet<Nucleotide> GcContentSymbols
        {
            get { return NucleotideAlphabet.GcContentSymbols; }
        }

        protected long _hammingDistance = _defaultIntValue;
        public virtual long CalculateHammingDistance(NucleotideSequence comparisonSequence)
        {
            if (_hammingDistance == _defaultIntValue)
            {
                _hammingDistance = CalculateHammingDistance(this, comparisonSequence);
            }
            return _hammingDistance;
        }

        public static long CalculateHammingDistance(NucleotideSequence a, NucleotideSequence b)
        {
            if (a.GetType() != b.GetType())
            {
                throw new ArgumentException(String.Format("Sequence types do not match. ({0} vs {1})", a.GetType(), b.GetType()));
            }

            if (a.Sequence.Length != b.Sequence.Length)
            {
                throw new ArgumentException("Sequences are of unequal length!");
            }

            return a.Sequence.Where((t, i) => t != b.Sequence[i]).Count();
        }

        protected string _rawComplement = String.Empty;
        NucleotideSequence INucleotideSequence.Complement
        {
            get
            {
                if (String.IsNullOrEmpty(_rawComplement))
                {
                    _rawComplement = GetComplementString();
                }

                if (GetType() == typeof (DnaSequence))
                {
                    return DnaSequence.FastDnaSequence(_rawComplement, ActiveAlphabet, GeneticCode, SymbolCounts);
                }
                else
                {
                    return RnaSequence.FastRnaSequence(_rawComplement, ActiveAlphabet, GeneticCode, SymbolCounts);
                }
            }
        }

        private string GetComplementString()
        {
            var newSequence = new StringBuilder(Sequence.Length);
            foreach (var nucleotide in Sequence)
            {
                var typedNucleotide = (Nucleotide)Char.ToUpperInvariant(nucleotide);
                var complement = ComplementTable[typedNucleotide];

                if (Char.IsLower(nucleotide))
                {
                    newSequence.Append(Char.ToLowerInvariant((char)complement));
                }
                else
                {
                    newSequence.Append((char)ComplementTable[typedNucleotide]);
                }
            }
            return newSequence.ToString();
        }

        NucleotideSequence INucleotideSequence.ReverseComplement
        {
            get
            {
                if (String.IsNullOrEmpty(_rawComplement))
                {
                    _rawComplement = GetComplementString();
                }

                var complementArray = _rawComplement.ToCharArray();
                Array.Reverse(complementArray);
                var reversed = new string(complementArray);

                if (GetType() == typeof(DnaSequence))
                {
                    return DnaSequence.FastDnaSequence(reversed, ActiveAlphabet, GeneticCode, SymbolCounts);
                }
                else
                {
                    return RnaSequence.FastRnaSequence(reversed, ActiveAlphabet, GeneticCode, SymbolCounts);
                }
            }
        }

        public virtual IDictionary<Nucleotide, Nucleotide> ComplementTable
        {
            get { return NucleotideAlphabet.ComplementTable; }
        }

        public virtual IDictionary<Nucleotide, Nucleotide> TranscriptionTable
        {
            get { return NucleotideAlphabet.TranscriptionTable; }
        }

        protected void InitializeBasePairs(string incomingBasePairs)
        {
            if (String.IsNullOrWhiteSpace(incomingBasePairs))
            {
                throw new ArgumentException("Empty nucleotide sequence!");
            }

            SymbolCounts = new Dictionary<char, long>(AllowedCodes.Count);
            foreach (var basePair in AllowedCodes)
            {
                SymbolCounts.Add(basePair, 0);
            }

            foreach (var basePair in incomingBasePairs)
            {
                var upper = Char.ToUpperInvariant(basePair);
                if (!AllowedCodes.Contains(basePair))
                {
                    throw new ArgumentException(basePair + " is not a recognized nucleotide");
                }
                SymbolCounts[upper]++;
            }
            Sequence = incomingBasePairs;
        }


        
        public abstract bool Equals(INucleotideSequence nucleotideSequence);

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

        private long _gcCount;
        public long GcCount
        {
            get
            {
                if (_gcCount == 0)
                {
                    foreach (var element in GcContentSymbols)
                    {
                        _gcCount += CodeCounts[element];
                    }
                }
                return _gcCount;
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
        public ISet<string> Tags { get; private set; }

        internal static INucleotideSequence SafeConstructor(INucleotideSequence safeSequence, AlphabetType alphabet, GeneticCode geneticCode)
        {
            //ToDo: implement some kind of safe factory method that skips all the sequence validation steps
            var type = safeSequence.GetType();

            if (type == typeof (DnaSequence))
            {
                DnaSequence.SafeConstructor()
            }

            switch (type)
            {
                case AlphabetType.AmbiguousDna:
            }
        }
        
    }
}

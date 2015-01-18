using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCompute
{
    public abstract class NucleotideSequence : INucleotideSequence
    {
        public const string InvalidNucleotideForAlphabetType = "{0} is not a valid nucleotide for a sequence with a {1} ActiveAlphabet";
        public const string InvalidAlphabetForSequenceType = "{0} is an invalid alphabet for a {1}";
        private const double _defaultDoubleValue = -1.00d;
        private const int _defaultIntValue = -1;

        public NucleotideAlphabet NucleotideAlphabet { get; protected set; }
        public AlphabetType ActiveAlphabet { get; protected set; }
        public GeneticCode GeneticCode { get; protected set; }
        public string Sequence { get; protected set; }

        protected NucleotideSequence(string sequence, AlphabetType alphabet, GeneticCode geneticCode)
        {
            NucleotideAlphabet = new NucleotideAlphabet(alphabet, geneticCode);
            ActiveAlphabet = alphabet;
            GeneticCode = geneticCode;
            VerifyAndInitializeNucleotides(sequence);
            Sequence = sequence;
        }

        protected void VerifyAndInitializeNucleotides(string rawNucleotides)
        {
            if (String.IsNullOrWhiteSpace(rawNucleotides))
            {
                throw new ArgumentException("Empty nucleotide sequence!");
            }

            SymbolCounts = new Dictionary<Nucleotide, long>(AllowedSymbols.Count);
            foreach (var basePair in AllowedSymbols)
            {
                SymbolCounts.Add(basePair, 0);
            }

            foreach (var nucleotide in rawNucleotides)
            {
                var typedNucleotide = (Nucleotide)Char.ToUpperInvariant(nucleotide);

                if (!AllowedSymbols.Contains(typedNucleotide))
                {
                    throw new ArgumentException(nucleotide + " is not a recognized nucleotide");
                }

                SymbolCounts[typedNucleotide]++;
            }
        }

        protected NucleotideSequence(string sequence, AlphabetType alphabet, GeneticCode geneticCode, Dictionary<Nucleotide, long> symbolCounts)
        {
            NucleotideAlphabet = new NucleotideAlphabet(alphabet, geneticCode);
            ActiveAlphabet = alphabet;
            GeneticCode = geneticCode;
            Sequence = sequence;
            SymbolCounts = symbolCounts;
        }

        public virtual ISet<Nucleotide> AllowedSymbols
        {
            get
            {
                return new HashSet<Nucleotide>(NucleotideAlphabet.AllowedSymbols);
            }
        }

        public virtual IDictionary<string, AminoAcid> TranslationTable
        {
            get { return NucleotideAlphabet.TranslationTable; }
        }

        public abstract NucleotideSequence Transcribe();

        public ProteinSequence Translate()
        {
            return new ProteinSequence(this);
        }

        //Test: 3 tests: nucleotide not part of the allowed symbols, nucleotide allowed, but not present, normal case
        public Dictionary<Nucleotide, long> SymbolCounts { get; private set; }
        public virtual long NucleotideCount(Nucleotide nucleotide)
        {
            if (!AllowedSymbols.Contains(nucleotide))
            {
                throw new ArgumentException(String.Format(InvalidNucleotideForAlphabetType, nucleotide, ActiveAlphabet));
            }

            return !SymbolCounts.ContainsKey(nucleotide) ? 0 : SymbolCounts[nucleotide];
        }    

        private long _gcCount = _defaultIntValue;
        public long GcCount
        {
            get
            {
                if (_gcCount == _defaultIntValue)
                {
                    foreach (var element in GcContentSymbols.Where(element => SymbolCounts.ContainsKey(element))) {
                        _gcCount += SymbolCounts[element];
                    }
                }
                return _gcCount;
            }
        }

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

        public virtual long CalculateHammingDistance(NucleotideSequence comparisonSequence)
        {
            return CalculateHammingDistance(this, comparisonSequence);
        }

        public static long CalculateHammingDistance(NucleotideSequence a, NucleotideSequence b)
        {
            if (a.Sequence.Length != b.Sequence.Length)
            {
                throw new ArgumentException("Sequences are of unequal length!");
            }

            if (a.ActiveAlphabet != b.ActiveAlphabet)
            {
                throw new ArgumentException("Sequences are not using the same nucleotide alphabet!");
            }

            if (a.GetType() != b.GetType())
            {
                throw new ArgumentException(String.Format("Sequence types do not match. ({0} vs {1})", a.GetType(), b.GetType()));
            }

            return a.Sequence.Where((t, i) => t != b.Sequence[i]).Count();
        }

        //Test: Adapt existing complement tests
        //Test: Create tests for symbol counts
        protected string _rawComplement = String.Empty;
        public NucleotideSequence Complement()
        {
            if (String.IsNullOrEmpty(_rawComplement))
            {
                _rawComplement = GetComplementString();
            }

            //Transform the symbol counts table T count becomes A count, etc.
            var _newSymbolCounts = new Dictionary<Nucleotide, long>(SymbolCounts.Count);
            foreach (var symbol in SymbolCounts)
            {
                if (_newSymbolCounts.ContainsKey(symbol.Key))
                {
                    continue;
                }
                var nucleotide = symbol.Key;
                var countToBeSwapped = SymbolCounts[nucleotide];
                var complement = ComplementTable[nucleotide];
                var newCount = SymbolCounts[complement];
                _newSymbolCounts.Add(nucleotide, newCount);
                _newSymbolCounts.Add(complement, countToBeSwapped);
            }

            if (GetType() == typeof (DnaSequence))
            {
                return DnaSequence.FastDnaSequence(_rawComplement, ActiveAlphabet, GeneticCode, _newSymbolCounts);
            }
            return RnaSequence.FastRnaSequence(_rawComplement, ActiveAlphabet, GeneticCode, _newSymbolCounts);
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
                    newSequence.Append((char)complement);
                }
            }
            return newSequence.ToString();
        }

        public NucleotideSequence ReverseComplement()
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

        public virtual IDictionary<Nucleotide, Nucleotide> ComplementTable
        {
            get { return NucleotideAlphabet.ComplementTable; }
        }

        public virtual IDictionary<Nucleotide, Nucleotide> TranscriptionTable
        {
            get { return NucleotideAlphabet.TranscriptionTable; }
        }

        //Test: Tests: identical sequences of different length, sequences of same type but different length, sequences of same length, but different nucleotide
        //Test: counts, identical sequences of different types, identical sequences but swap some letters around, same but then specify case significance
        public virtual bool Equals(NucleotideSequence sequence, bool matchCase = false)
        {
            if (Sequence.Length != sequence.Sequence.Length)
            {
                return false;
            }

            if (GetType() != sequence.GetType())
            {
                return false;
            }

            foreach (var nucleotidePair in SymbolCounts)
            {
                var nucleotide = nucleotidePair.Key;
                var thisValue = SymbolCounts[nucleotide];

                var thatValue = sequence.SymbolCounts[nucleotide];
                if (thisValue != thatValue)
                {
                    return false;
                }
            }

            return String.Equals(Sequence, sequence.Sequence, matchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
        }

        public ISet<string> Tags { get; private set; }
    }
}

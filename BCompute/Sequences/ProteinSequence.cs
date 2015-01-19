using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCompute
{
    public class ProteinSequence : ISequence, IProteinSequence
    {
        public GeneticCode GeneticCode { get; private set; }
        public AlphabetType ActiveAlphabet { get; private set; }
        public ISet<AminoAcid> AllowedSymbols { get; private set; }
        public IDictionary<string, AminoAcid> TranslationTable { get; private set; }
        public string Sequence { get; private set; }

        private const string _invalidAminoAcidCharacter = "{0} is not a valid amino acid character for the {1} alphabet";

        private readonly ProteinAlphabet _proteinAlphabet;
        private readonly Dictionary<AminoAcid, long> _aminoCounts;

        public ProteinSequence(string rawSequence, AlphabetType desiredProteinAlphabet, GeneticCode geneticCode = GeneticCode.Standard)
        {
            switch (desiredProteinAlphabet)
            {
                case AlphabetType.ExtendedProtein:
                    break;
                case AlphabetType.StandardProtein:
                    break;
                default:
                    throw new ArgumentException(String.Format(ProteinAlphabet.InvalidProteinAlphabet, desiredProteinAlphabet));
            }

            _proteinAlphabet = new ProteinAlphabet(desiredProteinAlphabet, geneticCode);
            ActiveAlphabet = desiredProteinAlphabet;

            _aminoCounts = new Dictionary<AminoAcid, long>(AllowedSymbols.Count);
            foreach (var symbol in AllowedSymbols)
            {
                _aminoCounts.Add(symbol, 0);
            }

            var trimmedRaw = rawSequence.Trim();

            foreach (var aminoCharacter in trimmedRaw)
            {
                var typedAmino = (AminoAcid) Char.ToUpperInvariant(aminoCharacter);
                if (!AllowedSymbols.Contains(typedAmino))
                {
                    throw new ArgumentException(String.Format(_invalidAminoAcidCharacter, aminoCharacter, _proteinAlphabet));
                }
                _aminoCounts[typedAmino]++;
            }
            Sequence = trimmedRaw;
        }

        public long AminoCount(AminoAcid aminoAcid)
        {
            if (!AllowedSymbols.Contains(aminoAcid))
            {
                throw new ArgumentException(String.Format(_invalidAminoAcidCharacter, aminoAcid, _proteinAlphabet));
            }
            return _aminoCounts[aminoAcid];
        }

        public bool Equals(ISequence aminoSequence, bool matchCase)
        {
            if (Sequence.Length != aminoSequence.Sequence.Length)
            {
                return false;
            }

            //Make sure incoming matches an amino type
            if (GetType() != typeof (ProteinSequence))
            {
                return false;
            }

            var typedSequence = (ProteinSequence) aminoSequence;

            foreach (var aminoPair in _aminoCounts)
            {
                if (_aminoCounts[aminoPair.Key] != typedSequence.AminoCount(aminoPair.Key))
                {
                    return false;
                }
            }

            return String.Equals(Sequence, aminoSequence.Sequence, matchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
        }

        public IEnumerable<int> FindMotif(string motif)
        {
            return Utilities.FindMotif(motif, Sequence);
        }

        private HashSet<string> _tags = new HashSet<string>(); 
        public ISet<string> Tags { get; private set; }

        public ProteinSequence Translate(DnaSequence dna, AlphabetType desiredProteinAlphabet)
        {
            return new ProteinSequence(dna);
        }

        public ProteinSequence Translate(RnaSequence rna, AlphabetType desiredProteinAlphabet)
        {
            return new ProteinSequence(rna);
        }

        internal ProteinSequence(NucleotideSequence seq)
        {
            AlphabetType alphabet;
            var allowedSymbols = new HashSet<AminoAcid>{AminoAcid.Stop, AminoAcid.Gap};

            switch (seq.ActiveAlphabet)
            {
                case AlphabetType.AmbiguousDna:
                    alphabet = AlphabetType.ExtendedProtein;
                    allowedSymbols.UnionWith(AlphabetDataProvider.ExtendedProtein);
                    break;
                case AlphabetType.AmbiguousRna:
                    alphabet = AlphabetType.ExtendedProtein;
                    allowedSymbols.UnionWith(AlphabetDataProvider.ExtendedProtein);
                    break;
                default:
                    alphabet = AlphabetType.StandardProtein;
                    allowedSymbols.UnionWith(AlphabetDataProvider.StandardProtein);
                    break;
            }
            var translationTable = new Dictionary<string, AminoAcid>(AlphabetDataProvider.GetTranslationTable(seq.GeneticCode, seq.ActiveAlphabet));

            //string safeSequence, AlphabetType alphabet, GeneticCode geneticCode, Dictionary<Nucleotide, long> symbolCounts
            ActiveAlphabet = seq.ActiveAlphabet;
            _proteinAlphabet = new ProteinAlphabet(alphabet, seq.GeneticCode);

            var proteinBlob = Translate(seq.Sequence, translationTable);
            Sequence = proteinBlob.Sequence;
            _aminoCounts = proteinBlob.AminoCounts;
        }
        
        //string rawSequence, Dictionary<string, Amino> theMap
        //returns string
        private ProteinBlob Translate(string rawSequence, IReadOnlyDictionary<string, AminoAcid> map)
        {
            var aminoString = new StringBuilder((rawSequence.Length / 2) + 1);  //Who knows how many ambiguities there will be?
            var uniqueAminos = new HashSet<AminoAcid>(map.Values);
            var aminoCounts = new Dictionary<AminoAcid, long>(uniqueAminos.Count + 2) {{AminoAcid.Gap, 0}};
            var allowAmbiguous = false;

            switch (ActiveAlphabet)
            {
                case AlphabetType.StrictRna:
                    break;
                case AlphabetType.StrictDna:
                    break;
                default:
                    allowAmbiguous = true;
                    aminoCounts.Add(AminoAcid.Unknown, 0);
                    break;
            }

            const string invalidSubsetForAlphabet = "{0} is an invalid sequence for unambiguous alphabets";

            foreach (var amino in uniqueAminos)
            {
                aminoCounts.Add(amino, 0);
            }
            
            var pointer = 0;
            var appendedUnknown = false;
            var appendedGap = false;
            while (pointer < rawSequence.Length)
            {
                var subset = rawSequence.Skip(pointer).Take(3).ToArray();
                var stringifiedSubset = new string(subset);

                if (map.ContainsKey(stringifiedSubset))
                {
                    aminoString.Append((char)map[stringifiedSubset]);
                    aminoCounts[map[stringifiedSubset]]++;
                    pointer += 3;
                    appendedUnknown = false;
                    appendedGap = false;
                    continue;
                }

                if (!allowAmbiguous)
                {
                    //Any unrecognized triplet in a sequence with a strict alphabet is an error
                    throw new ArgumentException(String.Format(invalidSubsetForAlphabet, new string(subset)));
                }

                //Unknown triplet:
                if (!appendedGap && subset[0] == (char) AminoAcid.Gap)
                {
                    aminoString.Append((char)AminoAcid.Gap);
                    aminoCounts[AminoAcid.Gap]++;
                    appendedGap = true;
                    appendedUnknown = false;
                    pointer++;
                    continue;
                }

                //ToDo: Fix aminoCounts so if the alphabet is ambiguous, we can use unknowns
                if (!appendedUnknown && subset[0] != (char)AminoAcid.Gap)
                {
                    aminoString.Append((char) AminoAcid.Unknown);
                    aminoCounts[AminoAcid.Unknown]++;
                    appendedUnknown = true;
                    appendedGap = false;
                }
                pointer++;
            }
            return new ProteinBlob(aminoString.ToString(), aminoCounts);
        }
    }
}

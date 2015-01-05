using System;
using System.Collections.Generic;

namespace BCompute
{
    public class AminoSequence : ISequence
    {
        public string Sequence { get; private set; }

        public ISet<string> Tags { get; private set; }

        private Dictionary<char, long> _codeCounts;
        public IDictionary<char, long> CodeCounts { get { return _codeCounts; } }

        internal AminoSequence(string sequence, bool isSafe)
        {
            if (!isSafe)
            {
                InitializeSequenceFromUncheckedString(sequence);
            }
            else
            {
                InitializeSequenceFromRna(sequence);
            }
        }

        private void InitializeSequenceFromUncheckedString(string rnaSequence)
        {
            //Reduce the RNA rnaSequence to amino acids
            _codeCounts = new Dictionary<char, long>(AllowedCodes.Count);
            foreach (var rawAmino in rnaSequence)
            {
                var normalizedAmino = Char.ToUpperInvariant(rawAmino);
                if (!AllowedCodes.Contains(normalizedAmino))
                {
                    throw new ArgumentException(String.Format("{0} is not a recognized rnaSequence character.", rawAmino));
                }
                _codeCounts[normalizedAmino]++;
            }
        }

        private void InitializeSequenceFromRna(RnaSequence rna)
        {
            //Reduce the RNA nucleotides to amino acids
            var aminoAcids = ConvertRnaSequenceToAminoAcidSequence(rna.Sequence);
            

            _codeCounts = new Dictionary<char, long>(AllowedCodes.Count);
            foreach (var rawAmino in rna)
            {
                _codeCounts[Char.ToUpperInvariant(rawAmino)]++;
            }
        }

        public static AminoSequence FromDna(DnaSequence dna)
        {
            var rnaSequence = DnaSequence.ConvertToRna(dna);
            return new AminoSequence(rnaSequence.Sequence, true);
        }

        public static AminoSequence FromRna(RnaSequence rna)
        {
            return new AminoSequence(rna.Sequence, true);
        }

        public AminoSequence FromRawString(string uncheckedString)
        {
            var subset = uncheckedString.Substring(0, 100).ToUpperInvariant();
            var nucleotideCount = 0;
            var aminoCount = 0;
            foreach (var letter in subset)
            {
                if (AllowedCodes.Contains(letter))
                {
                    aminoCount++;
                }
                else if(DnaSequence.GetAllowedNucleotides.Contains(letter) || RnaSequence.GetAllowedNucleotides.Contains(letter))
                {
                    nucleotideCount++;
                }
            }

            if (aminoCount > nucleotideCount)
            {
                return new AminoSequence(uncheckedString, false);
            }
            throw new ArgumentException(String.Format("The rnaSequence you provided looks more like a nucleotide rnaSequence than an amino acid rnaSequence{0}{1}", Environment.NewLine, subset));
        }

        private string ConvertRnaSequenceToAminoAcidSequence(string nucleotides)
        {
            var list = new List<char>(nucleotides.Length / 2);
            _codeCounts = new Dictionary<char, long>(Maps.CodonToAmino.Count + 2);

            var index = 0;
            while (index < nucleotides.Length)
            {
                var normalizedBase = Char.ToUpperInvariant(nucleotides[index]);
                
                //if it's a strict RNA or DNA nucleotide, grab the next two letters
                //If they make up a pair, 
                
                
                
                switch (Char.ToUpperInvariant(normalizedBase))
                {
                    case ((char) Nucleotide) :
                        index++;
                        break;
                    case Maps.SkipNucleotides:
                        index++;
                        break;
                    default:
                        var maybeCodon = String.Format("{0}{1}{2}", normalizedBase, nucleotides[++index], nucleotides[++index]).ToUpperInvariant();
                        if (!Maps.CodonToAmino.ContainsKey(maybeCodon))
                        {
                            throw new ArgumentException(String.Format("{0} is not a valid codon", maybeCodon));
                        }
                        list.AddRange(maybeCodon.ToCharArray());
                        break;
                }
                list.Add(normalizedBase);
                _codeCounts[normalizedBase]++;
            }
            return String.Join(String.Empty, list.ToArray());
        }

        private static HashSet<char> _allowedCodes; 
        public ISet<char> AllowedCodes
        {
            get
            {
                if (_allowedCodes == null || _allowedCodes.Count == 0)
                {
                    //ToDo: Implement the AminoAcid enum, and populate this set from it
                    _allowedCodes = new HashSet<char>();
                }
                return _allowedCodes;
            }
        }

        public int GapCount { get; private set; }
        public int AnyBaseCount { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCompute
{
    public class AminoSequence
    {
        public string AminoAcids { get; protected set; }
        public AminoSequence(string rawAminos)
        {
            foreach (var amino in rawAminos.Where(amino => !AllowedSequenceCharacters.Contains(Char.ToUpperInvariant(amino)))) {
                throw new ArgumentException("Unrecognized amino acid: " + amino);
            }
            //Translate amino sequence from codons to amino acids...

            //var aminos = 
        }

        private static ImmutableHashSet<char> _allowedSequenceCharacters;
        public static ImmutableHashSet<char> AllowedSequenceCharacters
        {
            get
            {
                

                if (_allowedSequenceCharacters == null || _allowedSequenceCharacters.Count == 0)
                {
                    var tempTable = new HashSet<char>(Maps.CodonToAmino.Values) {Maps.SkipNucleotides};
                    _allowedSequenceCharacters = tempTable.ToImmutableHashSet();
                }
                return _allowedSequenceCharacters;
            }
        }

        public static AminoSequence ConvertRnaToAminoSequence(RnaSequence rna)
        {
            var aminoSequence = new AminoSequence(rna.BasePairs);

        }

        private static string ConvertNucleotidesToAminos(string nucleotides)
        {
            var list = new List<char>(nucleotides.Length);

            var index = 0;
            while (index < nucleotides.Length)
            {
                var nucleotide = nucleotides[index];
                switch (nucleotide)
                {
                    case '*':
                        list.Add(nucleotide);
                        index++;
                        break;
                    case '-':
                        list.Add('-');
                        index++;
                        break;
                    default:
                        var thisSubset = String.Format("{0}{1}{2}", nucleotide, nucleotides[++index], nucleotides[++index]);
                        if (!Maps.CodonToAmino.ContainsKey(thisSubset))
                        {
                            throw new ArgumentException(String.Format("{0} is not a valid codon", thisSubset));
                        }
                        list.AddRange(thisSubset.ToCharArray());
                        break;
                }
            }
            return String.Join(String.Empty, list.ToArray());
        }
    }
}

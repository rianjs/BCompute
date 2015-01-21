using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCompute
{
    public class ConsensusBuilder
    {
        private readonly List<ISequence> _sequences;
        private readonly int _sequenceLength;
        private readonly AlphabetType _alphabet;
        public ConsensusBuilder(IEnumerable<ISequence> sequences)
        {
            _sequences = sequences.ToList();

            var firstElement = _sequences.First();
            _sequenceLength = firstElement.Sequence.Length;
            _alphabet = firstElement.ActiveAlphabet;
            foreach (var sequence in _sequences)
            {
                if (sequence.Sequence.Length != _sequenceLength)
                {
                    throw new ArgumentException("Sequences were not all of uniform length");
                }

                if (_alphabet != sequence.ActiveAlphabet)
                {
                    throw new ArgumentException("Sequences must all be of the same type");
                }
            }

            char[] alphabetChars;

            switch (_alphabet)
            {
                case AlphabetType.StrictRna:
                case AlphabetType.AmbiguousRna:
                case AlphabetType.StrictDna:
                case AlphabetType.AmbiguousDna:
                    {
                        var symbols = AlphabetDataProvider.GetAllowedNucleotideSymbols(_alphabet).ToArray();
                        alphabetChars = new char[symbols.Length];
                
                        for (var i = 0; i < symbols.Length; i++)
                        {
                            alphabetChars[i] = (char) symbols[i];
                        }
                    }
                    break;
                case AlphabetType.ExtendedProtein:
                case AlphabetType.StandardProtein:
                    {
                        var symbols = AlphabetDataProvider.GetAllowedProteinSymbols(_alphabet).ToArray();
                        alphabetChars = new char[symbols.Length];
                
                        for (var i = 0; i < symbols.Length; i++)
                        {
                            alphabetChars[i] = (char) symbols[i];
                        }
                    }
                    break;
                default:
                    throw new ArgumentException("Unknown alphabet type");
            }

            _consensusMatrix = new Dictionary<char, int[]>(alphabetChars.Length);

            foreach (var character in alphabetChars)
            {
                _consensusMatrix.Add(character, new int[_sequenceLength]);
            }
        }

        private readonly Dictionary<char, int[]> _consensusMatrix;
        private bool _isComputed = false;
        private void ComputeConsensusMatrix()
        {
            foreach (var sequence in _sequences)
            {
                for (var i = 0; i < _sequenceLength; i++)
                {
                    var character = Char.ToUpperInvariant(sequence.Sequence[i]);
                    _consensusMatrix[character][i]++;
                }
            }

            _isComputed = true;
        }

        private string _consensusString;
        public string GetConsensusString()
        {
            if (!_isComputed)
            {
                ComputeConsensusMatrix();
                _consensusString = String.Empty;
            }

            if (String.IsNullOrEmpty(_consensusString))
            {
                var placeholder = new StringBuilder(_sequenceLength);


                for (var column = 0; column < _sequenceLength; column++)
                {
                    //Which letter is the highest number associated with?
                    var mostPrevalent = -1;
                    var toAppend = '\0';
                    foreach (var pair in _consensusMatrix)
                    {
                        var count = pair.Value[column];
                        if (count > mostPrevalent)
                        {
                            mostPrevalent = count;
                            toAppend = pair.Key;
                        }
                    }
                    placeholder.Append(toAppend);
                }
                _consensusString = placeholder.ToString();
            }

            return _consensusString;
        }

        public IDictionary<char, int[]> GetConsensusMatrix()
        {
            if (!_isComputed)
            {
                ComputeConsensusMatrix();
            }
            return _consensusMatrix;
        }

        public override string ToString()
        {
            if (!_isComputed)
            {
                ComputeConsensusMatrix();
            }

            var charCount = (_consensusMatrix.Count * _sequenceLength * 4)  //Places for the counts (4 digits each)
                            + _sequenceLength                               //Spaces after each count
                            + (_consensusMatrix.Count * 3)                  //Keys for the matrix value + trailing ": "
                            + (_consensusMatrix.Count * 2)                  //newline chars after each row (\r\n)
                            + (_consensusMatrix.Count * _sequenceLength);   //Just in case

            var intermediate = new StringBuilder(charCount);

            foreach (var entry in _consensusMatrix)
            {
                intermediate.Append(entry.Key + ": ");
                foreach (var count in entry.Value)
                {
                    intermediate.Append(count + " ");
                }
                intermediate.Append(Environment.NewLine);
            }
            return intermediate.ToString().Trim();
        }
    }
}

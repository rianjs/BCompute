using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace BCompute
{
    public class FastaParser
    {
        public const string FastaSequenceBegin = ">";
        private readonly AlphabetType _alphabet;
        private readonly GeneticCode _geneticCode;
        private readonly Uri _dataPath;
        private List<string> _lines = new List<string>();

        private FastaParser(AlphabetType alphabet, GeneticCode geneticCode)
        {
            _geneticCode = geneticCode;
            _alphabet = alphabet;
        }

        public FastaParser(Uri pathToFasta, AlphabetType alphabet, GeneticCode geneticCode = GeneticCode.Standard) :
            this(alphabet, geneticCode)
        {
            _dataPath = pathToFasta;
        }

        public FastaParser(IEnumerable<string> fastaText, AlphabetType alphabet, GeneticCode geneticCode = GeneticCode.Standard) :
            this (alphabet, geneticCode)
        {
            _lines.AddRange(fastaText);
        }

        public FastaParser(string fastaTextBlob, AlphabetType alphabet, GeneticCode geneticCode = GeneticCode.Standard) :
            this(alphabet, geneticCode)
        {
            _lines = Regex.Split(fastaTextBlob, Constants.CrossPlatformNewlines).ToList();
        }

        public IEnumerable<ISequence> Parse()
        {
            if (_lines.Count == 0)
            {
                _lines = RemoveBlankLines(GetText()).ToList();
            }

            var lineIndex = 0;
            while (lineIndex < _lines.Count)
            {
                
                if (!_lines[lineIndex].Trim().StartsWith(FastaSequenceBegin))
                {
                    continue;
                }

                var toTake = 1; //.Take() 1-based
                var sequenceStart = lineIndex;
                lineIndex++;
                while (lineIndex < _lines.Count && !_lines[lineIndex].Trim().StartsWith(FastaSequenceBegin))
                {
                    lineIndex++;
                    toTake++;
                }
                var taken = _lines.Skip(sequenceStart).Take(toTake);
                yield return GenerateFastaSequence(taken);
            }
        }

        public ISequence GenerateFastaSequence(IEnumerable<string> fastaBlock)
        {
            var metadataLine = fastaBlock.First().Remove(0, 1); //Snip off the ">"
            var tags = new HashSet<string>(ExtractMetadataFromLine(metadataLine));

            var sequenceData = fastaBlock.Skip(1).Take(fastaBlock.Count() - 1);
            var sequence = String.Join(String.Empty, sequenceData);

            switch (_alphabet)
            {
                case AlphabetType.StrictDna:
                    return new DnaSequence(sequence, _alphabet, _geneticCode, tags);
                case AlphabetType.AmbiguousDna:
                    return new DnaSequence(sequence, _alphabet, _geneticCode, tags);
                case AlphabetType.StrictRna:
                    return new RnaSequence(sequence, _alphabet, _geneticCode, tags);
                case AlphabetType.AmbiguousRna:
                    return new RnaSequence(sequence, _alphabet, _geneticCode, tags);
                case AlphabetType.StandardProtein:
                    return new ProteinSequence(sequence, _alphabet, _geneticCode, tags);
                case AlphabetType.ExtendedProtein:
                    return new ProteinSequence(sequence, _alphabet, _geneticCode, tags);
                default:
                    throw new ArgumentException("Invalid FastA sequence");
            }
        }

        private static IEnumerable<string> ExtractMetadataFromLine(string line)
        {
            var rawMetadata = line.Split('|');
            var cleanedMetadata = new List<string>(rawMetadata.Length);
            cleanedMetadata.AddRange(rawMetadata.Select(element => element.Trim()).Where(trimmed => !String.IsNullOrEmpty(trimmed)));

            return cleanedMetadata;
        } 

        private IEnumerable<string> GetText()
        {
            if (_dataPath.IsFile)
            {
                return File.ReadAllLines(_dataPath.AbsolutePath);
            }

            string text;
            using (var webClient = new WebClient())
            {
                text = webClient.DownloadString(_dataPath);
            }
            return text.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
        }

        private IEnumerable<string> RemoveBlankLines(IEnumerable<string> text)
        {
            return text.Where(line => !String.IsNullOrWhiteSpace(line));
        }
    }
}

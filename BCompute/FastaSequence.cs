using System;
using System.Collections.Generic;

namespace BCompute
{
    public class FastaSequence
    {
        public readonly string Label;
        public readonly NucleotideSequence NucleotideSequence;
        public IEnumerable<string> Metadata { get; private set; }

        public FastaSequence(string label, NucleotideSequence nucleotideSequence, IEnumerable<string> metadata)
        {
            Label = label;
            NucleotideSequence = nucleotideSequence;
        }

        /// <summary>
        /// </summary>
        /// <param name="fastaBlock">A single, complete FASTA sequence chunk with metadata and sequence information</param>
        /// <returns></returns>
        //public static FastaSequence GenerateFastaSequence(IEnumerable<string> fastaBlock)
        //{
        //    var enumerable = fastaBlock as IList<string> ?? fastaBlock.ToList();

        //    var metadataLine = ExtractMetadataFromLine(enumerable.First());
        //    var line = metadataLine as IList<string> ?? metadataLine.ToList();
        //    var fastaLabel = line.First().Remove(0, 1); //Snips off the ">" character
        //    var remainingMetadata = line.Skip(1).Take(line.Count() - 1);
        //    var sequenceLines = enumerable.Skip(1).Take(enumerable.Count() - 1).ToArray();
        //    var sequence = String.Join(String.Empty, sequenceLines);
        //    AlphabetType alphabet = DetermineAlphabetInUse(sequence);

        //    return new FastaSequence(fastaLabel, new );
        //}

        //public static FastaSequence GenerateFastaSequence(string fastaText)
        //{
        //    var lines = fastaText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        //    return GenerateFastaSequence(lines);
        //}

        //public static IEnumerable<FastaSequence> GenerateFastaSequences(IEnumerable<string> fastaLines)
        //{
        //    var lines = fastaLines.ToList();
        //    var sequences = ExtractDiscreteFastaBlocksFromBlob(lines);
        //    return sequences;
        //}

        //public static IEnumerable<FastaSequence> GenerateFastaSequences(string fastaText)
        //{
        //    var lines = fastaText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        //    return GenerateFastaSequences(lines);
        //}

        //private static IEnumerable<FastaSequence> ExtractDiscreteFastaBlocksFromBlob(IList<string> fastaTextBlob)
        //{
        //    var index = 0;
        //    while (index < fastaTextBlob.Count)
        //    {
        //        var toTake = 0;
        //        if (fastaTextBlob[index].Trim().StartsWith(">"))
        //        {
        //            while (!fastaTextBlob[index].Trim().StartsWith(">") && index < fastaTextBlob.Count)
        //            {
        //                index++;
        //                toTake++;
        //            }
        //            var taken = fastaTextBlob.Skip(index).Take(toTake);
        //            yield return GenerateFastaSequence(taken);
        //        }
        //    }
        //}

        //private static IEnumerable<string> ExtractMetadataFromLine(string line)
        //{
        //    var rawMetadata = line.Split('|');
        //    var cleanedMetadata = new List<string>(rawMetadata.Length);
        //    cleanedMetadata.AddRange(rawMetadata.Select(element => element.Trim()));
        //    return cleanedMetadata;
        //} 

        /// <summary>
        /// Value-oriented equality semantics: are the sequences of the same type? If so, are the nucleotides in the sequence identical?
        /// </summary>
        /// <param name="fastaSequence"></param>
        /// <returns></returns>
        public override bool Equals(object fastaSequence)
        {
            try
            {
                if (GetType() != fastaSequence.GetType())
                {
                    return false;
                }

                var typedSequence = (FastaSequence) fastaSequence;
                if (NucleotideSequence.GetType() != typedSequence.NucleotideSequence.GetType())
                {
                    return false;
                }

                if (!NucleotideSequence.Equals(typedSequence.NucleotideSequence))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}

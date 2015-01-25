using System;
using System.Collections.Generic;
using System.Linq;

namespace BCompute
{
    public class SequenceUtilities
    {
        /// <summary>
        /// Returns the indices in the haystack where the needle may be found
        /// </summary>
        /// <param name="needle"></param>
        /// <param name="haystack"></param>
        /// <returns></returns>
        public static IEnumerable<int> FindMotif(string needle, string haystack)
        {
            if (String.IsNullOrWhiteSpace(needle))
            {
                throw new ArgumentException(String.Format("Motif is null or whitespace"));
            }

            if (String.IsNullOrWhiteSpace(haystack))
            {
                throw new ArgumentException(String.Format("The sequence you're searching was null or whitespace"));
            }

            var index = 0;
            do
            {
                var instance = haystack.IndexOf(needle, index, StringComparison.OrdinalIgnoreCase);
                index = instance + 1;
                if (instance != Constants.MotifNotFound)
                {
                    yield return instance;
                }
                else
                {
                    yield break;
                }
            } while (index < haystack.Length);
        }

        /// <summary>
        /// Returns a collection of overlapping sequences in the form of sequence => overlappingSeq1, overlappingSeq2, ..., overlappingSeqN
        /// </summary>
        /// <param name="sequences"></param>
        /// <returns></returns>
        public static IDictionary<ISequence, IEnumerable<ISequence>> FindOverlappingSequences(IEnumerable<ISequence> sequences, int overlap)
        {
            if (overlap < 2)
            {
                throw new ArgumentException("Overlap must be 2 or more characters");
            }

            var haystack = sequences.ToList();
            var overlappingElements = new Dictionary<ISequence, IEnumerable<ISequence>>(haystack.Count);
            foreach (var straw in haystack)
            {
                var needle = new string(straw.Sequence.Skip(straw.Sequence.Length - overlap).Take(overlap).ToArray());
                var list = new List<ISequence>(haystack.Count - 1);
                list.AddRange(haystack.Where(element => element != straw && element.Sequence.StartsWith(needle)));
                if (list.Any())
                {
                    overlappingElements.Add(straw, list);
                }
            }
            return overlappingElements;
        } 
    }
}

using System;
using System.Collections.Generic;

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
    }
}

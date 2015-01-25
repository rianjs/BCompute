using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using BCompute;

namespace Tester
{
    public class Tester
    {
        static void Main()
        {
            const string parent = @"C:\Users\rianjs\Desktop";
            const string filename = "overlap-graphs3.txt";
            var path = Path.Combine(parent, filename);

            var sequences = new FastaParser(new Uri(path), AlphabetType.StrictDna).Parse();
            const int overlapCount = 3;

            var overlapping = SequenceUtilities.FindOverlappingSequences(sequences, overlapCount);

            var theString = String.Empty;
            foreach (var overlap in overlapping)
            {
                var activeTag = overlap.Key.Tags.First();
                theString = overlap.Value.Aggregate(theString, (current, sequence) => current + String.Format("{0} {1}{2}", activeTag, sequence.Tags.First(), Environment.NewLine));
            }

            Console.WriteLine(theString);
            File.WriteAllText(Path.Combine(parent, "answer.txt"), theString);
            
            Console.ReadLine();
        }
    }
}

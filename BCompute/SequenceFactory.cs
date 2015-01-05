using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCompute
{
    public class SequenceFactory
    {
        /// <summary>
        /// Returns a DnaSequence object or an RnaSequence object from the string of nucleotides
        /// </summary>
        /// <param name="nucleotides"></param>
        /// <returns></returns>
        public static NucleotideSequence GetNucleotideSequence(string nucleotides)
        {
            return NucleotideSequence.GenerateNucleotideSequence(nucleotides);
        }

        public static AminoSequence GetAminoAcidSequence(string untypedSequence)
        {
            var nucleotideSequence = NucleotideSequence.GenerateNucleotideSequence(untypedSequence);

            //If it's RNA, turn it into an amino sequence
            if (nucleotideSequence.GetType() == typeof (DnaSequence))
            {
                //Convert the DNA into RNA then an amino sequence
            }

            if (nucleotideSequence.GetType() == typeof (RnaSequence))
            {
                //Convert the RNA sequence into an amino sequence
            }

            //Inspect the string to see if it's DNA, RNA, or a sequence of amino acids
            foreach (var letter in untypedSequence)
            {
                var upperCase = Char.ToUpperInvariant(letter);
                //if the letter is A, G, C, we know it's DNA or RNA
                //if the letter is U, we know it's RNA
                //if the letter is in 
                if (DnaSequence.C)
            }
        }

        /// <summary>
        /// Returns a sequence object at the highest level of abstraction: if the sequence is DNA, it will return a
        /// </summary>
        /// <param name="untypedSequence"></param>
        /// <returns></returns>
        public static ISequence GetTopLevelSequence(string untypedSequence)
        {
            
        }
    }
}

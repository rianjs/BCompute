using System.Collections.Generic;
using BCompute.Data.GeneticCode;

namespace BCompute
{
    internal interface INucleotideSequence
    {
        /// <summary>
        /// The set of symbols that are allowed in the sequence's alphabet
        /// </summary>
        string AllowedSymbols { get; }

        /// <summary>
        /// The NCBI genetic code triplet mappings, which governs which transcription table is used for transcribing and back transcribing RNA and DNA
        /// </summary>        
        GeneticCode GeneticCode { get; }

        /// <summary>
        /// Mapping of codon triplets to proteins. (Works for DNA, too.)
        /// </summary>
        IDictionary<Nucleotide, Nucleotide> TranslationTable { get; }

        /// <summary>
        /// Mapping of nucleotide complements
        /// </summary>
        IDictionary<Nucleotide, Nucleotide> ComplementTable { get; }

        /// <summary>
        /// Mapping of DNA to RNA and RNA to DNA transciptions
        /// </summary>
        IDictionary<Nucleotide, Nucleotide> TranscriptionTable { get; }

        /// <summary>
        /// Transcribes or back transcribes from a DnaSequence to an RnaSequence, and vice versa
        /// </summary>
        /// <returns></returns>
        INucleotideSequence Transcribe();

        /// <summary>
        /// Returns the number of times the specified nucleotide appears in the sequence data
        /// </summary>
        /// <param name="nucleotide"></param>
        /// <returns></returns>
        long NucleotideCount(Nucleotide nucleotide);

        /// <summary>
        /// Returns DNA or RNA base pair string
        /// </summary>
        string RawSequence { get; }

        /// <summary>
        /// Returns the GC content of the sequence to 6 decimal places
        /// </summary>
        double GcContent { get; }

        /// <summary>
        /// Returns a set of the alphabet symbols that were used to compute the GC content
        /// </summary>
        ISet<Nucleotide> GcContentNucleotides { get; } 

        /// <summary>
        /// Provides the complementary DnaSequence or RnaSequence object
        /// </summary>
        INucleotideSequence Complement { get; }
        
        /// <summary>
        /// Provides the reversed complementary DnaSequence or RnaSequence object
        /// </summary>
        INucleotideSequence ReverseComplement { get; }

        /// <summary>
        /// Returns the Hamming Distance between this nucleotide sequence, and another nucleotide sequence of the same type
        /// </summary>
        /// <param name="nucleotideSequence"></param>
        /// <returns></returns>
        long CalculateHammingDistance(INucleotideSequence nucleotideSequence);

        /// <summary>
        /// Returns true if this nucleotide sequence value is the same as a comparison nucleotide sequence value.
        /// </summary>
        /// <param name="nucleotideSequence"></param>
        /// <returns></returns>
        bool Equals(INucleotideSequence nucleotideSequence);
    }
}

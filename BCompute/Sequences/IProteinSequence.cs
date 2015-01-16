using System.Collections.Generic;

namespace BCompute
{
    internal interface IProteinSequence
    {
        /// <summary>
        /// Provides the set of allowed symbols
        /// </summary>
        ISet<AminoAcid> AllowedSymbols { get; }

        /// <summary>
        /// The NCBI genetic code triplet mappings, which governs which transcription table is used for transcribing and back transcribing RNA and DNA
        /// </summary>
        GeneticCode GeneticCode { get; }

        /// <summary>
        /// Mapping of proteins to all possible triplets as specified by the GeneticCode
        /// </summary>
        IDictionary<AminoAcid, IEnumerable<string>> ReverseTranscriptionTable { get; }

        /// <summary>
        /// Returns a protein sequence from the specified 
        /// </summary>
        /// <param name="dna"></param>
        /// <returns></returns>
        ProteinSequence FromDna(DnaSequence dna);


        ProteinSequence FromRna(RnaSequence rna);

        /// <summary>
        /// If this protein sequence is ambiguous, the DnaSequence will contain the possible triplets associated with this sequence's GeneticCode, otherwise it
        /// will return an unambiguous DnaSequence
        /// </summary>
        /// <returns></returns>
        DnaSequence ToDna();

        /// <summary>
        /// If this protein sequence is ambiguous, the RnaSequence will contain the possible triplets associated with this sequence's GeneticCode, otherwise it
        /// will return an unambiguous RnaSequence
        /// </summary>
        /// <returns></returns>
        RnaSequence ToRna();
    }
}

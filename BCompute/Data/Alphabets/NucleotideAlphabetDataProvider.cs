using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BCompute.Data.GeneticCode;

namespace BCompute.Data.Alphabets
{
    internal static class NucleotideAlphabetDataProvider
    {
        public const string InvalidNucleotideAlphabet = "{0} is not a nucleotide alphabet";
        private const string _invalidGeneticCode = "{0} is not a valid genetic code";

        #region Nucleotide sets
        private static readonly Nucleotide[] _allNucleotides = (Nucleotide[]) Enum.GetValues(typeof(Nucleotide));
        public static ISet<Nucleotide> AllNucleotides
        {
            get
            {
                return new HashSet<Nucleotide>(_allNucleotides);
            }
        }

        private static readonly Nucleotide[] _strictDnaEnum = (Nucleotide[]) Enum.GetValues(typeof (StrictDna));
        public static ISet<Nucleotide> StrictDna
        {
            get
            {
                return new HashSet<Nucleotide>(_strictDnaEnum);
            }
        }

        public static ISet<Nucleotide> NotInAnyDnaAlphabet = new HashSet<Nucleotide> { Nucleotide.Uracil };

        public static ISet<Nucleotide> AmbiguousDna
        {
            get
            {
                return new HashSet<Nucleotide>(AllNucleotides.Except(NotInAnyDnaAlphabet));
            }
        }

        private static readonly Nucleotide[] _strictRnaEnum = (Nucleotide[]) Enum.GetValues(typeof (StrictRna));
        public static ISet<Nucleotide> StrictRna
        {
            get
            {
                return new HashSet<Nucleotide>(_strictRnaEnum);
            }
        }

        public static ISet<Nucleotide> NotInAnyRnaAlphabet = new HashSet<Nucleotide> { Nucleotide.Thymine };

        private static readonly IEnumerable<Nucleotide> _notInRnaAlphabet = AllNucleotides.Except(NotInAnyRnaAlphabet);
        public static ISet<Nucleotide> AmbiguousRna
        {
            get
            {
                return new HashSet<Nucleotide>(_notInRnaAlphabet);
            }
        }
        #endregion

        /// <summary>
        /// Returns the set of allowed symbols for a given nucleotide alphabet
        /// </summary>
        /// <param name="alphabet"></param>
        /// <returns></returns>
        public static ISet<Nucleotide> GetAllowedSymbols(AlphabetType alphabet)
        {
            switch (alphabet)
            {
                case AlphabetType.StrictDna:
                    return StrictDna;
                case AlphabetType.AmbiguousDna:
                    return AmbiguousDna;
                case AlphabetType.StrictRna:
                    return StrictRna;
                case AlphabetType.AmbiguousRna:
                    return AmbiguousRna;
                default:
                    throw new ArgumentException(String.Format(InvalidNucleotideAlphabet, alphabet));
            }
        }

        /// <summary>
        /// Complement table for the ambiguous DNA alphabet
        /// </summary>
        public static IDictionary<Nucleotide, Nucleotide> AmbiguousDnaComplements
        {
            get
            {
                return new Dictionary<Nucleotide, Nucleotide>
                {
                    {Nucleotide.Adenine, Nucleotide.Thymine}, //A <=> T
                    {Nucleotide.Thymine, Nucleotide.Adenine},
                    {Nucleotide.NotAdenine, Nucleotide.NotThymine}, //B <=> V
                    {Nucleotide.NotThymine, Nucleotide.NotAdenine},
                    {Nucleotide.Cytosine, Nucleotide.Guanine}, //C <=> G
                    {Nucleotide.Guanine, Nucleotide.Cytosine},
                    {Nucleotide.NotGuanine, Nucleotide.NotCytosine}, //D <=> H
                    {Nucleotide.NotCytosine, Nucleotide.NotGuanine},
                    {Nucleotide.Keto, Nucleotide.Amino}, //K <=> M
                    {Nucleotide.Amino, Nucleotide.Keto},
                    {Nucleotide.Strong, Nucleotide.Strong}, //S <=> S
                    {Nucleotide.Weak, Nucleotide.Weak}, //W <=> W
                    {Nucleotide.Unknown, Nucleotide.Unknown}, //N <=> N
                    {Nucleotide.Gap, Nucleotide.Gap}, //- <=> -
                };
            }
        }

        /// <summary>
        /// Complement table for the strict DNA alphabet
        /// </summary>
        public static Dictionary<Nucleotide, Nucleotide> StrictDnaComplements
        {
            get
            {
                return new Dictionary<Nucleotide, Nucleotide>
                {
                    {Nucleotide.Adenine, Nucleotide.Thymine},
                    {Nucleotide.Guanine, Nucleotide.Cytosine},
                    {Nucleotide.Thymine, Nucleotide.Adenine},
                    {Nucleotide.Cytosine, Nucleotide.Guanine}
                };
            }
        }

        /// <summary>
        /// Complement table for the ambiguous RNA alphabet
        /// </summary>
        public static Dictionary<Nucleotide, Nucleotide> AmbiguousRnaComplements
        {
            get
            {
                return new Dictionary<Nucleotide, Nucleotide>
                {
                    {Nucleotide.Adenine, Nucleotide.Uracil}, //A <=> U
                    {Nucleotide.Uracil, Nucleotide.Adenine},
                    {Nucleotide.NotAdenine, Nucleotide.NotThymine}, //B <=> V
                    {Nucleotide.NotThymine, Nucleotide.NotAdenine},
                    {Nucleotide.Cytosine, Nucleotide.Guanine}, //C <=> G
                    {Nucleotide.Guanine, Nucleotide.Cytosine},
                    {Nucleotide.NotGuanine, Nucleotide.NotCytosine}, //D <=> H
                    {Nucleotide.NotCytosine, Nucleotide.NotGuanine},
                    {Nucleotide.Keto, Nucleotide.Amino}, //K <=> M
                    {Nucleotide.Amino, Nucleotide.Keto},
                    {Nucleotide.Strong, Nucleotide.Strong}, //S <=> S
                    {Nucleotide.Weak, Nucleotide.Weak}, //W <=> W
                    {Nucleotide.Unknown, Nucleotide.Unknown}, //N <=> N
                    {Nucleotide.Gap, Nucleotide.Gap}, //- <=> -
                };
            }
        }

        /// <summary>
        /// Complement table for the strict RNA alphabet
        /// </summary>
        public static Dictionary<Nucleotide, Nucleotide> StrictRnaComplements
        {
            get
            {
                return new Dictionary<Nucleotide, Nucleotide>
                {
                    {Nucleotide.Adenine, Nucleotide.Uracil},
                    {Nucleotide.Guanine, Nucleotide.Cytosine},
                    {Nucleotide.Uracil, Nucleotide.Adenine},
                    {Nucleotide.Cytosine, Nucleotide.Guanine}
                };
            }
        }

        /// <summary>
        /// Returns the proper complement table for the given nucleotide alphabet
        /// </summary>
        /// <param name="alphabet"></param>
        /// <returns></returns>
        public static IDictionary<Nucleotide, Nucleotide> GetComplementTable(AlphabetType alphabet)
        {
            switch (alphabet)
            {
                case AlphabetType.StrictDna:
                    return StrictDnaComplements;
                case AlphabetType.AmbiguousDna:
                    return AmbiguousDnaComplements;
                case AlphabetType.StrictRna:
                    return StrictRnaComplements;
                case AlphabetType.AmbiguousRna:
                    return AmbiguousRnaComplements;
                default:
                    throw new ArgumentException(String.Format(InvalidNucleotideAlphabet, alphabet));
            }
        }

        /// <summary>
        /// Transcription table from the strict RNA alphabet to the strict DNA alphabet
        /// </summary>
        public static IDictionary<Nucleotide, Nucleotide> UnambiguousRnaTranslationTable
        {
            get
            {
                return new Dictionary<Nucleotide, Nucleotide>
                {
                    {Nucleotide.Adenine, Nucleotide.Adenine},
                    {Nucleotide.Thymine, Nucleotide.Uracil},
                    {Nucleotide.Guanine, Nucleotide.Cytosine},
                    {Nucleotide.Cytosine, Nucleotide.Guanine},
                };
            }
        }

        /// <summary>
        /// Transcription table from the ambiguous RNA alphabet to the ambiguous DNA alphabet
        /// </summary>
        public static IDictionary<Nucleotide, Nucleotide> AmbiguousRnaTranslationTable
        {
            get
            {
                var computedTranslation = new Dictionary<Nucleotide, Nucleotide>(AmbiguousDnaComplements);
                computedTranslation.Remove(Nucleotide.Thymine);
                computedTranslation.Add(Nucleotide.Uracil, Nucleotide.Thymine);
                return computedTranslation;
            }
        }

        /// <summary>
        /// Transcription table from the ambiguous DNA alphabet to the ambiguous RNA alphabet
        /// </summary>
        public static IDictionary<Nucleotide, Nucleotide> AmbiguousDnaTranscriptionTable
        {
            get
            {
                var computedTranscriptionTable = new Dictionary<Nucleotide, Nucleotide>(AmbiguousRnaTranslationTable);
                computedTranscriptionTable.Remove(Nucleotide.Uracil);
                computedTranscriptionTable.Add(Nucleotide.Thymine, Nucleotide.Uracil);
                return computedTranscriptionTable;
            }
        }

        /// <summary>
        /// Transcription table from the strict DNA alphabet to the strict RNA alphabet
        /// </summary>
        public static IDictionary<Nucleotide, Nucleotide> UnambiguousDnaTranscriptionTable
        {
            get
            {
                var computedTranscriptionTable = new Dictionary<Nucleotide, Nucleotide>(UnambiguousRnaTranslationTable);
                computedTranscriptionTable.Remove(Nucleotide.Thymine);
                computedTranscriptionTable.Add(Nucleotide.Uracil, Nucleotide.Thymine);
                return computedTranscriptionTable;
            }
        }

        /// <summary>
        /// Returns the transcription table for DNA to RNA conversions, or the back transcription table for RNA to DNA conversion. An RNA alphabet will return
        /// a DNA alphabet.
        /// </summary>
        /// <param name="nucleotideAlphabet"></param>
        /// <returns></returns>
        public static IDictionary<Nucleotide, Nucleotide> GetTranscriptionTable(AlphabetType nucleotideAlphabet)
        {
            switch (nucleotideAlphabet)
            {
                case AlphabetType.AmbiguousDna:
                    return AmbiguousDnaTranscriptionTable;
                case AlphabetType.StrictDna:
                    return UnambiguousDnaTranscriptionTable;
                case AlphabetType.AmbiguousRna:
                    return AmbiguousRnaTranslationTable;
                case AlphabetType.StrictRna:
                    return UnambiguousRnaTranslationTable;
                default:
                    throw new ArgumentException(String.Format(InvalidNucleotideAlphabet, nucleotideAlphabet));
            }
        }

        /// <summary>
        /// Returns the translation table for RNA triplet or DNA triplet to amino acid conversion
        /// </summary>
        /// <param name="geneticCode"></param>
        /// <param name="nucleotideAlphabet"></param>
        /// <returns></returns>
        public static IDictionary<string, AminoAcid> GetTranslationTable(GeneticCode.GeneticCode geneticCode, AlphabetType nucleotideAlphabet)
        {
            Dictionary<string, AminoAcid> rnaTranslationTable;
            switch (geneticCode)
            {
                case GeneticCode.GeneticCode.Standard:
                    rnaTranslationTable = new Dictionary<string, AminoAcid>(StandardGeneticCode.RnaTranslationTable);
                    break;
                case GeneticCode.GeneticCode.VertebrateMitochondria:
                    rnaTranslationTable = new Dictionary<string, AminoAcid>(VertebrateMitochondrialCode.RnaTranslationTable);
                    break;
                case GeneticCode.GeneticCode.YeastMitochondrial:
                    rnaTranslationTable = new Dictionary<string, AminoAcid>(YeastMitochondrialCode.RnaTranslationTable);
                    break;
                case GeneticCode.GeneticCode.MoldProtozoanCoelenterateMitochondrialAndMycoplasmaSpiroplasma:
                    rnaTranslationTable = new Dictionary<string, AminoAcid>(MoldProtozoanCoelenterateMitochondrialAndMycoplasmaSpiroplasma.RnaTranslationTable);
                    break;
                case GeneticCode.GeneticCode.InvertebrateMitochrondria:
                    rnaTranslationTable = new Dictionary<string, AminoAcid>(InvertebrateMitochondria.RnaTranslationTable);
                    break;
                case GeneticCode.GeneticCode.CiliateDasycladaceanHexamita:
                    rnaTranslationTable = new Dictionary<string, AminoAcid>(CiliateDasycladaceanHexamita.RnaTranslationTable);
                    break;
                case GeneticCode.GeneticCode.EchinodermFlatwormMitochondrial:
                    rnaTranslationTable = new Dictionary<string, AminoAcid>(EchinodermFlatwormMitochondrial.RnaTranslationTable);
                    break;
                case GeneticCode.GeneticCode.EuplotidNuclear:
                    rnaTranslationTable = new Dictionary<string, AminoAcid>(EuplotidNuclear.RnaTranslationTable);
                    break;
                case GeneticCode.GeneticCode.BacterialArchaealPlantPlastid:
                    rnaTranslationTable = new Dictionary<string, AminoAcid>(BacterialArchaealPlantPlastid.RnaTranslationTable);
                    break;
                case GeneticCode.GeneticCode.AlternativeYeastNuclear:
                    rnaTranslationTable = new Dictionary<string, AminoAcid>(AlternativeYeastNuclear.RnaTranslationTable);
                    break;
                case GeneticCode.GeneticCode.AscidianMitochondrial:
                    rnaTranslationTable = new Dictionary<string, AminoAcid>(AscidianMitochondrial.RnaTranslationTable);
                    break;
                case GeneticCode.GeneticCode.AlternativeFlatwormMitochondrial:
                    rnaTranslationTable = new Dictionary<string, AminoAcid>(AlternativeFlatwormMitochondrial.RnaTranslationTable);
                    break;
                case GeneticCode.GeneticCode.ChlorophyceanMitochondrial:
                    rnaTranslationTable = new Dictionary<string, AminoAcid>(ChlorophyceanMitochondrial.RnaTranslationTable);
                    break;
                case GeneticCode.GeneticCode.TrematodeMitochondrial:
                    rnaTranslationTable = new Dictionary<string, AminoAcid>(TrematodeMitochondrial.TranscriptionTable);
                    break;
                case GeneticCode.GeneticCode.ScenedesmusObliquusMitochondrial:
                    rnaTranslationTable = new Dictionary<string, AminoAcid>(ScenedesmusObliquusMitochondrial.RnaTranslationTable);
                    break;
                case GeneticCode.GeneticCode.ThraustochytriumMitochondrial:
                    rnaTranslationTable = new Dictionary<string, AminoAcid>(ThraustochytriumMitochondrial.RnaTranslationTable);
                    break;
                case GeneticCode.GeneticCode.PterobranchiaMitochondrial:
                    rnaTranslationTable = new Dictionary<string, AminoAcid>(PterobranchiaMitochondrial.RnaTranslationTable);
                    break;
                case GeneticCode.GeneticCode.CandidateDivisionSr1AndGracilibacteria:
                    rnaTranslationTable = new Dictionary<string, AminoAcid>(CandidateDivisionSr1AndGracilibacteria.RnaTranslationTable);
                    break;
                default:
                    throw new ArgumentException(String.Format(_invalidGeneticCode, geneticCode));
            }

            switch (nucleotideAlphabet)
            {
                case AlphabetType.AmbiguousRna:
                    return rnaTranslationTable;
                case AlphabetType.StrictRna:
                    return rnaTranslationTable;
                case AlphabetType.AmbiguousDna:
                    return ConvertRnaTranslationTableToDnaTranslationTable(rnaTranslationTable);
                case AlphabetType.StrictDna:
                    return ConvertRnaTranslationTableToDnaTranslationTable(rnaTranslationTable);
                default:
                    throw new ArgumentException(String.Format(InvalidNucleotideAlphabet, nucleotideAlphabet));
            }
        }

        private static Dictionary<string, AminoAcid> ConvertRnaTranslationTableToDnaTranslationTable(Dictionary<string, AminoAcid> rnaTable)
        {
            var dnaTable = new Dictionary<string, AminoAcid>(rnaTable.Count);
            foreach (var pair in rnaTable)
            {
                var dnaCodon = pair.Key.Replace("U", "T").Replace("u", "t");
                dnaTable.Add(dnaCodon, pair.Value);
            }
            return dnaTable;
        }

        /// <summary>
        /// Contains the symbols that correspond to GC content in ambiguous DNA and RNA alphabets
        /// </summary>
        public static ISet<Nucleotide> AmbiguousAlphabetGcSymbols
        {
            get
            {
                return new HashSet<Nucleotide> { Nucleotide.Guanine, Nucleotide.Cytosine, Nucleotide.Strong };
            }
        }

        /// <summary>
        /// Contains the symbols that correspond to GC content in strict DNA and RNA alphabets
        /// </summary>
        public static ISet<Nucleotide> UnambiguousAlphabetGcSymbols
        {
            get
            {
                return new HashSet<Nucleotide>{Nucleotide.Guanine, Nucleotide.Cytosine};
            }
        } 

        /// <summary>
        /// Returns the set of symbols that are used to compute the GC content given an alphabet type
        /// </summary>
        /// <param name="alphabetType"></param>
        /// <returns></returns>
        public static ISet<Nucleotide> GcContentSymbols(AlphabetType alphabetType)
        {
            switch (alphabetType)
            {
                case AlphabetType.AmbiguousDna:
                    return AmbiguousAlphabetGcSymbols;
                case AlphabetType.AmbiguousRna:
                    return AmbiguousAlphabetGcSymbols;
                case AlphabetType.StrictDna:
                    return UnambiguousAlphabetGcSymbols;
                case AlphabetType.StrictRna:
                    return UnambiguousAlphabetGcSymbols;
                default:
                    throw new ArgumentException(String.Format(InvalidNucleotideAlphabet, alphabetType));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCompute.Data.Alphabets
{
    internal static class NucleotideAlphabetDataProvider
    {
        #region Nucleotide sets
        private static HashSet<Nucleotide> _allNucleotides;
        public static ISet<Nucleotide> AllNucleotides
        {
            get
            {
                if (_allNucleotides == null)
                {
                    _allNucleotides = new HashSet<Nucleotide>((Nucleotide[])Enum.GetValues(typeof(Nucleotide)));
                }
                return _allNucleotides;
            }
        }

        private static HashSet<Nucleotide> _unambiguousDna;
        public static ISet<Nucleotide> UnambiguousDna
        {
            get
            {
                if (_unambiguousDna == null)
                {
                    _unambiguousDna = new HashSet<Nucleotide>((Nucleotide[])Enum.GetValues(typeof(StrictDna)));
                }
                return _unambiguousDna;
            }
        }

        public static ISet<Nucleotide> NotInAnyDnaAlphabet = new HashSet<Nucleotide> { Nucleotide.Uracil };

        private static HashSet<Nucleotide> _ambiguousDna;
        public static ISet<Nucleotide> AmbiguousDna
        {
            get
            {
                if (_ambiguousDna == null)
                {
                    _ambiguousDna = new HashSet<Nucleotide>(AllNucleotides.Except(NotInAnyDnaAlphabet));
                }
                return _ambiguousDna;
            }
        }

        private static HashSet<Nucleotide> _unambiguousRna;
        public static ISet<Nucleotide> UnambiguousRna
        {
            get
            {
                if (_unambiguousRna == null)
                {
                    _unambiguousRna = new HashSet<Nucleotide>((Nucleotide[])Enum.GetValues(typeof(StrictRna)));
                }
                return _unambiguousRna;
            }
        }

        public static ISet<Nucleotide> NotInAnyRnaAlphabet = new HashSet<Nucleotide> { Nucleotide.Thymine };

        private static HashSet<Nucleotide> _ambiguousRna;
        public static ISet<Nucleotide> AmbiguousRna
        {
            get
            {
                if (_ambiguousRna == null)
                {
                    _ambiguousRna = new HashSet<Nucleotide>(AllNucleotides.Except(NotInAnyRnaAlphabet));
                }
                return _ambiguousRna;
            }
        }
        #endregion

        public static ISet<Nucleotide> GetAllowedSymbols(AlphabetType alphabet)
        {
            switch (alphabet)
            {
                case AlphabetType.UnambiguousDna:
                    return UnambiguousDna;
                case AlphabetType.AmbiguousDna:
                    return AmbiguousDna;
                case AlphabetType.UnambiguousRna:
                    return UnambiguousRna;
                case AlphabetType.AmbiguousRna:
                    return AmbiguousRna;
                default:
                    throw new ArgumentException(String.Format("{0} is not a nucleotide alphabet", alphabet));
            }
        }
    }
}

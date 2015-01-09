using System.Collections.Generic;
using System.Collections.Immutable;

namespace BCompute
{
    internal sealed class Maps
    {
        public const char StopTranslation = '*';
        public const char SkipNucleotides = '-';
        private static ImmutableDictionary<char, char> _dnaComplements;
        public static ImmutableDictionary<char, char> DnaComplements
        {
            get
            {
                if (_dnaComplements == null || _dnaComplements.Count == 0)
                {
                    var dnaComplements = new Dictionary<char, char>
                    {
                        {(char)StrictDna.Adenine, (char)StrictDna.Thymine},
                        {(char)StrictDna.Guanine, (char)StrictDna.Cytosine},
                        {(char)StrictDna.Thymine, (char)StrictDna.Adenine},
                        {(char)StrictDna.Cytosine, (char)StrictDna.Guanine}
                    };
                    _dnaComplements = dnaComplements.ToImmutableDictionary();
                }
                return _dnaComplements;
            }
        }

        private static ImmutableDictionary<char, char> _rnaComplements;
        public static ImmutableDictionary<char, char> RnaComplements
        {
            get
            {
                if (_rnaComplements == null || _rnaComplements.Count == 0)
                {
                    var rnaComplements = new Dictionary<char, char>
                    {
                        {(char)StrictRna.Adenine, (char)StrictRna.Uracil},
                        {(char)StrictRna.Guanine, (char)StrictRna.Cytosine},
                        {(char)StrictRna.Uracil, (char)StrictRna.Adenine},
                        {(char)StrictRna.Cytosine, (char)StrictRna.Guanine}
                    };
                    _rnaComplements = rnaComplements.ToImmutableDictionary();
                }
                return _rnaComplements;
            }
        }
    }
}

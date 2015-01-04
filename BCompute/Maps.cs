using System.Collections.Generic;
using System.Collections.Immutable;

namespace BCompute
{
    internal sealed class Maps
    {
        private static ImmutableDictionary<char, char> _dnaComplements;
        public static ImmutableDictionary<char, char> DnaComplements
        {
            get
            {
                if (_dnaComplements == null || _dnaComplements.Count == 0)
                {
                    var dnaComplements = new Dictionary<char, char>
                    {
                        {'A', 'T'},
                        {'G', 'C'},
                        {'T', 'A'},
                        {'C', 'G'}
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
                        {'A', 'U'},
                        {'G', 'C'},
                        {'U', 'A'},
                        {'C', 'G'}
                    };
                    _rnaComplements = rnaComplements.ToImmutableDictionary();
                }
                return _rnaComplements;
            }
        }
    }
}

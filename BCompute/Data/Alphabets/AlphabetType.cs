namespace BCompute.Data.Alphabets
{
    public enum AlphabetType
    {
        /// <summary>
        /// IUPAC ambiguous DNA alphabet
        /// </summary>
        AmbiguousDna,
        /// <summary>
        /// IUPAC unambiguous DNA alphabet
        /// </summary>
        StrictDna,
        /// <summary>
        /// IUPAC ambiguous RNA alphabet
        /// </summary>
        AmbiguousRna,
        /// <summary>
        /// IUPAC unambiguous RNA alphabet
        /// </summary>
        StrictRna,
        /// <summary>
        /// IUPAC standard Protein alphabet
        /// </summary>
        StandardProtein,
        /// <summary>
        /// IUPAC extended Protein alphabet
        /// </summary>
        ExtendedProtein,
    }
}

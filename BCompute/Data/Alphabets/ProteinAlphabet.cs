namespace BCompute.Data.Alphabets
{
    public enum ProteinAlphabet
    {
        StandardProtein,
        ExtendedProtein,
    }

    /// <summary>
    /// Protein names and symbols adapted from http://blast.ncbi.nlm.nih.gov/blastcgihelp.shtml
    /// </summary>
    public enum Protein
    {
        Alanine = 'A',
        AspartateOrAsparagine = 'B',
        Cysteine = 'C',
        Aspartate = 'D',
        Glutamate = 'E',
        Phenylalanine = 'F',
        Glycine = 'G',
        Histidine = 'H',
        Isoleucine = 'I',
        Lysine = 'K',
        Leucine = 'L',
        Methionine = 'M',
        Asparagine = 'N',
        Proline = 'P',
        Glutamine = 'Q',
        Arginine = 'R',
        Serine = 'S',
        Threonine = 'T',
        Selenocystein = 'U',
        Valine = 'V',
        Tryptophan = 'W',
        Unknown = 'X',
        Tyrosine = 'Y',
        GlutamateOrGlutamine = 'Z',
        Stop = '.',
        Gap = '-'
    }
}

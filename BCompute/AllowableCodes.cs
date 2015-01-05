namespace BCompute
{
    //A  adenosine          C  cytidine             G  guanine
    //T  thymidine          N  A/G/C/T (any)        U  uridine 
    //K  G/T (keto)         S  G/C (strong)         Y  T/C (pyrimidine) 
    //M  A/C (amino)        W  A/T (weak)           R  G/A (purine)        
    //B  G/T/C              D  G/A/T                H  A/C/T      
    //V  G/C/A              -  gap of indeterminate length

    /// <summary>
    /// IUPAC nucleotide codes
    /// 
    /// These codes are defined at http://www.chem.qmul.ac.uk/iubmb/misc/naseq.html
    /// </summary>
    public enum Nucleotide
    {
        Adenine = 'A',
        NotAdenine = 'B',
        Cytosine = 'C',
        NotCytosine = 'D',
        Guanine = 'G',
        NotGuanine = 'H',
        Keto = 'K',
        Amino = 'M',
        Unknown = 'N',
        Purine = 'R',
        Strong = 'S',
        Thymine = 'T',
        Uracil = 'U',
        NotThymine = 'V',
        Weak = 'W',
        Pyrimidine = 'Y',
        Gap = '-',
    }

    public enum StrictRna
    {
        Adenine = 'A',
        Cytosine = 'C',
        Thymine = 'T',
        Uracil = 'U',
    }

    public enum StrictDna
    {
        Adenine = 'A',
        Cytosine = 'C',
        Thymine = 'T',
        Guanine = 'G',
    }

    /// <summary>
    /// Adapted from http://blast.ncbi.nlm.nih.gov/blastcgihelp.shtml
    /// </summary>
    public enum AminoAcid
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

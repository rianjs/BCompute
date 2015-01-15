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
        //ambiguous dna is usually: GATCRYWSMKHBVDN (no uracil)
        //ambiguous rna is usually: GAUCRYWSMKHBVDN (no thymine)
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
        Gap = '-',      //Does not appear in some ambiguous DNA/RNA alphabets
    }

    public enum StrictRna
    {
        Adenine = 'A',
        Uracil = 'U',
        Cytosine = 'C',
        Guanine = 'G',
    }

    public enum StrictDna
    {
        Adenine = 'A',
        Thymine = 'T',
        Cytosine = 'C',
        Guanine = 'G',
    }
}

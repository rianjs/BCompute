﻿namespace BCompute
{
    /// <summary>
    /// ProteinType names and symbols adapted from http://blast.ncbi.nlm.nih.gov/blastcgihelp.shtml
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
        LeucineOrIsoleucine = 'J',
        Lysine = 'K',
        Leucine = 'L',
        Methionine = 'M',
        Asparagine = 'N',
        Pyrrolysine = 'O',
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

    //Standard protein: ACDEFGHIKLMNPQRSTVWY
    //Extended protein: ACDEFGHIKLMNPQRSTVWY BXZJUO

    public enum StandardProtein
    {
        Alanine = 'A',
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
        Pyrrolysine = 'O',
        Proline = 'P',
        Glutamine = 'Q',
        Arginine = 'R',
        Serine = 'S',
        Threonine = 'T',
        Valine = 'V',
        Tryptophan = 'W',
        Tyrosine = 'Y',
    }

    public enum ExtendedProtein
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
        LeucineOrIsoleucine = 'J',
        Lysine = 'K',
        Leucine = 'L',
        Methionine = 'M',
        Asparagine = 'N',
        Pyrrolysine = 'O',
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
    }
}

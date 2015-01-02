using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCompute;

namespace Tester
{
    public class Tester
    {
        static void Main()
        {
            //What is the probability of getting a child that expressed the dominant allele?
            //p(D*) = p(DD) + p(Dd)
            //p(dd) = 1.0d - p(D*) OR p(dd) = p(dd)
            var pop = new Population(2, 2, 2);
            var pDominant = pop.GetChildAlleleProbability(Genotype.HomozygousDominant) + pop.GetChildAlleleProbability(Genotype.Heterozygous);
            var pRecessiveA = 1.0d - pDominant;
            var pRecessiveB = pop.GetChildAlleleProbability(Genotype.HomozygousRecessive);
            Console.WriteLine("Computed using 1.0 - <value> = {0:N5}", pRecessiveA);
            Console.WriteLine("Computed using GetChildAlleleProbability(recessive): {0:N5}", pRecessiveB);
            Console.WriteLine("Probability of a child expressing the dominant trait: {0:N5}", pDominant);
            Console.ReadLine();
        }
    }
}

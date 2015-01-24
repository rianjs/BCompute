using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using BCompute;

namespace Tester
{
    public class Tester
    {
        static void Main()
        {
            var initialPopulation = 1;
            var maturity = 1;
            var offspringPerCycle = 1;
            var lifespan = 3;
            //var population = new SexualReproductionModeler(initialPopulation, maturity, offspringPerCycle, lifespan);
            //var foo = population.GetPopulationCount(6);     //Should be 4

            //var immortal = new SexualReproductionModeler(1, 1, 3, int.MaxValue);
            //var immortalAnswer = immortal.GetPopulationCount(5);
            //Console.WriteLine(foo);
            //Console.WriteLine(immortalAnswer);  //Should be 19

            var bigTest = new SexualReproductionModeler(initialPopulation, offspringPerCycle, int.MaxValue);
            var immortalTotal = bigTest.GetPopulationCount(35);
            Console.WriteLine("{0:N0}", immortalTotal);

            lifespan = 18;
            var actualTest = new SexualReproductionModeler(initialPopulation, maturity, offspringPerCycle, lifespan);
            var total = actualTest.GetPopulationCount(87);      //Should be 676172117379838466 -- OK
            Console.WriteLine(total);
            Console.WriteLine("{0:N0}", total);
            Console.ReadLine();
        }
    }
}

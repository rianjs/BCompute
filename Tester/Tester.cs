using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BCompute;

namespace Tester
{
    public class Tester
    {
        static void Main()
        {
            var generations = 6;
            var growthPerGeneration = 2;
            var survivalGenerations = int.MaxValue;

            var foo = ReprodutiveModeling.Growth(generations, growthPerGeneration, survivalGenerations);
            Console.WriteLine(foo);


            Console.ReadLine();
        }
    }
}

using System;
using System.Linq;
using BCompute;

namespace Tester
{
    public class Tester
    {
        static void Main()
        {
            const string parent = @"C:\Users\rianjs\Desktop";
            const string filename = "overlap-graphs3.txt";

            //var model = new ExpectedOffspringModel(19843, 16233, 18989, 19312, 16213, 17310);  //155311
            var model = new ExpectedOffspringModel(1, 0, 0, 1, 0, 1); //3.5
            var pSum = model.Parents.Values.Sum();
            Console.WriteLine("Probability sum: {0}", pSum);    //OK

            var hetero = model.ExpectedOffspring(Genotype.Heterozygous, 2);
            var dominant = model.ExpectedOffspring(Genotype.Dominant, 2);
            var recessive = model.ExpectedOffspring(Genotype.Recessive, 2);

            var answer = hetero + dominant;
            Console.WriteLine("Direct answer: {0:N}", answer);

            Console.ReadLine();
        }
    }
}

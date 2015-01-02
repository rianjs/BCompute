using System.Linq;

namespace BCompute
{
    public class Recurrence
    {
        public static long Growth(int generations, int growthPerGeneration, int survivalGenerations = int.MaxValue)
        {
            // F(generations - 1) + growthPerGeneration * F(generations - 2)

            var lookupTable = new long[generations];
            for (var i = 0; i < generations; i++)
            {
                if (i == 0 || i == 1)
                {
                    lookupTable[i] = 1;
                }
                else
                {
                    lookupTable[i] = lookupTable[i - 1] + growthPerGeneration * lookupTable[i - 2];
                }
            }
            return lookupTable.Last();
        }
    }
}

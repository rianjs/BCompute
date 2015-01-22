using System;
using System.Linq;

namespace BCompute
{
    public class ReprodutiveModeling
    {
        //Modeling recurrence relations. Elements:
        //  Cycles until maturity
        //  Lifespan in cycles
        //  Offspring per cycle
        //  Reproductive cycles before death
        //  Cycles between birth and reproduction

        public static long Growth(int generations, int growthPerGeneration, int survivalGenerations = Int32.MaxValue)
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
                    var number = lookupTable[i - 1] + growthPerGeneration * lookupTable[i - 2];
                    if (i >= survivalGenerations)
                    {
                        number -= lookupTable[i - survivalGenerations];
                    }
                    lookupTable[i] = number;
                }
            }
            return lookupTable.Last();
        }
    }
}

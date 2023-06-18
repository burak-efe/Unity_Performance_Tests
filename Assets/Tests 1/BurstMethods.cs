using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Mathematics;

namespace Tests_1
{
    [BurstCompile]
    public static class BurstMethods
    {
        [BurstCompile]
        public static void Float3Math(ref float3 output)
        {
            var value = new float3();
            
            for (int i = 0; i < 10_000; i++)
            {
                value = new float3(17, 17, 17) + new float3(19, 19, 19);
                value = math.normalize(value);
                value /= math.PI;
            }

            output = value;
        }


        [BurstCompile]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static int NotInlined()
        {
            var i = 17 * 19;
            for (int j = 0; j < 1000; j++)
            {
                i *= i;
            }

            return i;
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Inlined()
        {
            var i = 17 * 19;
            for (int j = 0; j < 1000; j++)
            {
                i *= i;
            }

            return i;
        }
    }
}
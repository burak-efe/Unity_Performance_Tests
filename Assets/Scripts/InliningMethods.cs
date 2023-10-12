using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace Ica.Benchmarks
{
    [BurstCompile]
    public class InliningMethods
    {
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
    
    
        [BurstCompile]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static int BurstedNotInlined()
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
        public static int BurstedInlined()
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
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Tests_1
{
    [BurstCompile]
    public static class BurstMethods
    {
        [BurstCompile]
        public static void BurstedBasicIO(ref NativeArray<float> inArray ,ref NativeArray<float> outArray)
        {
            for (int i = 0; i < inArray.Length; i++)
            {
                outArray[i] = inArray[i];
            }
        }
    }
}
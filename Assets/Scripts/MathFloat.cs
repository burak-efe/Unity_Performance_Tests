using System;
using Unity.Burst;
using Unity.Collections;
using UnityEngine;

namespace Ica.Benchmarks
{
    [BurstCompile]
    public static class MathFloat
    {
        public static void FloatMul(ref NativeArray<float> inArr, in float val)
        {
            for (int i = 0; i < inArr.Length; i++)
            {
                inArr[i] *= val;
            }
        }

        public static void DoubleMul(ref NativeArray<double> inArr, in double val)
        {
            for (int i = 0; i < inArr.Length; i++)
            {
                inArr[i] *= val;
            }
        }

        [BurstCompile]
        public static void FloatMul_Bursted([NoAlias] ref NativeArray<float> inArr, [NoAlias] in float val)
        {
            for (int i = 0; i < inArr.Length; i++)
            {
                inArr[i] *= val;
            }
        }


        [BurstCompile]
        public static void DoubleMul_Bursted([NoAlias] ref NativeArray<double> inArr, [NoAlias] in double val)
        {
            for (int i = 0; i < inArr.Length; i++)
            {
                inArr[i] *= val;
            }
        }


        public static float FloatMath(ref float output)
        {
            for (int i = 0; i < 10_000; i++)
            {
                output *= Mathf.PI;
                output += Mathf.PI;
                output /= Mathf.PI;
                output -= 0.5f;
            }

            return output;
        }

        public static double DoubleMath(ref double output)
        {
            for (int i = 0; i < 10_000; i++)
            {
                output *= Math.PI;
                output += Math.PI;
                output /= Math.PI;
                output -= 0.5d;
            }

            return output;
        }

        [BurstCompile]
        public static float FloatMath_Bursted(ref float output)
        {
            for (int i = 0; i < 10_000; i++)
            {
                output *= Mathf.PI;
                output += Mathf.PI;
                output /= Mathf.PI;
                output -= 0.5f;
            }

            return output;
        }

        [BurstCompile]
        public static double DoubleMath_Bursted(ref double output)
        {
            for (int i = 0; i < 10_000; i++)
            {
                output *= Math.PI;
                output += Math.PI;
                output /= Math.PI;
                output -= 0.5d;
            }

            return output;
        }

        public static float AddFloats(ref float a, ref float b)
        {
            var x = a + b;
            for (int i = 0; i < 100; i++)
            {
                x += a;
                x += b;
            }

            return x;
        }

        [BurstCompile]
        public static float AddFloats_Bursted(ref float a, ref float b)
        {
            var x = a + b;
            for (int i = 0; i < 100; i++)
            {
                x += a;
                x += b;
            }

            return x;
        }

        public static double AddDoubles(ref double a, ref double b)
        {
            var x = a + b;
            for (int i = 0; i < 100; i++)
            {
                x += a;
                x += b;
            }

            return x;
        }

        [BurstCompile]
        public static double AddDoubles_Bursted(ref double a, ref double b)
        {
            var x = a + b;
            for (int i = 0; i < 100; i++)
            {
                x += a;
                x += b;
            }

            return x;
        }
    }
}
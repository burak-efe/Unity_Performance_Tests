using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile(CompileSynchronously = true)]
public static class MathMethods
{
    public static void BasicMath_Default(ref Vector3 output)
    {
        for (int i = 0; i < 10_000; i++)
        {
            var x = Vector3.Normalize(output);
            output += x;
            x -= output;
            output *= x.x;
            output /= Mathf.PI;
            output *= Mathf.Sqrt(output.x);
        }
    }

    [BurstCompile]
    public static void BasicMath_BurstedNewMath(ref float3 output)
    {
        for (int i = 0; i < 10_000; i++)
        {
            var x = math.normalize(output);
            output += x;
            x -= output;
            output *= x.x;
            output /= math.PI;
            output *= math.sqrt(output.x);
        }
    }

    public static void BasicMath_NewMath(ref float3 output)
    {
        for (int i = 0; i < 10_000; i++)
        {
            var x = math.normalize(output);
            output += x;
            x -= output;
            output *= x.x;
            output /= math.PI;
            output *= math.sqrt(output.x);
        }
    }


    [BurstCompile]
    public static void BasicMath_IJob(ref float3 output)
    {
        var job = new BasicMath
        {
            output = output
        };
        job.Run();
    }


    [BurstCompile]
    public struct BasicMath : IJob
    {
        public float3 output;

        public void Execute()
        {
            for (int i = 0; i < 10_000; i++)
            {
                var x = math.normalize(output);
                output += x;
                x -= output;
                output *= x.x;
                output /= math.PI;
                output *= math.sqrt(output.x);
            }
        }
    }

    public static void Vector3New(ref Vector3 output)
    {
        var value = new Vector3(17, 17, 17);

        output = value;
    }

    public static void Float3New(ref float3 output)
    {
        var value = new float3(17, 17, 17);


        output = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [BurstCompile]
    public static void BurstedFloat3New(ref float3 output)
    {
        var value = new float3(17, 17, 17);

        output = value;
    }


    [BurstCompile]
    public static void Float3Length(ref NativeArray<float3> inArray, ref NativeArray<float> outArray)
    {
        for (int i = 0; i < inArray.Length; i++)
        {
            outArray[i] = math.length(inArray[i]);
        }
    }

    [BurstCompile]
    public static void Float3LengthSq(ref NativeArray<float3> inArray, ref NativeArray<float> outArray)
    {
        for (int i = 0; i < inArray.Length; i++)
        {
            outArray[i] = math.lengthsq(inArray[i]);
        }
    }

    public static void Vector3Length(ref Vector3[] inArray, ref float[] outArray)
    {
        for (int i = 0; i < inArray.Length; i++)
        {
            outArray[i] = Vector3.Magnitude(inArray[i]);
        }
    }

    public static void Vector3SquaredLength(ref Vector3[] inArray, ref float[] outArray)
    {
        for (int i = 0; i < inArray.Length; i++)
        {
            outArray[i] = Vector3.SqrMagnitude(inArray[i]);
        }
    }


    [BurstCompile]
    public static void Float4_Array_Mul(ref NativeArray<float4> inArr, float val)
    {
        for (int i = 0; i < inArr.Length; i++)
        {
            inArr[i] *= val;
        }
    }

    [BurstCompile]
    public static void Float4_Array_Mul_TwoPerIteration(ref NativeArray<float4> inArr, float val)
    {
        for (int i = 0; i < inArr.Length; i += 2)
        {
            inArr[i] *= val;
            inArr[i + 1] *= val;
        }
    }

    [BurstCompile]
    public static void Float_Array_Mul(ref NativeArray<float> inArr, float val)
    {
        for (int i = 0; i < inArr.Length; i++)
        {
            inArr[i] *= val;
        }
    }

    [BurstCompile]
    public static void Float3_Array_Mul(ref NativeArray<float3> inArr, float val)
    {
        for (int i = 0; i < inArr.Length; i++)
        {
            inArr[i] *= val;
        }
    }

    [BurstCompile]
    public static void IntArraySum(ref NativeArray<int> inArr, out int sum)
    {
        sum = 0;
        for (int i = 0; i < inArr.Length; i++)
        {
            sum += inArr[i];
        }
    }

    [BurstCompile]
    public static void IntArraySum_8perIter(ref NativeArray<int> inArr, out int sum)
    {
        sum = 0;
        for (int i = 0; i < inArr.Length; i += 8)
        {
            sum += inArr[i];
            sum += inArr[i + 1];
            sum += inArr[i + 2];
            sum += inArr[i + 3];
            sum += inArr[i + 4];
            sum += inArr[i + 5];
            sum += inArr[i + 6];
            sum += inArr[i + 7];
        }
    }

    // [BurstCompile]
    // public static void IntArraySum_Intrs(ref NativeArray<int> inArr, out int sum)
    // {
    //     sum = 0;
    //     for (int i = 0; i < inArr.Length; i += 8)
    //     {
    //         var num = new Unity.Burst.Intrinsics.v256(inArr[i], inArr[i + 1], inArr[i + 2], inArr[i + 3], inArr[i + 4], inArr[i + 5], inArr[i + 6], inArr[i + 7]);
    //
    //     }
    // }
}
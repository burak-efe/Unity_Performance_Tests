using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
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
    
    
    [BurstCompile(CompileSynchronously = true)]
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
}
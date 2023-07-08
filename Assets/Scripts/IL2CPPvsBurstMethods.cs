using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class IL2CPPvsBurstMethods
{
    public static void Vec3Add_Default(ref Vector3 output)
    {
        for (int i = 0; i < 10_000; i++)
        {
            output += output;
        }
    }

    [BurstCompile]
    public static void Vec3Add_Burst(ref float3 output)
    {
        for (int i = 0; i < 10_000; i++)
        {
            output += output;
        }
    }
    
    public static void Vec3AddArray_Default(ref Vector3[] input, ref Vector3[] output)
    {
        for (int i = 0; i < output.Length; i++)
        {
            output[i] += input[i];
        }
    }

    [BurstCompile]
    public static void Vec3AddArray_Burst(ref float3[] input,ref float3[] output)
    {
        for (int i = 0; i < output.Length; i++)
        {
            output[i] += input[i];
        }
    }
    
    [BurstCompile]
    public static void Vec3AddNativeArray_Burst(ref NativeArray<float3> input,ref NativeArray<float3> output)
    {
        for (int i = 0; i < output.Length; i++)
        {
            output[i] += input[i];
        }
    }
    
    
    public static void VecMul_Default(ref Vector3 output)
    {
        for (int i = 0; i < 10_000; i++)
        {
            
            output *= 19.17f;
        }
    }

    [BurstCompile]
    public static void VecMul_Burst(ref float3 output)
    {
        for (int i = 0; i < 10_000; i++)
        {
            output *= 19.17f;
        }
    }
    
    
    public static void VecUnpack_Default(ref Vector3 output)
    {
        for (int i = 0; i < 10_000; i++)
        {
            var sum = output.x + output.y + output.z;
            var sum2 = output.z - output.y - output.x;
            output.z = sum;
            output.x *= sum2;
        }
    }

    [BurstCompile]
    public static void VecUnpack_Burst(ref float3 output)
    {
        for (int i = 0; i < 10_000; i++)
        {
            var sum = output.x + output.y + output.z;
            var sum2 = output.z - output.y - output.x;
            output.z = sum;
            output.x *= sum2;
        }
    }
    
    public static void Vec3Normalize_Default(ref Vector3 output)
    {
        for (int i = 0; i < 10_000; i++)
        {
            output  = output.normalized;
        }
    }
    
    public static void Vec3Normalize_DefaultCustom(ref Vector3 output)
    {
        for (int i = 0; i < 10_000; i++)
        {
            output /= Vector3.Magnitude(output);
        }
    }
    
    [BurstCompile]
    public static void Vec3Normalize_Burst(ref float3 output)
    {
        for (int i = 0; i < 10_000; i++)
        {
            output = math.normalize(output);
        }
    }
    
    [BurstCompile]
    public static void Vec3Normalize_BurstCustom(ref float3 output)
    {
        for (int i = 0; i < 10_000; i++)
        {
            output /= math.length(output);
        }
    }
}
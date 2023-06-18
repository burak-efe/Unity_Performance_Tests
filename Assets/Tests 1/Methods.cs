using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class Methods
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
    
    
    public static void Vector3Math(ref Vector3 output)
    {
        var value = new Vector3();
            
        for (int i = 0; i < 10_000; i++)
        {
            value = new Vector3(17, 17, 17) + new Vector3(19, 19, 19);
            value = value.normalized;
            value /= Mathf.PI;
        }

        output =  value;
    }
    
    public static void Float3Math(ref float3 output)
    {
        var value = new float3();
            
        for (int i = 0; i < 10_000; i++)
        {
            value = new float3(17, 17, 17) + new float3(19, 19, 19);
            value = math.normalize(value);
            value /= math.PI;
        }

        output =  value;
    }
}
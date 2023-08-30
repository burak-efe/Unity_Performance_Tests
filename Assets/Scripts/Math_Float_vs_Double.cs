using Unity.Burst;
using Unity.Collections;

namespace Tests_1
{
    [BurstCompile]
    public static class Math_Float_vs_Double
    {
        public static void FloatMul_NonBursted(ref NativeArray<float> inArr,in float val)
        {
            for (int i = 0; i < inArr.Length; i++)
            {
                inArr[i] *= val;
            }
        }
        
        public  static void DoubleMul_NonBursted(ref NativeArray<double> inArr,in double val)
        {
            for (int i = 0; i < inArr.Length; i++)
            {
                inArr[i] *= val;
            }
        }
        
        [BurstCompile]
        public static void FloatMul([NoAlias]ref NativeArray<float> inArr,[NoAlias] in float val)
        {
            for (int i = 0; i < inArr.Length; i++)
            {
                inArr[i] *= val;
            }
        }
        
        
        [BurstCompile]
        public  static void DoubleMul([NoAlias]ref NativeArray<double> inArr,[NoAlias] in double val)
        {
            for (int i = 0; i < inArr.Length; i++)
            {
                inArr[i] *= val;
            }
        }
        
    }
}
using NUnit.Framework;
using Tests_1;
using Unity.Collections;
using Unity.Mathematics;
using Unity.PerformanceTesting;
using UnityEngine;


public class MathTests
{
    [Test, Performance]
    public void Distance_vs_SquaredDistance()
    {
        var f3 = new NativeArray<float3>(100_000, Allocator.TempJob);
        var resultNative = new NativeArray<float>(100_000, Allocator.TempJob);
        RandomFiller.FillArrayFloat3(ref f3);

        var v3 = new Vector3[100_000];
        RandomFiller.FillArrayFloat3(ref v3);
        var resultsManaged = new float[100_000];

        SampleGroup group1 = new SampleGroup("Vector3Length", SampleUnit.Microsecond);
        Measure.Method(() => MathMethods.Vector3Length(ref v3, ref resultsManaged)).SampleGroup(group1).MeasurementCount(1000).Run();

        SampleGroup group2 = new SampleGroup("Vector3SquaredLength", SampleUnit.Microsecond);
        Measure.Method(() => MathMethods.Vector3SquaredLength(ref v3, ref resultsManaged)).SampleGroup(group2).MeasurementCount(1000).Run();

        SampleGroup group3 = new SampleGroup("Bursted_Float3Length", SampleUnit.Microsecond);
        Measure.Method(() => MathMethods.Float3Length(ref f3, ref resultNative)).SampleGroup(group3).MeasurementCount(1000).Run();

        SampleGroup group4 = new SampleGroup("Bursted_Float3SquaredLength", SampleUnit.Microsecond);
        Measure.Method(() => MathMethods.Float3LengthSq(ref f3, ref resultNative)).SampleGroup(group4).MeasurementCount(1000).Run();

        f3.Dispose();
        resultNative.Dispose();
    }


    [Test, Performance]
    public void Vec3Add_Default_vs_Burst()
    {
        var v3 = new Vector3();
        var f3 = new float3();

        SampleGroup group0 = new SampleGroup("Default", SampleUnit.Microsecond);
        Measure.Method(() => IL2CPPvsBurstMethods.Vec3Add_Default(ref v3)).SampleGroup(group0).MeasurementCount(1000).Run();

        SampleGroup group1 = new SampleGroup("Burst", SampleUnit.Microsecond);
        Measure.Method(() => IL2CPPvsBurstMethods.Vec3Add_Burst(ref f3)).SampleGroup(group1).MeasurementCount(1000).Run();
    }

    [Test, Performance]
    public void Vec3AddArray_Default_vs_Burst()
    {
        var v3in = new Vector3[10_000];
        var v3out = new Vector3[10_000];
        RandomFiller.FillArrayFloat3(ref v3in);

        var f3inNative = new NativeArray<float3>(10_00, Allocator.Persistent);
        var f3outNative = new NativeArray<float3>(10_00, Allocator.Persistent);
        RandomFiller.FillArrayFloat3(ref f3inNative);

        SampleGroup group0 = new SampleGroup("Default", SampleUnit.Microsecond);
        Measure.Method(() => IL2CPPvsBurstMethods.Vec3AddArray_Default(ref v3in, ref v3out)).SampleGroup(group0).MeasurementCount(1000).Run();


        SampleGroup group2 = new SampleGroup("Burst", SampleUnit.Microsecond);
        Measure.Method(() => IL2CPPvsBurstMethods.Vec3Add_Burst(ref f3inNative, ref f3outNative)).SampleGroup(group2).MeasurementCount(1000).Run();

        f3inNative.Dispose();
        f3outNative.Dispose();
    }

    [Test, Performance]
    public void Vec3Mul_Default_vs_Burst()
    {
        var v3 = new Vector3(19, 19, 19);
        var f3 = new float3(19, 19, 19);

        SampleGroup group0 = new SampleGroup("Default", SampleUnit.Microsecond);
        Measure.Method(() => IL2CPPvsBurstMethods.VecMul_Default(ref v3)).SampleGroup(group0).MeasurementCount(1000).Run();

        SampleGroup group1 = new SampleGroup("Burst", SampleUnit.Microsecond);
        Measure.Method(() => IL2CPPvsBurstMethods.VecMul_Burst(ref f3)).SampleGroup(group1).MeasurementCount(1000).Run();
    }

    [Test, Performance]
    public void Vec3Unpack_Default_vs_Burst()
    {
        var v3 = new Vector3();
        var f3 = new float3();

        SampleGroup group0 = new SampleGroup("Default", SampleUnit.Microsecond);
        Measure.Method(() => IL2CPPvsBurstMethods.VecUnpack_Default(ref v3)).SampleGroup(group0).MeasurementCount(1000).Run();

        SampleGroup group1 = new SampleGroup("Burst", SampleUnit.Microsecond);
        Measure.Method(() => IL2CPPvsBurstMethods.VecUnpack_Burst(ref f3)).SampleGroup(group1).MeasurementCount(1000).Run();
    }


    [Test, Performance]
    public void BasicMathPerf_Default_vs_NewMath_vs_BurstedNewMath()
    {
        var v3 = new Vector3(19, 19, 19);
        var f3 = new float3(19, 19, 19);
        var f3b = new float3(19, 19, 19);

        SampleGroup group1 = new SampleGroup("Default", SampleUnit.Microsecond);
        Measure.Method(() => MathMethods.BasicMath_Default(ref v3)).SampleGroup(group1).MeasurementCount(1000).Run();

        SampleGroup group2 = new SampleGroup("NewMath", SampleUnit.Microsecond);
        Measure.Method(() => MathMethods.BasicMath_NewMath(ref f3)).SampleGroup(group2).MeasurementCount(1000).Run();

        SampleGroup group3 = new SampleGroup("BurstedNewMath", SampleUnit.Microsecond);
        Measure.Method(() => MathMethods.BasicMath_BurstedNewMath(ref f3b)).SampleGroup(group3).MeasurementCount(1000).Run();

        SampleGroup group4 = new SampleGroup("BurstedIJob", SampleUnit.Microsecond);
        Measure.Method(() => MathMethods.BasicMath_IJob(ref f3b)).SampleGroup(group4).MeasurementCount(1000).Run();
    }


    [Test, Performance]
    public void Vec3Normalize_Default_vs_Burst_vs_BurstCustom()
    {
        var v3 = new Vector3(19, 19, 19);
        var v3c = new Vector3(19, 19, 19);
        var f3 = new float3(19, 19, 19);
        var f3c = new float3(19, 19, 19);

        SampleGroup group0 = new SampleGroup("Default", SampleUnit.Microsecond);
        Measure.Method(() => IL2CPPvsBurstMethods.Vec3Normalize_Default(ref v3)).SampleGroup(group0).MeasurementCount(1000).Run();

        SampleGroup group1 = new SampleGroup("DefaultCustom", SampleUnit.Microsecond);
        Measure.Method(() => IL2CPPvsBurstMethods.Vec3Normalize_DefaultCustom(ref v3c)).SampleGroup(group1).MeasurementCount(1000).Run();

        SampleGroup group2 = new SampleGroup("Burst", SampleUnit.Microsecond);
        Measure.Method(() => IL2CPPvsBurstMethods.Vec3Normalize_Burst(ref f3)).SampleGroup(group2).MeasurementCount(1000).Run();

        SampleGroup group3 = new SampleGroup("BurstCustom", SampleUnit.Microsecond);
        Measure.Method(() => IL2CPPvsBurstMethods.Vec3Normalize_BurstCustom(ref f3c)).SampleGroup(group3).MeasurementCount(1000).Run();
    }


    [Test, Performance]
    public void CreateNew_Vector3_vs_Float3_vs_BurstedFloat3()
    {
        var v3 = new Vector3();
        var f3 = new float3();
        var f3b = new float3();

        SampleGroup group1 = new SampleGroup("Vector3New", SampleUnit.Microsecond);
        Measure.Method(() => MathMethods.Vector3New(ref v3)).SampleGroup(group1).MeasurementCount(1000).Run();

        SampleGroup group2 = new SampleGroup("Float3New", SampleUnit.Microsecond);
        Measure.Method(() => MathMethods.Float3New(ref f3)).SampleGroup(group2).MeasurementCount(1000).Run();

        SampleGroup group3 = new SampleGroup("BurstedFloat3New", SampleUnit.Microsecond);
        Measure.Method(() => MathMethods.BurstedFloat3New(ref f3b)).SampleGroup(group3).MeasurementCount(1000).Run();
    }

    [Test, Performance]
    public void BasicIO_StaticBursted_vs_IJob_IJobFor()
    {
        var inArray = new NativeArray<float>(1_000_000, Allocator.TempJob);
        var outArray = new NativeArray<float>(1_000_000, Allocator.TempJob);

        SampleGroup group = new SampleGroup("BurstedBasicIOJob", SampleUnit.Microsecond);
        Measure.Method(() => BurstMethods.BurstedBasicIO(ref inArray, ref outArray))
            .SampleGroup(group).MeasurementCount(1000).Run();

        SampleGroup group1 = new SampleGroup("BurstedBasicIOJob", SampleUnit.Microsecond);
        Measure.Method(() => JobSystemMethods.BasicIOJob(ref inArray, ref outArray))
            .SampleGroup(group1).MeasurementCount(1000).Run();

        SampleGroup group2 = new SampleGroup("BurstedBasicIOJobParallel", SampleUnit.Microsecond);
        Measure.Method(() => JobSystemMethods.BasicIOJobParallel(ref inArray, ref outArray))
            .SampleGroup(group2).MeasurementCount(1000).Run();

        inArray.Dispose();
        outArray.Dispose();
    }

    [Test, Performance]
    public void Float_vs_FloatPacked4_Mul()
    {
        var floats = new NativeArray<float>(1_000_000, Allocator.Temp);
        var floatsToPacked = new NativeArray<float>(1_000_000, Allocator.Temp);
        var floatsToPackedTwo = new NativeArray<float>(1_000_000, Allocator.Temp);
        RandomFiller.FillArrayFloat(ref floats);
        floatsToPacked.CopyFrom(floats);
        floatsToPackedTwo.CopyFrom(floats);

        var packedFloats = floatsToPacked.Reinterpret<float4>(sizeof(float));
        var packedFloats2 = floatsToPackedTwo.Reinterpret<float4>(sizeof(float));


        SampleGroup standard = new SampleGroup("Float_Array_Mul", SampleUnit.Microsecond);
        Measure.Method(() => MathMethods.Float_Array_Mul(ref floats, 36.247f))
            .SampleGroup(standard).MeasurementCount(1000).Run();

        SampleGroup packed = new SampleGroup("Float4_Array_Mul", SampleUnit.Microsecond);
        Measure.Method(() => MathMethods.Float4_Array_Mul(ref packedFloats, 36.247f))
            .SampleGroup(packed).MeasurementCount(1000).Run();
        
        SampleGroup packedTwo = new SampleGroup("Float4_Array_Mul_TwoPerIteration", SampleUnit.Microsecond);
        Measure.Method(() => MathMethods.Float4_Array_Mul_TwoPerIteration(ref packedFloats2, 36.247f))
            .SampleGroup(packedTwo).MeasurementCount(1000).Run();

        bool packedCorrect = true;
        bool packedTwoCorrect = true;

        for (int i = 0; i < floats.Length; i++)
        {
            if (floats[i] != floatsToPacked[i])
            {
                packedCorrect = false;
            }
            
            if (floats[i] != floatsToPackedTwo[i])
            {
                packedTwoCorrect = false;
            }
            
            Assert.IsTrue(packedCorrect,"packed not correct");
            Assert.IsTrue(packedTwoCorrect,"packedTwo not correct");
        }
        
    }
    
    [Test, Performance]
    public void Float_vs_Double_Mul()
    {
        var floats = new NativeArray<float>(1_000_000, Allocator.Temp);
        var doubles = new NativeArray<double>(1_000_000, Allocator.Temp);
        RandomFiller.FillArrayFloat(ref floats);
        RandomFiller.FillArrayDouble(ref doubles);

        SampleGroup FloatMul_NonBursted = new SampleGroup("FloatMul_NonBursted", SampleUnit.Microsecond);
        Measure.Method(() => Math_Float_vs_Double.FloatMul_NonBursted(ref floats, 36.247f))
            .SampleGroup(FloatMul_NonBursted).MeasurementCount(1000).Run();

        SampleGroup DoubleMul_NonBursted = new SampleGroup("DoubleMul_NonBursted", SampleUnit.Microsecond);
        Measure.Method(() => Math_Float_vs_Double.DoubleMul_NonBursted(ref doubles, 36.247f))
            .SampleGroup(DoubleMul_NonBursted).MeasurementCount(1000).Run();
        
        SampleGroup FloatMul = new SampleGroup("FloatMul", SampleUnit.Microsecond);
        Measure.Method(() => Math_Float_vs_Double.FloatMul(ref floats, 36.247f))
            .SampleGroup(FloatMul).MeasurementCount(1000).Run();

        SampleGroup DoubleMul = new SampleGroup("DoubleMul", SampleUnit.Microsecond);
        Measure.Method(() => Math_Float_vs_Double.DoubleMul(ref doubles, 36.247f))
            .SampleGroup(DoubleMul).MeasurementCount(1000).Run();


    }
}
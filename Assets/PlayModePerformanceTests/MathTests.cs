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
    public void BasicMathPerf_Default_vs_NewMath_vs_BurstedNewMath()
    {
        var v3 = new Vector3();
        var f3 = new float3();
        var f3b = new float3();
        
        SampleGroup group1 = new SampleGroup("Default", SampleUnit.Microsecond);
        Measure.Method(() => MathMethods.BasicMath_Default(ref v3)).SampleGroup(group1).MeasurementCount(1000).Run();
        
        SampleGroup group2 = new SampleGroup("NewMath", SampleUnit.Microsecond);
        Measure.Method(() => MathMethods.BasicMath_NewMath(ref f3)).SampleGroup(group2).MeasurementCount(1000).Run();

        SampleGroup group3 = new SampleGroup("BurstedNewMath", SampleUnit.Microsecond);
        Measure.Method(() => MathMethods.BasicMath_BurstedNewMath(ref f3b)).SampleGroup(group3).MeasurementCount(1000).Run();
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
        Measure.Method(() => BurstMethods.BurstedBasicIO(ref inArray,ref outArray))
            .SampleGroup(group).MeasurementCount(1000).Run();
        
        SampleGroup group1 = new SampleGroup("BurstedBasicIOJob", SampleUnit.Microsecond);
        Measure.Method(() => JobSystemMethods.BasicIOJob(ref inArray,ref outArray))
            .SampleGroup(group1).MeasurementCount(1000).Run();
        
        SampleGroup group2 = new SampleGroup("BurstedBasicIOJobParallel", SampleUnit.Microsecond);
        Measure.Method(() => JobSystemMethods.BasicIOJobParallel(ref inArray,ref outArray))
            .SampleGroup(group2).MeasurementCount(1000).Run();

        inArray.Dispose();
        outArray.Dispose();

    }

}


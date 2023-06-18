using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Tests_1;
using Unity.Burst;
using Unity.Mathematics;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.TestTools;


public class NewTestScript
{
    [Test, Performance]
    public void Vector3_vs_Float3()
    {
        var v3 = new Vector3();
        var f3 = new float3();
        var f3b = new float3();
        
        SampleGroup group1 = new SampleGroup("Vector3Add", SampleUnit.Microsecond);
        Measure.Method(() => Methods.Vector3Math(ref v3)).SampleGroup(group1).MeasurementCount(1000).Run();
        
        SampleGroup group2 = new SampleGroup("Float3Add", SampleUnit.Microsecond);
        Measure.Method(() => Methods.Float3Math(ref f3)).SampleGroup(group2).MeasurementCount(1000).Run();

        SampleGroup group3 = new SampleGroup("BurstMethods.Float3Add", SampleUnit.Microsecond);
        Measure.Method(() => BurstMethods.Float3Math(ref f3b)).SampleGroup(group3).MeasurementCount(1000).Run();
    }

    [Test, Performance]
    public void InliningTest()
    {
        SampleGroup group1 = new SampleGroup("NoInliningEmpty", SampleUnit.Nanosecond);
        Measure.Method(() => Methods.NotInlined()).SampleGroup(group1).MeasurementCount(1000).Run();

        SampleGroup group2 = new SampleGroup("InliningEmpty", SampleUnit.Nanosecond);
        Measure.Method(() => Methods.NotInlined()).SampleGroup(group2).MeasurementCount(1000).Run();


        SampleGroup group3 = new SampleGroup("BurstMethods.NotInlined", SampleUnit.Nanosecond);
        Measure.Method(() => BurstMethods.NotInlined()).SampleGroup(group3).MeasurementCount(1000).Run();

        SampleGroup group4 = new SampleGroup("BurstMethods.Inlined", SampleUnit.Nanosecond);
        Measure.Method(() => BurstMethods.Inlined()).SampleGroup(group4).MeasurementCount(1000).Run();
    }





    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
//     [UnityTest]
//     public IEnumerator NewTestScriptWithEnumeratorPasses()
//     {
//         // Use the Assert class to test conditions.
//         // Use yield to skip a frame.
//         yield return null;
//     }
}
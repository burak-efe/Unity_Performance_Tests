using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Tests_1;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.TestTools;


public class OtherTests
{



    [Test, Performance]
    public void InliningTest()
    {
        SampleGroup group1 = new SampleGroup("NoInliningEmpty", SampleUnit.Nanosecond);
        Measure.Method(() => InliningMethods.NotInlined()).SampleGroup(group1).MeasurementCount(1000).Run();

        SampleGroup group2 = new SampleGroup("InliningEmpty", SampleUnit.Nanosecond);
        Measure.Method(() => InliningMethods.NotInlined()).SampleGroup(group2).MeasurementCount(1000).Run();


        SampleGroup group3 = new SampleGroup("BurstedNotInlined", SampleUnit.Nanosecond);
        Measure.Method(() => InliningMethods.BurstedNotInlined()).SampleGroup(group3).MeasurementCount(1000).Run();

        SampleGroup group4 = new SampleGroup("BurstedInlined", SampleUnit.Nanosecond);
        Measure.Method(() => InliningMethods.BurstedInlined()).SampleGroup(group4).MeasurementCount(1000).Run();
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
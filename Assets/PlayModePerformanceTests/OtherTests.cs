using NUnit.Framework;
using Tests_1;
using Unity.PerformanceTesting;


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

    [Test, Performance]
    public void FizzBuzzTest()
    {
        SampleGroup naiveGroup = new SampleGroup("Naive", SampleUnit.Microsecond);
        Measure.Method(() => FizzBuzzer.FizzBuzzNaive(1000)).SampleGroup(naiveGroup).DynamicMeasurementCount().Run();
        
        SampleGroup parallelJobGroup = new SampleGroup("parallelJobGroup", SampleUnit.Microsecond);
        Measure.Method(() => {
            var na =  FizzBuzzer.FizzBuzzParallelTempJobAlloc(1000);
           na.Dispose();
        }).SampleGroup(parallelJobGroup).DynamicMeasurementCount().Run();
    }

    [Test]
    public void FizzBuzzParallelUnit()
    {
        var a = FizzBuzzer.FizzBuzzNaive(1000);
        var na =  FizzBuzzer.FizzBuzzParallelTempJobAlloc(1000);
        for (int i = 0; i < 1000; i++)
        {
            Assert.IsTrue(a[i] == na[i].ToString());
        }
        na.Dispose();
    }
}
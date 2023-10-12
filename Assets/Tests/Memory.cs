using NUnit.Framework;
using Unity.Collections;
using Unity.PerformanceTesting;


namespace Ica.Benchmarks.PerformanceTests
{
    public class Memory
    {
        // [Test, Performance]
        // public void Native_Collection_Allocation_Bursted_vs_NonBursted()
        // {
        //     SampleGroup group1 = new SampleGroup("non bursted temp collection", SampleUnit.Microsecond);
        //     Measure.Method(() => MemoryMethods.AllocTempUninitialized_NonBursted(10_000_000)).SampleGroup(group1).MeasurementCount(100).Run();
        //
        //     SampleGroup group2 = new SampleGroup("bursted temp collection", SampleUnit.Microsecond);
        //     Measure.Method(() => MemoryMethods.AllocTemp(10_000_000)).SampleGroup(group2).MeasurementCount(100).Run();
        // }

        [Test, Performance]
        public void Collection_Allocation_NativeInitialized_vs_NativeUninitialized_vs_Managed()
        {
            SampleGroup group1 = new SampleGroup("Temp native uninitialized", SampleUnit.Microsecond);
            Measure.Method(() => MemoryMethods.AllocTempUninitialized(10_000_000)).SampleGroup(group1).MeasurementCount(100).Run();

            SampleGroup group2 = new SampleGroup("Temp native ", SampleUnit.Microsecond);
            Measure.Method(() => MemoryMethods.AllocTemp(10_000_000)).SampleGroup(group2).MeasurementCount(100).Run();

            SampleGroup tub = new SampleGroup("Temp native uninitialized Bursted", SampleUnit.Microsecond);
            Measure.Method(() => MemoryMethods.AllocTempUninitialized_Bursted(10_000_000)).SampleGroup(tub).MeasurementCount(100).Run();

            SampleGroup tb = new SampleGroup("Temp native Bursted ", SampleUnit.Microsecond);
            Measure.Method(() => MemoryMethods.AllocTemp_Bursted(10_000_000)).SampleGroup(tb).MeasurementCount(100).Run();

            SampleGroup group3 = new SampleGroup("Default Managed", SampleUnit.Microsecond);
            Measure.Method(() => MemoryMethods.AllocManagedBuffer(10_000_000)).SampleGroup(group3).MeasurementCount(100).Run();
        }


        [Test, Performance]
        public void CollectionNestedAllocation_Native_vs_Unsafe_Default_10k()
        {
            int sampleSize = 10_000;
            int measurementCount = 50;
            SampleUnit sampleUnit = SampleUnit.Microsecond;

            SampleGroup managed = new SampleGroup("Default C#", sampleUnit);
            Measure.Method(() => MemoryMethods.AllocNestedManagedList(sampleSize)).SampleGroup(managed).MeasurementCount(measurementCount).Run();

            // SampleGroup group1 = new SampleGroup("Native", sampleUnit);
            // Measure.Method(() => MemoryMethods.AllocNestedNativeList(sampleSize, Allocator.Temp)).SampleGroup(group1).MeasurementCount(measurementCount).Run();
            //
            // SampleGroup group2 = new SampleGroup("Unsafe ", sampleUnit);
            // Measure.Method(() => MemoryMethods.AllocNestedUnsafeList(sampleSize, Allocator.Temp)).SampleGroup(group2).MeasurementCount(measurementCount).Run();
            //
            // SampleGroup group3 = new SampleGroup("NativeList Bursted ", sampleUnit);
            // Measure.Method(() => MemoryMethods.AllocNestedNativeListBursted(sampleSize, Allocator.TempJob)).SampleGroup(group3).MeasurementCount(measurementCount).Run();
            //
            // SampleGroup group4 = new SampleGroup("Unsafe Bursted ", sampleUnit);
            // Measure.Method(() => MemoryMethods.AllocNestedUnsafeListBursted(sampleSize, Allocator.Temp)).SampleGroup(group4).MeasurementCount(measurementCount).Run();
            //
            // SampleGroup group5 = new SampleGroup("Unsafe Bursted Array", sampleUnit);
            // Measure.Method(() => MemoryMethods.AllocNestedUnsafeArrayBursted(sampleSize, Allocator.Temp)).SampleGroup(group5).MeasurementCount(measurementCount).Run();
            //
            // var rew = new RewStruct(sampleSize * sizeof(int) * 51);
            // SampleGroup group6 = new SampleGroup("List Rewindable Allocator", sampleUnit);
            // Measure.Method(() => { MemoryMethods.AllocNestedNativeListRew(sampleSize, ref rew); }).SampleGroup(group6).MeasurementCount(measurementCount).Run();
            // rew.FreeRewindableAllocator();
            // rew.Dispose();
            //
            // SampleGroup group7 = new SampleGroup("IJobFor NativeList", sampleUnit);
            // Measure.Method(() => MemoryMethods.AllocNestedNativeParallelJob(sampleSize)).SampleGroup(group7).MeasurementCount(measurementCount).Run();
        }


        [Test, Performance]
        public void BasicRW_StaticBursted_vs_IJob_IJobFor()
        {
            int sampleSize = 10_000;
            int measurementCount = 100;
            SampleUnit sampleUnit = SampleUnit.Microsecond;


            var inNativeArray = new NativeArray<float>(sampleSize, Allocator.TempJob);
            var outNativeArray = new NativeArray<float>(sampleSize, Allocator.TempJob);

            var inArray = new float[sampleSize];
            var outArray = new float[sampleSize];

            SampleGroup defaultGroup = new SampleGroup("Basic RW Default", sampleUnit);
            Measure.Method(() => MemoryMethods.BasicRW(ref inArray, ref outArray))
                .SampleGroup(defaultGroup).MeasurementCount(measurementCount).Run();


            SampleGroup group = new SampleGroup("Basic RW Bursted Method", sampleUnit);
            Measure.Method(() => MemoryMethods.BasicRW_Bursted(ref inNativeArray, ref outNativeArray))
                .SampleGroup(group).MeasurementCount(measurementCount).Run();

            SampleGroup group3 = new SampleGroup("Basic RW Bursted Method with NoAlias Attribute", sampleUnit);
            Measure.Method(() => MemoryMethods.BasicRW_Bursted_NoAlias(ref inNativeArray, ref outNativeArray))
                .SampleGroup(group3).MeasurementCount(measurementCount).Run();

            SampleGroup group1 = new SampleGroup("Basic RW Bursted IJob", sampleUnit);
            Measure.Method(() => JobSystemMethods.BasicIOJob(ref inNativeArray, ref outNativeArray))
                .SampleGroup(group1).MeasurementCount(measurementCount).Run();

            SampleGroup group2 = new SampleGroup("Basic RW Bursted IJobFor", sampleUnit);
            Measure.Method(() => JobSystemMethods.BasicIOJobParallel(ref inNativeArray, ref outNativeArray))
                .SampleGroup(group2).MeasurementCount(measurementCount).Run();


            inNativeArray.Dispose();
            outNativeArray.Dispose();
        }
    }
}
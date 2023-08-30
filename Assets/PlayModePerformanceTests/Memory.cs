using System.Collections;
using NUnit.Framework;
using Tests_1;
using Unity.Collections;
using Unity.PerformanceTesting;
using UnityEngine.TestTools;

namespace PlayModePerformanceTests
{
    public class Memory
    {
        [Test,Performance]
        public void Native_Collection_Allocation_Bursted_vs_NonBursted()
        {
            SampleGroup group1 = new SampleGroup("non bursted persistent collection", SampleUnit.Microsecond);
            Measure.Method( () => MemoryMethods.AllocAndDeallocNativeNonBurst(10_000_000,Allocator.Persistent)).SampleGroup(group1).MeasurementCount(100).Run();
            
            SampleGroup group2 = new SampleGroup("bursted persistent collection", SampleUnit.Microsecond);
            Measure.Method( () => MemoryMethods.AllocAndDeallocNative(10_000_000,Allocator.Persistent)).SampleGroup(group2).MeasurementCount(100).Run();

        }
        
        [Test,Performance]
        public void Collection_Allocation_NativeInitialized_vs_NativeUninitialized_vs_Managed()
        {
            SampleGroup group1 = new SampleGroup("persistent native uninitialized", SampleUnit.Microsecond);
            Measure.Method( () => MemoryMethods.AllocAndDeallocNativeUninitialized(10_000_000,Allocator.Persistent)).SampleGroup(group1).MeasurementCount(100).Run();
            
            SampleGroup group2 = new SampleGroup("persistent native ", SampleUnit.Microsecond);
            Measure.Method( () => MemoryMethods.AllocAndDeallocNative(10_000_000,Allocator.Persistent)).SampleGroup(group2).MeasurementCount(100).Run();
            
            SampleGroup group3 = new SampleGroup("managed", SampleUnit.Microsecond);
            Measure.Method( () => MemoryMethods.AllocManaged(10_000_000)).SampleGroup(group3).MeasurementCount(100).Run();

        }


    }
}
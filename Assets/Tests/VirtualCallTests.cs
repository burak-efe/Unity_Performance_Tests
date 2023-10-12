using NUnit.Framework;
using Unity.Collections;
using Unity.PerformanceTesting;

namespace Ica.Benchmarks.PerformanceTests
{
    public class VirtualCallTests
    {
        [Test, Performance]
        public void Access_Class_vs_Struct_Multiple()
        {
            int sampleSizeCount = 10_000;
            int measurementCount = 100;
            SampleUnit sampleUnit = SampleUnit.Microsecond;


            MoveClass[] classRef = new MoveClass[sampleSizeCount];
            IMove[] classInterfaceRef = new IMove[sampleSizeCount];

            MoveStruct[] structs = new MoveStruct[sampleSizeCount];
            IMove[] structInterfaceRef = new IMove[sampleSizeCount];

            NativeArray<MoveStruct> structsNative = new NativeArray<MoveStruct>(sampleSizeCount, Allocator.Temp);

            for (int i = 0; i < sampleSizeCount; i++)
            {
                classRef[i] = new MoveClass();
                classInterfaceRef[i] = new MoveClass();

                structs[i] = new MoveStruct();
                structInterfaceRef[i] = new MoveStruct();

                structsNative[i] = new MoveStruct();
            }


            SampleGroup group1 = new SampleGroup("call class ", sampleUnit);
            Measure.Method(() => VirtualDispatch.MoveAllClasses(ref classRef)
            ).SampleGroup(group1).MeasurementCount(measurementCount).Run();

            SampleGroup group2 = new SampleGroup("call class from interface", sampleUnit);
            Measure.Method(() => VirtualDispatch.MoveAllInterfaces(ref classInterfaceRef)
            ).SampleGroup(group2).MeasurementCount(measurementCount).Run();


            SampleGroup group3 = new SampleGroup("call struct", sampleUnit);
            Measure.Method(() => VirtualDispatch.MoveAllStructs(ref structs)
            ).SampleGroup(group3).MeasurementCount(measurementCount).Run();

            SampleGroup group4 = new SampleGroup("call struct from interface", sampleUnit);
            Measure.Method(() => VirtualDispatch.MoveAllInterfaces(ref structInterfaceRef)
            ).SampleGroup(group4).MeasurementCount(measurementCount).Run();


            SampleGroup group5 = new SampleGroup("call struct bursted", sampleUnit);
            Measure.Method(() => VirtualDispatch.MoveAllStructsBursted(ref structsNative)
            ).SampleGroup(group5).MeasurementCount(measurementCount).Run();
        }
    }
}
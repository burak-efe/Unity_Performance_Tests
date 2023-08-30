using System.Collections.Generic;
using NUnit.Framework;
using Tests_1;
using Unity.Collections;
using Unity.PerformanceTesting;

namespace PlayModePerformanceTests
{
    public class VirtualCallTests
    {
        [Test,Performance]
        public void CostOfVirtualDispatch()
        {
            IMove classInterface = new MoveClass();
            IMove classInterfaceSealed = new MoveClassSealed();
            var classNoInheritance = new MoveClassNonInterface();
            var classNoInheritanceSealed = new MoveClassSealedNonInterface();
            
            SampleGroup group1 = new SampleGroup("call class method from interface", SampleUnit.Nanosecond);
            Measure.Method( () => classInterface.Move(5,5)
            ).SampleGroup(group1).MeasurementCount(1000).Run();
            
            SampleGroup group2 = new SampleGroup("call sealed class method from interface", SampleUnit.Nanosecond);
            Measure.Method( () => classInterfaceSealed.Move(5,5)
            ).SampleGroup(group2).MeasurementCount(1000).Run();
            
            SampleGroup group3 = new SampleGroup("call class method", SampleUnit.Nanosecond);
            Measure.Method( () => classNoInheritance.Move(5,5)
            ).SampleGroup(group3).MeasurementCount(1000).Run();
            
            SampleGroup group4 = new SampleGroup("call sealed class method", SampleUnit.Nanosecond);
            Measure.Method( () => classNoInheritanceSealed.Move(5,5)
            ).SampleGroup(group4).MeasurementCount(1000).Run();

        }
        
    }
}
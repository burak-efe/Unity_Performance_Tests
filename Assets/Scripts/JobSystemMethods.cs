using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Ica.Benchmarks
{
    public static class JobSystemMethods
    {
        public static void BasicIOJob(ref NativeArray<float> inArray, ref NativeArray<float> outArray)
        {
            var job = new BasicIO
            {
                In = inArray,
                Out = outArray
            };
            job.Run();
        }
    

        public static void BasicIOJobParallel(ref NativeArray<float> inArray, ref NativeArray<float> outArray)
        {
            var job = new BasicIOParallel
            {
                In = inArray,
                Out = outArray
            };
        
            var handle = job.ScheduleParallel(inArray.Length, inArray.Length / 64, default);
            handle.Complete();
        }

    
        [BurstCompile]
        public struct BasicIO : IJob
        {
            [ReadOnly] public NativeArray<float> In;
            [WriteOnly] public NativeArray<float> Out;
            public void Execute()
            {
                for (int i = 0; i < In.Length; i++)
                {
                    Out[i] = In[i];
                }
            }
        }
    
    
        [BurstCompile]
        public struct BasicIOParallel : IJobFor
        {
            [ReadOnly] public NativeArray<float> In;
            [WriteOnly] public NativeArray<float> Out;

            public void Execute(int i)
            {
                Out[i] = In[i];
            }
        }
    }
}
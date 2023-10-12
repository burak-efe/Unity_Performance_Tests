using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Ica.Benchmarks
{
    [BurstCompile]
    public static class MemoryMethods
    {
        public static void AllocManagedBuffer(int size)
        {
            var data = new byte[size];
        }


        public static void AllocTemp(int size)
        {
            var data = new NativeArray<byte>(size, Allocator.Temp);
        }


        public static void AllocTempUninitialized(int size)
        {
            var data = new NativeArray<byte>(size, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
        }
        
        [BurstCompile]
        public static void AllocTemp_Bursted(int size)
        {
            var data = new NativeArray<byte>(size, Allocator.Temp);
        }

         [BurstCompile]
        public static void AllocTempUninitialized_Bursted(int size)
        {
            var data = new NativeArray<byte>(size, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
        }
        
        public static void AllocNestedManagedList(int count)
        {
            var l = new List<List<int>>(1);

            for (int i = 0; i < count; i++)
            {
                l.Add(new List<int>(1));
            }
        }
        

        

        public static void AllocNestedNativeList(int count, Allocator allocator)
        {
            var l = new NativeList<NativeList<int>>(1, allocator);

            for (int i = 0; i < count; i++)
            {
                l.Add(new NativeList<int>(1, allocator));
            }
        }

        [BurstCompile]
        public static void AllocNestedNativeListBursted(int count, Allocator allocator)
        {
            var l = new NativeList<NativeList<int>>(count, allocator);

            for (int i = 0; i < count; i++)
            {
                l.Add(new NativeList<int>(1, allocator));
            }
        }

        public static void AllocNestedUnsafeList(int count, Allocator allocator)
        {
            var l = new NativeList<NativeList<int>>(1, allocator);

            for (int i = 0; i < count; i++)
            {
                l.Add(new NativeList<int>(1, allocator));
            }
        }

        [BurstCompile]
        public static void AllocNestedUnsafeListBursted(int count, Allocator allocator)
        {
            var l = new NativeList<NativeList<int>>(1, allocator);

            for (int i = 0; i < count; i++)
            {
                l.Add(new NativeList<int>(1, allocator));
            }
        }

        [BurstCompile]
        public static void AllocNestedUnsafeArrayBursted(int count, Allocator allocator)
        {
            var l = new NativeList<NativeArray<int>>(1, allocator);

            for (int i = 0; i < count; i++)
            {
                l.Add(new NativeArray<int>(count, allocator, NativeArrayOptions.UninitializedMemory));
            }

            for (int i = 0; i < count; i++)
            {
                l[i].Dispose();
            }

            l.Dispose();
        }

        [BurstCompile]
        public static void AllocNestedNativeListRew(int count, ref RewStruct rew)
        {
            rew.FreeRewindableAllocator();
            var l = new NativeList<NativeList<int>>(count, rew.RwdAllocator.Handle);
            for (int i = 0; i < count; i++)
            {
                l.Add(new NativeList<int>(1, rew.RwdAllocator.Handle));
            }
        }

        [BurstCompile]
        public static void AllocNestedNativeParallelJob(int count)
        {
            var l = new UnsafeList<NativeList<int>>(count, Allocator.TempJob);

            var job = new AllocNestedJob()
            {
                Nested = l.AsParallelWriter(),
            };

            job.ScheduleParallel(count, count / 128 + 1, default).Complete();
        }

        [BurstCompile]
        private struct AllocNestedJob : IJobFor
        {
            public UnsafeList<NativeList<int>>.ParallelWriter Nested;

            public void Execute(int index)
            {
                Nested.AddNoResize(new NativeList<int>(1, Allocator.TempJob));
            }
        }


        [BurstCompile]
        public static void BasicRW_Bursted(ref NativeArray<float> inArray, ref NativeArray<float> outArray)
        {
            for (int i = 0; i < inArray.Length; i++)
            {
                outArray[i] = inArray[i];
            }
        }

        [BurstCompile]
        public static void BasicRW_Bursted_NoAlias([NoAlias] ref NativeArray<float> inArray, [NoAlias] ref NativeArray<float> outArray)
        {
            for (int i = 0; i < inArray.Length; i++)
            {
                outArray[i] = inArray[i];
            }
        }


        public static void BasicRW(ref float[] inArray, ref float[] outArray)
        {
            for (int i = 0; i < inArray.Length; i++)
            {
                outArray[i] = inArray[i];
            }
        }
    }
}
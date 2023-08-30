using Unity.Burst;
using Unity.Collections;

namespace Tests_1
{
    [BurstCompile]
    public static class MemoryMethods
    {
        public static void AllocManaged(int size)
        {
            var data = new byte[size];
        }
        [BurstCompile]
        public static void AllocAndDeallocNative(int size, Allocator allocator)
        {
            var data = new NativeArray<byte>(size, allocator);
            data.Dispose();
        }

        [BurstCompile]
        public static void AllocAndDeallocNativeUninitialized(int size, Allocator allocator)
        {
            var data = new NativeArray<byte>(size, allocator, NativeArrayOptions.UninitializedMemory);
            data.Dispose();
        }
        
        

        public static void AllocAndDeallocNativeNonBurst(int size, Allocator allocator)
        {
            var data = new NativeArray<byte>(size, allocator);
            data.Dispose();
        }
    }
}
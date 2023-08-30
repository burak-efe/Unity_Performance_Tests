using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Tests_1
{
    [BurstCompile]
    public class FizzBuzzer
    {
        public static string[] FizzBuzzNaive(int count)
        {
            var outArray = new string[count];

            for (int i = 0; i < count; i++)
            {
                if (i % 3 == 0)
                {
                    if (i % 5 == 0)
                    {
                        outArray[i] = "FizzBuzz";
                    }
                    else
                    {
                        outArray[i] = "Fizz";
                    }
                }
                else if (i % 5 == 0)
                {
                    outArray[i] = "Buzz";
                }
                else
                {
                    outArray[i] = i.ToString();
                }
            }

            return outArray;
        }

        public static NativeArray<FixedString32Bytes> FizzBuzzParallelTempJobAlloc(int count)
        {
            var outArray = new NativeArray<FixedString32Bytes>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);

            var job = new FizzBuzzParallelJob
            {
                FizzString = new FixedString32Bytes("Fizz"),
                BuzzString = new FixedString32Bytes("Buzz"),
                FizzBuzzString = new FixedString32Bytes("FizzBuzz"),
                OutData = outArray
            };

            job.ScheduleParallel(count, count / 128 + 1, default).Complete();
            return outArray;
        }

        [BurstCompile(OptimizeFor = OptimizeFor.Performance)]
        private struct FizzBuzzParallelJob : IJobFor
        {
            [WriteOnly] public NativeArray<FixedString32Bytes> OutData;
            [ReadOnly] public FixedString32Bytes FizzString;
            [ReadOnly] public FixedString32Bytes BuzzString;
            [ReadOnly] public FixedString32Bytes FizzBuzzString;

            public void Execute(int index)
            {
                if (index % 3 == 0)
                {
                    if (index % 5 == 0)
                    {
                        OutData[index] = FizzBuzzString;
                    }
                    else
                    {
                        OutData[index] = FizzString;
                    }
                }
                else if (index % 5 == 0)
                {
                    OutData[index] = BuzzString;
                }
                else
                {
                    var str = new FixedString32Bytes();
                    str.Append(index);
                    OutData[index] = str;
                }
            }
        }
    }
}
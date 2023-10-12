
⏱Various performance benchmarks on Unity Engine for finding most performant solutions and validating existing assumptions.

I list some results that interest me.

📙Quick Glossary:

Mono : Default Unity backend for Editor and Runtime. It is very old badly optimized and unity working on new backend to replace it. It's a JIT compiler and compiles fast.

IL2CPP : This backend created for support AOT platform, Most of the time faster than mono, but its very slow to compile. It is runtime only.

Burst: Latest and most optimized compiler for Unity code. Uses LLVM which well known open source compiler backend for low-level languages. Burst is much more limited (or focused?) and only works with small subset of C# called HPC#. Also compiles code very slow. Works on Runtime and Editor.

Job System: Modern multithreading solution for game engines, also exist in Unity Engine. When combined with burst it can makes thing super fast.

Native Collections: Collections that allocate directly with underlying c++ engine(unmanaged) instead of c# layer(managed).
These are should be used when compiling with Burst. These are not garbage collected and should be freed by the programmer.

1_000µs = 1ms
1_000_000ns  = 1ms


(These results from PC platform, build, avx2 enabled, burst max performance)
### ✨ On Mono, float slower than double
~200 add operation on single value
|    ns  | Mono| IL2Cpp |Burst|
|--------|-----|--------|-----|
| Float  | 800 |    300    | 200 |
| Double | 600 |    300    | 200 |

~40k basic math ops on single value
|    µs  | Mono| IL2Cpp |Burst|
|--------|-----|--------|-----|
| Float  | 193 |    46    | 45  |
| Double | 136 |   52     | 52  |

Multiply operation on array 10k
|    µs  | Mono| IL2Cpp |Burst|
|--------|-----|--------|-----|
| Float  | 146|   3.5     | 0.4  |
| Double | 11|    3.7    | 0.7  |

Reason: On mono floats converted to doubles under the hood, then converted back to float. Yes this is "suboptimal" 💀
And don't ask what is happening at array of floats on mono, idk either.


### ✨Distance vs Square Distance
Distance (length) roughly 2x slower than Squared Distance on all backends.

|    µs  | Mono| IL2Cpp |Burst|
|--------|-----|--------|-----|
| Vector Distance| 65|   27.7     | x|
| Vector Squared Distance | 36|   13.4     | x|
| float3 Distance| x|    x    | 11.8|
| float3 Squared Distance | x|   x     | 5.7|

Reason: Squared version uses less cpu cycle. 🤓

### ✨Structs vs Classes not matter much on Mono and IL2CPP
Very small struct/class mutate itself with method.

|    µs  | Mono| IL2Cpp |Burst|
|--------|-----|--------|-----|
| Struct*| 133 |    115    | 0.1  |
| Struct from Interface | 150 |   139     | x |
| Class| 143 |  120      | x  |
| Class from Interface| 148 |    140    | x  |
*Array of Struct

Reason: This was very simple data structure, probably gap will be widen if more dynamic dispatch introduced to example.
note:  On  .Net 7 both struct and class results are was 7µs .

### ✨ Parallel Jobs Have Overhead
copy to one array to another naively
|    µs  | Bursted Method| Bursted IJob|Bursted IJobFor|
|--------|-----|--------|-----|
| Float  | 0.8|      1  | 4.9  |

Reason: Job System aims to be safe as possible to avoid any false sharing or data corruption. To do that it copies all data for each used thread. So there is a overhead that scales with used thread count and data in the job struct. While this overhead very small for majority of operations, it can be make multithreading slower when data is large and ALU ops count is low (Like this example).

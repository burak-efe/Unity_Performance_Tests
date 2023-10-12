using Unity.Burst;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ica.Benchmarks
{
    [BurstCompile]
    public static class VirtualDispatch
    {
        public static void MoveAllInterfaces(ref IMove[] movables)
        {
            for (int i = 0; i < movables.Length; i++)
            {
                movables[i].Move(19, 19);
            }
        }

        public static void MoveAllClasses(ref MoveClass[] movables)
        {
            for (int i = 0; i < movables.Length; i++)
            {
                movables[i].Move(19, 19);
            }
        }


        public static void MoveAllStructs(ref MoveStruct[] movables)
        {
            for (int i = 0; i < movables.Length; i++)
            {
                movables[i].Move(19, 19);
            }
        }

        [BurstCompile]
        public static void MoveAllStructsBursted(ref NativeArray<MoveStruct> movables)
        {
            for (int i = 0; i < movables.Length; i++)
            {
                movables[i].Move(19, 19);
            }
        }
    }

    public interface IMove
    {
        void Move(int x, int y);
    }

    public class MoveClass : IMove
    {
        public Vector4 SomeOtherData;
        public Vector3Int Position;
        public Vector4 SomeData;

        public void Move(int x, int y)
        {
            Position *= x;
            Position /= y;
        }
    }

    public sealed class MoveClassSealed : IMove
    {
        public Vector4 SomeOtherData;
        public Vector3Int Position;
        public Vector4 SomeData;

        public void Move(int x, int y)
        {
            Position *= x;
            Position /= y;
        }
    }

    public class MoveClassNonInterface
    {
        public Vector4 SomeOtherData;
        public Vector3Int Position;
        public Vector4 SomeData;

        public void Move(int x, int y)
        {
            Position *= x;
            Position /= y;
        }
    }

    public sealed class MoveClassSealedNonInterface
    {
        public Vector4 SomeOtherData;
        public Vector3Int Position;
        public Vector4 SomeData;

        public void Move(int x, int y)
        {
            Position *= x;
            Position /= y;
        }
    }

    public struct MoveStruct : IMove
    {
        public Vector4 SomeOtherData;
        public Vector3Int Position;
        public Vector4 SomeData;

        public void Move(int x, int y)
        {
            Position *= x;
            Position /= y;
        }
    }
}
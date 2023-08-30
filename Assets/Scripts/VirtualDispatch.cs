using UnityEngine;
using UnityEngine.UIElements;

namespace Tests_1
{
    public interface IMove
    {
        void Move(int x,int y);
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
    
    
    
}
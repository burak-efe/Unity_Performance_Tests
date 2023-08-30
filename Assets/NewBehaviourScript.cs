using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {
        var na = new NativeList<int>(100, Allocator.Temp);
        na.Resize(100,NativeArrayOptions.UninitializedMemory);
        Debug.Log(na.AsArray().Length);
        

    }

}

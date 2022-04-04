using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnCharacter : MonoBehaviour
{
    public void Spawn(GameObject prefab)
    {
        ObjectPool.Spawn(prefab);
    }
}

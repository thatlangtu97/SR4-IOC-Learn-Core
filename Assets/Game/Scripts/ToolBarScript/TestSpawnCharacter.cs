using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnCharacter : MonoBehaviour
{
    public bool SetPlayer;
    public void Spawn(GameObject prefab)
    {
        StateMachineController temp = ObjectPool.Spawn(prefab).GetComponent<StateMachineController>();
        temp.gameObject.SetActive(true);
        if (SetPlayer)
        {
            GameUIController.instance.stateMachine = temp;
            GameUIController.instance.MODIFY();
        }
    }

    public void SpawnMap(GameObject prefab)
    {
        ObjectPool.Spawn(prefab);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnCharacter : MonoBehaviour
{
    public bool SetPlayer;
    public void Spawn(GameObject prefab)
    {
        StateMachineController temp = null;
        int index = 0;
        while (index<count)
        {
            temp = ObjectPool.Spawn(prefab).GetComponent<StateMachineController>();
            temp.gameObject.SetActive(true);
            index += 1;
        } 
        
        
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

    public int count;
    public void TestSpawnTextMesh(Transform transform)
    {
        int index = 0;
        while (index<count)
        {
            DamageTextManager.AddReactiveComponent(DamageTextType.Normal,10.ToString(),transform.position + new Vector3(Random.Range(-.5f,.5f),Random.Range(1.5f,2f),0f));
            index += 1;
        } 
        
    }
}

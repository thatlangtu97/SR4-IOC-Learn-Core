using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    
    public void SpawnEnemy(GameObject prefab)
    {
        int countSpawn = 0;
        float timeDelay = 1f;
        while (countSpawn< count )
        {
            Vector3 randomPos = new Vector3(Random.Range(-13f,5f),0f,0f);
            Action action = delegate 
            {
                StateMachineController temp = ObjectPool.Spawn(prefab,null,randomPos).GetComponent<StateMachineController>();
                temp.gameObject.SetActive(true);  
            };
            ActionBufferManager.Instance.ActionDelayTime(action,timeDelay*countSpawn);
            //timeDelay *= countSpawn;
            countSpawn += 1;
        }
    }
    private int fps = -1;
    public void SetFps()
    {
        if (fps == -1)
        {
            Application.targetFrameRate = 70;
            fps = 70;
        }
        else
        {
            Application.targetFrameRate = -1;
            fps = -1;
        }
    }
}

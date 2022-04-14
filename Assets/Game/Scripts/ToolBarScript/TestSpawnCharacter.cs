﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class TestSpawnCharacter : MonoBehaviour
{
    public bool SetPlayer;
    public float TimeDelay;
    public void Spawn(GameObject prefab)
    {
        StateMachineController temp = null;
        int index = 0;
        while (index<count)
        {
//            //temp = ObjectPool.Spawn(prefab).GetComponent<StateMachineController>();
//            temp = PoolManager.Spawn(prefab.GetComponent<PoolItem>()).GetComponent<StateMachineController>();
//            //temp.GetComponent<ComponentManager>().SetupEntity();
//            
//            
//            //ActionBufferManager.Instance.ActionDelayTime(() => { temp.GetComponent<ComponentManager>().SetupEntity();}, 2f);
//            
//            //temp.gameObject.SetActive(true);
//            Action tempAction = delegate {  temp.GetComponent<ComponentManager>().SetupEntity(); };
//            setActionDelay(tempAction, TimeDelay);
            
            
            temp = PoolManager.Spawn(prefab.GetComponent<PoolItem>()).GetComponent<StateMachineController>();
            Action tempAction = delegate {  temp.GetComponent<ComponentManager>().SetupEntity(); };
            setActionDelay(tempAction, TimeDelay);
            
            index += 1;
        } 
        
        
        if (SetPlayer)
        {
            Action tempAction = delegate {  
                GameUIController.instance.stateMachine = temp;
                GameUIController.instance.MODIFY(); 
            };
            
            setActionDelay(tempAction, TimeDelay);
//            ActionBufferManager.Instance.ActionDelayTime(() =>
//            {
//                GameUIController.instance.stateMachine = temp;
//                GameUIController.instance.MODIFY();
//                
//            }, 2.1f);
            
        }
    }

    public void SpawnMap(GameObject prefab)
    {
       // ObjectPool.Spawn(prefab);
        
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
        while (countSpawn < count )
        {
            Vector3 randomPos = new Vector3(Random.Range(-13f,5f),0f,0f);
            StateMachineController temp = PoolManager.Spawn(prefab.GetComponent<PoolItem>(),null,randomPos).GetComponent<StateMachineController>();
            Action tempAction = delegate
            {
                temp.GetComponent<ComponentManager>().SetupEntity();
            };
            setActionDelay(tempAction, timeDelay*countSpawn);
            countSpawn += 1;
        }
    }
    
    public void SpawnStateMachine(Object prefab)
    {
        int countSpawn = 1;
        while (countSpawn < count + 1)
        {
            Object temp = Instantiate(prefab);
            if (temp.GetType() == typeof(StateMachineCollection))
            {
                StateMachineCollection convert = temp as StateMachineCollection;
                convert.Create();
            }
            countSpawn += 1;
        }
    }
    public void SpawnEffect(PoolItem prefab)
    {
        PoolItem temp = PoolManager.Spawn(prefab);
        PoolManager.Recycle(temp,1f);
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

    private void Update()
    {
        if (actions.Count > 0)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                if (actions[i].CanInvoke)
                {
                    actions[i].Invoke();
                }
                else
                {
                    actions[i].DownTime(Time.deltaTime);
                }
            }
            
            for (int i = 0; i < actions.Count; i++)
            {
                if (actions[i].CanRemove)
                {
                    actions.Remove(actions[i]);
                    break;
                }
            }
//            
//            foreach (var action in actions)
//            {
//                if (action.CanInvoke)
//                {
//                    action.Invoke();
//                }
//                else
//                {
//                    action.DownTime(Time.deltaTime);
//                }
//            }
//
//            foreach (var action in actions)
//            {
//                if (action.CanRemove)
//                {
//                    actions.Remove(action);
//                    break;
//                }
//            }
        }
    }

    public List<DataAction> actions= new List<DataAction>();
    public void setActionDelay(Action actionNew, float time)
    {
        if (actions == null)
        {
            actions= new List<DataAction>();
        }
        actions.Add(new DataAction(){action = actionNew,timeDelay = time,isInvoke = false});
    }
    [System.Serializable]
    public class DataAction
    {
        public Action action;
        public float timeDelay;
        public bool isInvoke;

        public bool CanInvoke
        {
            get { return isInvoke == false && timeDelay < 0; }
        }
        public bool CanRemove
        {
            get { return isInvoke == true && timeDelay < 0; }
        }
        public void DownTime(float Time)
        {
            timeDelay -= Time;
        }

        public void Invoke()
        {
            action.Invoke();
            isInvoke = true;
        }
    }
}

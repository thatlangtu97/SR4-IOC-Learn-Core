using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class PoolItemStateMachine : PoolItem
{
    public StateMachineController stateMachine;
    public BehaviorTree bt;
    public override void Create()
    {
        //stateMachine.SetupState();
        gameObject.SetActive(true);
        transform.position = Vector3.left * 10000f;
        
        if (bt != null)
        {
            ActionBufferManager.Instance.ActionDelayTime(() => { bt.DisableBehavior(); }, .1f);
        }
        if(stateMachine)
        stateMachine.DisableRigibody();
    }

    public override void Spawn()
    {
        gameObject.SetActive(true);
        if(stateMachine)
        stateMachine.EnableRigibody();
    }

    public override void Recycle()
    {
        gameObject.SetActive(true);
        transform.position = Vector3.left * 10000f;
        if(stateMachine)
        stateMachine.DisableRigibody();
    }
}

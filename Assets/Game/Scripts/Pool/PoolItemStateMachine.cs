using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItemStateMachine : PoolItem
{
    //public StateMachineController stateMachine;
    public override void Create()
    {
        //stateMachine.SetupState();
        gameObject.SetActive(false);
    }

    public override void Spawn()
    {
        gameObject.SetActive(true);
    }

    public override void Recycle()
    {
        gameObject.SetActive(false);
    }
}

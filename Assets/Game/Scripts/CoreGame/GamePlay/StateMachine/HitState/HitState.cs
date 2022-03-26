﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HitState", menuName = "CoreGame/State/HitState")]
public class HitState : State
{
    public override void EnterState()
    {
        base.EnterState();
        if (entity.hasBehaviourTree)
        {
            entity.behaviourTree.value.DisableBehavior();
        }
        if (eventCollectionData.Count!=0)
        idState = (idState + 1) % eventCollectionData.Count;
        controller.SetTrigger(eventCollectionData[idState].NameTrigger,eventCollectionData[idState].typeAnim,eventCollectionData[idState].timeStart);
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (timeTrigger > eventCollectionData[idState].durationAnimation)
        {
            if (controller.componentManager.checkGround())
            {
                controller.ChangeState(NameState.IdleState);
            }
//            else
//            {
//                controller.ChangeState(NameState.FallingState);
//            }
        }
        if (!controller.componentManager.checkGround())
        {
            controller.ChangeState(NameState.FallingState);
        }
        
    }
    public override void ExitState()
    {
        base.ExitState();
        if (entity.hasBehaviourTree)
        {
            entity.behaviourTree.value.EnableBehavior();
        }
    }
    public override void OnHit()
    {
        base.OnHit();
        EnterState();
    }
}

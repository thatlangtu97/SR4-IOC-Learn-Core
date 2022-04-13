﻿using UnityEngine;
[CreateAssetMenu(fileName = "DieState", menuName = "CoreGame/State/DieState")]
public class DieState : State
{
    public override void EnterState()
    {
        base.EnterState();
        if (entity.hasBehaviourTree)
        {
            entity.behaviourTree.value.DisableBehavior();
        }
        controller.SetTrigger(eventCollectionData[idState].NameTrigger,eventCollectionData[idState].typeAnim,eventCollectionData[idState].timeStart);
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (timeTrigger > eventCollectionData[idState].durationAnimation)
        {
            ExitState();
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        //Destroy(controller.gameObject);
        PoolManager.Recycle(controller.GetComponent<PoolItem>());
//        controller.componentManager.DestroyEntity();
//        
//        controller.gameObject.SetActive(false);
//            Destroy(controller.gameObject);
    }
    public override void OnRevive()
    {
        if (timeTrigger > eventCollectionData[idState].durationAnimation)
        {
            base.OnRevive();
            controller.ChangeState(NameState.ReviveState, true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FreezeState", menuName = "CoreGame/State/FreezeState")]
public class FreezeState : State
{
    public float duration = 0.4f;
    public override void EnterState()
    {
        base.EnterState();
        if (entity.hasBehaviourTree)
        {
            entity.behaviourTree.behaviorTree.DisableBehavior();
        }
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (timeTrigger > eventCollectionData[idState].durationAnimation)
        {
            controller.ChangeState(NameState.IdleState);
            //ExitState();
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        if (entity.hasBehaviourTree)
        {
            entity.behaviourTree.behaviorTree.EnableBehavior();
        }
    }
}

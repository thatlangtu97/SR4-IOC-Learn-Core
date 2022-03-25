using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ReviveState", menuName = "CoreGame/State/ReviveState")]
public class ReviveState : State
{
    public override void EnterState()
    {
        base.EnterState();
        controller.SetTrigger(eventCollectionData[idState].NameTrigger);
        controller.componentManager.rgbody2D.velocity = Vector2.zero;
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (timeTrigger < eventCollectionData[idState].durationAnimation)
        {
        }
        else
        {
            entity.health.health = entity.health.maxHealth;
            if (controller.componentManager.checkGround() == true)
            {
                if (controller.componentManager.speedMove != 0)
                {
                    controller.ChangeState(NameState.MoveState, 0, true);
                }
                else
                {
                    controller.ChangeState(NameState.IdleState, 0, true);
                }
            }
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

using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AttackState", menuName = "CoreGame/State/AttackState")]
public class AttackState : State
{
    bool isEnemyForwark;
    public float timeBuffer = 0.15f;
    public bool useCheckEnemyForwark=true;
    public bool lockGravity = false;
    public override void EnterState()
    {
        base.EnterState();
        controller.componentManager.isAttack = true;
        if (lockGravity)
        {
            controller.componentManager.rgbody2D.gravityScale = 0;
        }
        CastSkill();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (timeTrigger < eventCollectionData[idState].durationAnimation)
        {
            if(useCheckEnemyForwark)
                isEnemyForwark = controller.componentManager.checkEnemyForwark();
            if (!isEnemyForwark)
            {
//                if (controller.componentManager.checkGround())
//                {
                    Vector2 velocityAttack = new Vector2(eventCollectionData[idState].curveX.Evaluate(timeTrigger),
                        eventCollectionData[idState].curveY.Evaluate(timeTrigger));
                    controller.componentManager.rgbody2D.position +=
                        new Vector2(velocityAttack.x * controller.transform.localScale.x,
                            velocityAttack.y * controller.transform.localScale.y) * Time.deltaTime;
                    controller.componentManager.rgbody2D.velocity = Vector2.zero;
//                }

            }

            if (controller.componentManager.checkGround() == false)
            {
                if (controller.componentManager.isBufferAttack == true)
                    controller.ChangeState(NameState.AirAttackState);
                else
                    controller.ChangeState(NameState.FallingState);
            }


//
//            if (controller.componentManager.isBufferAttack == true && (timeTrigger + timeBuffer) > eventCollectionData[idState].durationAnimation)
//            {
//                timeTrigger += timeBuffer;
//                if (!controller.componentManager.checkGround())
//                {
//                    controller.ChangeState(NameState.AirAttackState);
//                }
//            }
//            else
//            {
//                if ((timeTrigger + timeBuffer) > eventCollectionData[idState].durationAnimation)
//                {
//                    if (controller.componentManager.checkGround() == false)
//                    {
//                        controller.ChangeState(NameState.FallingState);
//                    }
//                }
//            }
            
        }
        else
        {
            if (controller.componentManager.isBufferAttack == true)
            {
                
                if (idState+1 >= eventCollectionData.Count)
                {
                    if (controller.componentManager.speedMove != 0)
                    {
                        controller.ChangeState(NameState.MoveState);
                    }
                    else
                    {
                        controller.ChangeState(NameState.IdleState);
                    }
                    return;
                }
                idState = Mathf.Clamp(idState + 1, 0, eventCollectionData.Count - 1);
                CastSkill();
            }
            else
            {
                if (controller.componentManager.checkGround() == true)
                {
                    if (controller.componentManager.speedMove != 0)
                    {
                        controller.ChangeState(NameState.MoveState);
                    }
                    else
                    {
                        controller.ChangeState(NameState.IdleState);
                    }
                }
                else
                {
                    controller.ChangeState(NameState.FallingState);
                }
            }
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        controller.componentManager.isAttack = false;
        idState = 0;
        if (lockGravity)
        {
            controller.componentManager.rgbody2D.gravityScale = controller.componentManager.gravityScale;
        }
    }
    public void CastSkill()
    {
        base.ResetTrigger();
        ResetEvent();
        isEnemyForwark = false;
//        if(useCheckEnemyForwark)
//            isEnemyForwark = controller.componentManager.checkEnemyForwark();
        controller.componentManager.Rotate();
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
        controller.componentManager.rgbody2D.velocity = Vector2.zero;
        controller.componentManager.isBufferAttack = false;

    }
    public override void OnInputDash()
    {
        base.OnInputDash();
        controller.ChangeState(NameState.DashState);
    }
    public override void OnInputJump()
    {
        base.OnInputJump();
        controller.ChangeState(NameState.JumpState);
    }
    public override void OnInputMove()
    {
        base.OnInputMove();
        if (idState >= eventCollectionData.Count) return;
        if (timeTrigger >= eventCollectionData[idState].durationAnimation)
            controller.ChangeState(NameState.MoveState);
    }
    public override void OnInputAttack()
    {
        base.OnInputAttack();
        controller.componentManager.isBufferAttack = true;
    }
    public override void OnInputSkill(int idSkill)
    {
        base.OnInputSkill(idSkill);
        if (controller.componentManager.checkGround() == true)
        {
            controller.ChangeState(NameState.SkillState);
        }
        else
        {
            controller.ChangeState(NameState.AirSkillState);
        }
    }
}

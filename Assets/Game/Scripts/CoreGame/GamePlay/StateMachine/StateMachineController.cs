﻿using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
public class StateMachineController : MonoBehaviour
{
    [DictionaryDrawerSettings(KeyLabel = "CustomKeyName", ValueLabel = "CustomValueLabel")]
    public Dictionary<NameState, State> dictionaryStateMachine = new Dictionary<NameState, State>();
    [BoxGroup("Current State")]
    public State currentState;
    [BoxGroup("Current State")]
    public NameState currentNameState;
    [BoxGroup("Previous State")]
    public NameState previousNameState;
    [LabelText("STATE TO CLONE")]
    public List<StateClone> States;

    public StateMachineCollection collection;
    
    public List<string> nameTrigger;
    public ComponentManager componentManager;
    public Animator animator;

    private StateMachineCollection _collectionSpawned;
//    private void Awake()
//    {
////        foreach (AnimatorControllerParameter p in animator.parameters)
////        {
////            if (p.type == AnimatorControllerParameterType.Trigger)
////            {
////                nameTrigger.Add(p.name);
////            }
////        }
//        //SetupAnim(animator);
//    }
    
    [Button("SETUP CONTROLL UI", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    public void SetupEntity()
    {
        GameUIController.instance.stateMachine = this;
        GameUIController.instance.MODIFY();
    }
    public void SetupAnim(Animator animator)
    {
        if(! animator) return;
        foreach (AnimatorControllerParameter p in animator.parameters)
        {
            if (p.type == AnimatorControllerParameterType.Trigger)
            {
                nameTrigger.Add(p.name);
            }
        }
    }

    public void SetupState()
    {
        //SetupAnim(animator);
        _collectionSpawned = PoolManager.SpawnPoolOrther(collection) as StateMachineCollection;
        dictionaryStateMachine = _collectionSpawned.dictionaryState;

        currentState = null;
        currentNameState = NameState.UnknowState;
        InitAllState();
        
//        dictionaryStateMachine = new Dictionary<NameState, State>();
//        currentState = null;
//        currentNameState = NameState.UnknowState;
//        foreach (StateClone tempState in States) {
//            CreateStateFactory(tempState);
//        }
    }

    public void Recycle()
    {
        PoolManager.RecycleOther(_collectionSpawned);
    }

    public void SetTrigger(string name, AnimationTypeState type , float timestart)
    {
        if (animator)
        {
            switch (type)
            {
                case AnimationTypeState.Trigger:
                    animator.SetTrigger(name);
                    break;
                case AnimationTypeState.PlayAnim:
                    animator.Play(name,0,timestart);
                    break;
            }
        }
    }
    
    public void SetSpeed(float speed)
    {
        if (animator)
        {
            animator.speed = speed;
        }
    }
    public virtual void InitStateMachine()
    {
//        if(dictionaryStateMachine.Count==0)
            SetupState();
        if (dictionaryStateMachine.ContainsKey(NameState.SpawnState))
        {
            ChangeState(NameState.SpawnState, 0, true);
        }
        else
        {
            ChangeState(NameState.IdleState, 0, true);
        }
    }
    public virtual void UpdateState()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }
    public virtual void OnSpawn()
    {
    }
    public virtual void OnRevival()
    {
    }
    protected void CreateStateFactory(StateClone stateClone)
    {
        State state = Instantiate(stateClone.StateToClone);
        state.InitState(this, componentManager);
        if (!dictionaryStateMachine.ContainsKey(stateClone.NameState))
        {
            dictionaryStateMachine.Add(stateClone.NameState, state);
        }
        else
        {
            dictionaryStateMachine[stateClone.NameState] = state;
        }
        
//        if (!dictionaryStateMachine.ContainsKey(stateClone.NameState))
//        {
//            State state = Instantiate(stateClone.StateToClone);
//            state.InitState(this, componentManager);
//            dictionaryStateMachine.Add(stateClone.NameState, state);
//        }
//        else
//        {
//            dictionaryStateMachine[stateClone.NameState].InitState(this, componentManager);
//        }
//        
    }

    protected void InitAllState()
    {
        foreach (var VARIABLE in dictionaryStateMachine.Values)
        {
            VARIABLE.InitState(this, componentManager);
        }
    }
    public virtual void ChangeState(NameState nameState, bool forceChange = false)
    {
        if (!dictionaryStateMachine.ContainsKey(nameState)) return;
        State newState = dictionaryStateMachine[nameState];
        if (!forceChange)
        {
            if (currentNameState != NameState.DieState && currentNameState != NameState.ReviveState)
            {
                
                if (nameState != currentNameState)
                {
                    if (currentState)
                    {
                        currentState.ExitState();
                    }

                    previousNameState = currentNameState;
                    currentState = newState;
                    currentNameState = nameState;
                    currentState.EnterState();
                }
            }
        }
        else
        {
            if (currentState)
            {
                currentState.ExitState();
            }
            previousNameState = currentNameState;
            currentState = newState;
            currentNameState = nameState;
            currentState.EnterState();
        }
    }
    public virtual void ChangeState(NameState nameState, int idState, bool forceChange = false)
    {
        if (!dictionaryStateMachine.ContainsKey(nameState)) return;
        State newState = dictionaryStateMachine[nameState];
        newState.idState = idState;
        if (!forceChange)
        {
            if (currentNameState != NameState.DieState && currentNameState != NameState.ReviveState)
            {
                if (nameState != currentNameState)
                {
                    if (currentState)
                    {
                        currentState.ExitState();
                    }
                    previousNameState = currentNameState;
                    currentState = newState;
                    currentNameState = nameState;
                    currentState.EnterState();
                }
            }
        }
        else
        {
            if (currentState)
            {
                currentState.ExitState();
            }
            previousNameState = currentNameState;
            currentState = newState;
            currentNameState = nameState;
            currentState.EnterState();
        }
    }
    public void SetIdState(NameState nameState, int idState)
    {
        if (!dictionaryStateMachine.ContainsKey(nameState)) return;
        State newState = dictionaryStateMachine[nameState];
        newState.idState = idState;
    }
    public virtual void OnInputState(NameState nameState,int idState)
    {
    }
    public virtual void OnInputAttack()
    {
    }
    public virtual void OnInputAttack(int idState)
    {

    }
    public virtual void OnInputJump()
    {
    }
    public virtual void OnInputMove()
    {
    }
    public virtual void OnInputDash()
    {
    }
    public virtual void OnInputRevive()
    {
    }
    public virtual void OnInputSkill(int idSkill)
    {
    }
    public virtual void OnHit(Action action)
    {
        if(componentManager.HasImmune(Immune.HIT)) 
            return;
        if (action != null)
        {
            action.Invoke();
        }
        ChangeState(NameState.HitState,true);
    }
    public virtual void OnKnockDown(Action action)
    {
        
        if(componentManager.HasImmune(Immune.KNOCK))
        {
            return;
        }
        ChangeState(NameState.KnockDownState, true);
        if (action != null)
        {
            action.Invoke();
        }
    }
    public virtual void InvokeAction(Action action)
    {
        if (action != null)
        {
            action.Invoke();
        }
    }
}

[System.Serializable]
public struct StateClone
{
    public NameState NameState;
    public State StateToClone;
}
public enum NameState
{
    UnknowState,
    SpawnState,
    IdleState,
    MoveState,
    JumpState,
    DashState,
    DieState,
    ReviveState,
    AttackState,
    AirAttackState,
    DashAttackState,
    SkillState,
    KnockDownState,
    HitState,
    GetUpState,
    FreezeState,
    StuntState,
    AirSkillState,
    FallingState,
    KnockUpState,
    RollOutState,

}
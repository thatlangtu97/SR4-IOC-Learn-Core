using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using UniRx;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Random = UnityEngine.Random;


public class TakeDamageSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    GameEntity targetEnemy;
    StateMachineController stateMachine ;
    DamageInfoSend damageInfoSend;
    List<Vector3> RandomVector;
    CompositeDisposable _disposable;
    int countRandom = 0;
    
    public TakeDamageSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
        RandomListPosition();
        _disposable= new CompositeDisposable();
    }
    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.TakeDamage);
    }
    protected override bool Filter(GameEntity entity)
    {
        return entity.hasTakeDamage;
    }
    protected override void Execute(List<GameEntity> entities)
    {
        //NativeArray<GameEntityData> datas = new NativeArray<GameEntityData>(entities.Count,Allocator.TempJob);
        float timedelay = 0;
        int indexAction = 0;
        foreach (GameEntity myEntity in entities)
        { 
            targetEnemy = myEntity.takeDamage.targetEnemy;
            stateMachine = targetEnemy.stateMachineContainer.value;
            damageInfoSend = myEntity.takeDamage.damageInfoSend;
            Vector3 position = stateMachine.transform.position;
            if (targetEnemy == null)
            {
                return;
            }
            
            if (!stateMachine)
            {
                return;
            }

            if (!stateMachine.componentManager.HasImmune(Immune.BLOCK))
            {
                int damageTake=(int) (damageInfoSend.damageProperties *
                                      damageInfoSend.damageInfoEvent.damageScale);
                targetEnemy.health.health -= damageTake; 
                if (targetEnemy.health.health <= 0)
                {
                    stateMachine.ChangeState(NameState.DieState);
                    targetEnemy.health.health = 0;
                }
                else
                {
                    switch (damageInfoSend.damageInfoEvent.powerCollider) {
                        //case PowerCollider.Node:
                        //    entityEnemy.stateMachineContainer.stateMachine.InvokeAction(e.takeDamage.action);
                        //    break;
                        case PowerCollider.Small:
                        case PowerCollider.Medium:
                        case PowerCollider.Heavy:
                            stateMachine.OnHit(damageInfoSend.action);
                            break;
                        case PowerCollider.KnockDown:
                            stateMachine.OnKnockDown(damageInfoSend.action); 
                            break;
                    }
                
                }

                Vector3 randomPos = RandomVector[countRandom];
                Observable.Timer(TimeSpan.FromSeconds(timedelay)).Subscribe(l => {  DamageTextManager.AddReactiveComponent(DamageTextType.Normal,damageTake.ToString(),position + randomPos); }).AddTo(_disposable);
                timedelay += 0.01f;
                
                //Observable.TimerFrame(1,FrameCountType.Update).Subscribe(l => {  DamageTextManager.AddReactiveComponent(DamageTextType.Normal,damageTake.ToString(),position + randomPos); }).AddTo(_disposable);
            }
            else
            {
                Vector3 randomPos = RandomVector[countRandom];
                Observable.Timer(TimeSpan.FromSeconds(timedelay)).Subscribe(l => {  DamageTextManager.AddReactiveComponent(DamageTextType.Normal,"Block",position + randomPos); }).AddTo(_disposable);
                timedelay += 0.01f;
                
                //Observable.TimerFrame(1,FrameCountType.FixedUpdate).Subscribe(l => {  DamageTextManager.AddReactiveComponent(DamageTextType.Normal,"Block",position + randomPos); }).AddTo(_disposable);
                
            }
            countRandom = (countRandom+1) % (RandomVector.Count);
            ObjectPool.instance.RecycleEntity(myEntity);
        }
    } 
    
    public void RandomListPosition()
    {
        RandomVector = new List<Vector3>();
        int count = 0;
        while (count<100)
        {
            count += 1;
            RandomVector.Add(new Vector3(Random.Range(-.5f,.5f),Random.Range(1.5f,2f),0f));
        }
    }
}

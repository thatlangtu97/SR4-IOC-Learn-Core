using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Random = UnityEngine.Random;


public class TakeDamageSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    GameEntity targetEnemy;
    StateMachineController stateMachine ;
    DamageInfoSend damageInfoSend ;
    public TakeDamageSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
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
        foreach (GameEntity myEntity in entities)
        { 
            targetEnemy = myEntity.takeDamage.targetEnemy;
//            StateMachineController stateMachine = targetEnemy.stateMachineContainer.value;
//            DamageInfoSend damageInfoSend = myEntity.takeDamage.damageInfoSend;
            stateMachine = targetEnemy.stateMachineContainer.value;
            damageInfoSend = myEntity.takeDamage.damageInfoSend;

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
                    
                DamageTextManager.AddReactiveComponent(DamageTextType.Normal,damageTake.ToString(),stateMachine.transform.position + new Vector3(Random.Range(-.5f,.5f),Random.Range(1.5f,2f),0f));
            }
            else
            {
                DamageTextManager.AddReactiveComponent(DamageTextType.Normal,"Block",stateMachine.transform.position + new Vector3(Random.Range(-.5f,.5f),Random.Range(1.5f,2f),0f));
            }
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
            ObjectPool.instance.RecycleEntity(myEntity);
            //myEntity.RemoveAllComponents();
            //myEntity.Destroy();
            
            
            
            
            
            
            
        }
        //        NativeArray<float2> position = new NativeArray<float2>(1,Allocator.TempJob);
//        var job = new jobupdate(){position = position};
//        JobHandle jobHandle = job.Schedule(1, 1);
//        jobHandle.Complete();
//        position.Dispose();
    } 
    public struct GameEntityData
    {
        public GameEntity entity;
    }
    public struct JobUpdateTakeDamage : IJobParallelFor
    {
        public NativeArray<GameEntityData> Datas;
        public void Execute(int index)
        {
        }
    }
}

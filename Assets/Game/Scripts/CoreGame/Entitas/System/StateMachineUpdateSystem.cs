using Entitas;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class StateMachineUpdateSystem : IExecuteSystem
{
    public readonly Contexts context;
    readonly IGroup<GameEntity> entities;
    //UpdateMecanimJobSystem updateMecanimJobSystem;
    public StateMachineUpdateSystem(Contexts _contexts)
    {
        context = _contexts;
        entities = context.game.GetGroup(GameMatcher.AllOf(GameMatcher.StateMachineContainer));
        //updateMecanimJobSystem = new UpdateMecanimJobSystem(context.game, 1);
    }
    public void Execute()
    {
        
//        int sizeEntity = entities.GetEntities().Length;
//        
//        if(sizeEntity<=0) return;
        
        
        
//        NativeArray<DataCastGround> datas = new NativeArray<DataCastGround>(sizeEntity, Allocator.TempJob);
//        NativeArray<int> results = new NativeArray<int>(sizeEntity, Allocator.TempJob);    
        
        int count = 0;
        foreach (var e in entities.GetEntities())
        {
//            if (!e.hasStateMachineContainer)
//            {
//                e.RemoveAllComponents();
//                e.Destroy();
//                continue;
//            }

            e.stateMachineContainer.value.UpdateState();
//            datas[count] = e.stateMachineContainer.value.componentManager.GetDataCastGround;
//            results[count] = e.stateMachineContainer.value.componentManager.intCheckGround;
            //e.stateMachineContainer.stateMachine.componentManager.UpdateMecanim();
        }

//        var job = new JobUpdateGround()
//        {
//            datas = datas,
//            results = results,
//        };
//        JobHandle jobHandle = job.Schedule(sizeEntity, 1);
//        jobHandle.Complete();
//        int count2 = 0;
//        foreach (var e in entities.GetEntities())
//        {
//
//            e.stateMachineContainer.value.componentManager.intCheckGround = results[count2];
//            count2++;
//        }
//
//        datas.Dispose();
//        results.Dispose();
    }
    
//    public struct JobUpdateGround : IJobParallelFor
//    {
//        public NativeArray<DataCastGround> datas;
//        public NativeArray<int> results;
//        public void Execute(int index)
//        {
//            int t = Physics2D.BoxCastNonAlloc(datas[index].origin, datas[index].size, datas[index].angle, datas[index].direction,
//                datas[index].results, datas[index].distance, datas[index].layerMask);
//            results[index] = t;
//        }
//    }
//    public struct DataCastWall
//    {
//        public Vector2 origin;
//        public Vector2 direction;
//        public RaycastHit2D[] results;
//        public float distance;
//        public int layerMask;
//    }
//    public struct DataCastGround
//    {
//        public Vector2 origin;
//        public Vector2 size;
//        public float angle;
//        public Vector2 direction;
//        public RaycastHit2D[] results;
//        public float distance;
//        public int layerMask;
//    }
}

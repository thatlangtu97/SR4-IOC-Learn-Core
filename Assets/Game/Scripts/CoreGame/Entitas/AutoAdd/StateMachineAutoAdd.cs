using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineAutoAdd : AutoAddComponent
{
    public ConvertToStateMachineContainer StateMachine;
    public Spine.Unity.SkeletonMecanim prefabMecanim;
    public override void AddComponent(ref GameEntity e)
    {
        //Spine.Unity.SkeletonMecanim temp = ObjectPool.SpawnNotDeactive(prefabMecanim, StateMachine.value.transform);
        //StateMachine.value.animator = temp.GetComponent<Animator>();
        //StateMachine.value.componentManager.meshRenderer = temp.GetComponent<MeshRenderer>();
        e.AddStateMachineContainer(StateMachine.value);
        StateMachine.value.InitStateMachine();
        //StateMachine.value.SetupAnim(StateMachine.value.animator);
    }

    
}

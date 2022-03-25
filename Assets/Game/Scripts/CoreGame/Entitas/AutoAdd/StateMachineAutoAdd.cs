using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineAutoAdd : MonoBehaviour, IAutoAdd<GameEntity>
{
    public StateMachineContainerComponent stateMachineContainerComponent;
    public void AddComponent(ref GameEntity e)
    {
        e.AddStateMachineContainer(stateMachineContainerComponent.value);
        stateMachineContainerComponent.value.InitStateMachine();
        //Destroy(this);
    }

    
}

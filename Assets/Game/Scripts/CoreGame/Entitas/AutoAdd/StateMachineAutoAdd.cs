using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineAutoAdd : MonoBehaviour, IAutoAdd<GameEntity>
{
    public ConvertToStateMachineContainer StateMachine;
    public void AddComponent(ref GameEntity e)
    {
        e.AddStateMachineContainer(StateMachine.value);
        StateMachine.value.InitStateMachine();
    }

    
}

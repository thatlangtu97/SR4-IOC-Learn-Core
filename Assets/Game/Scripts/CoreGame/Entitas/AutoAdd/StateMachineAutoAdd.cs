using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineAutoAdd : AutoAddComponent
{
    public ConvertToStateMachineContainer StateMachine;
    public override void AddComponent(ref GameEntity e)
    {
        e.AddStateMachineContainer(StateMachine.value);
        StateMachine.value.InitStateMachine();
    }

    
}

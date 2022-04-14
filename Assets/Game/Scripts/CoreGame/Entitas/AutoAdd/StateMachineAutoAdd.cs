using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineAutoAdd : AutoAddComponent
{
    //public ConvertToStateMachineContainer StateMachine;
    public StateMachineController value;
    public override void AddComponent(ref GameEntity e)
    {
        e.AddStateMachineContainer(value);
        value.InitStateMachine();
    }

    
}

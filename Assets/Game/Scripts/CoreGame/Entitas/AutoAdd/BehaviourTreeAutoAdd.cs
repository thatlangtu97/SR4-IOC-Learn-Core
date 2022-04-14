using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class BehaviourTreeAutoAdd : AutoAddComponent
{
    //public ConvertToBehaviourTree behaviourTreeComponent;
    public BehaviorTree value;
    public override void AddComponent(ref GameEntity e)
    {
        e.AddBehaviourTree(value);
    }
}

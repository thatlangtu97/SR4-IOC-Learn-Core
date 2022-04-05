using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeAutoAdd : AutoAddComponent
{
    public ConvertToBehaviourTree behaviourTreeComponent;
    public override void AddComponent(ref GameEntity e)
    {
        e.AddBehaviourTree(behaviourTreeComponent.value);
    }
}

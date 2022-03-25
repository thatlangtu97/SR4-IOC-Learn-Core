using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeAutoAdd : MonoBehaviour , IAutoAdd<GameEntity>
{
    public BehaviourTreeComponent behaviourTreeComponent;
    public void AddComponent(ref GameEntity e)
    {
        e.AddBehaviourTree(behaviourTreeComponent.value);
    }
}

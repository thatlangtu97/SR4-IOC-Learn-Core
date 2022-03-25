using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlagAutoAdd : MonoBehaviour, IAutoAdd<GameEntity>
{
    public PlayerFlagComponent playerFlagComponent;
    public void AddComponent(ref GameEntity e)
    {
        e.AddPlayerFlag(playerFlagComponent.isPlayer);
    }
    
}

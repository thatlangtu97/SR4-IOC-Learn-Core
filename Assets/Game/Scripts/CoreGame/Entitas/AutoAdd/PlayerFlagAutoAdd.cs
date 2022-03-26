using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlagAutoAdd : MonoBehaviour, IAutoAdd<GameEntity>
{
    public ConvertToPlayerFlag value;
    public void AddComponent(ref GameEntity e)
    {
        e.AddPlayerFlag(value.isPlayer);
    }
    
}

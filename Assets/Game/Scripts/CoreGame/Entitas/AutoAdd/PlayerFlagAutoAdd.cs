using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlagAutoAdd : AutoAddComponent
{
    public ConvertToPlayerFlag value;
    public override void AddComponent(ref GameEntity e)
    {
        e.AddPlayerFlag(value.isPlayer);
    }
    
}

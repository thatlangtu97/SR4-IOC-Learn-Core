using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarAutoAdd : MonoBehaviour, IAutoAdd<GameEntity>
{
    public HPBarUI prefab;
    public bool left;
    public void AddComponent(ref GameEntity e)
    {
        HPBarUI temp = GameUIController.instance.SpawnHPBar(prefab, left);
        e.AddHealthBarUI(temp);
        
    }
}

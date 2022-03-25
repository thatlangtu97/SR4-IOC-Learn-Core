using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PowerAutoAdd : MonoBehaviour , IAutoAdd<GameEntity>
{
    public PowerComponent powerComponent;
    public void AddComponent(ref GameEntity e)
    {
        e.AddPower(powerComponent.power);
    }
}

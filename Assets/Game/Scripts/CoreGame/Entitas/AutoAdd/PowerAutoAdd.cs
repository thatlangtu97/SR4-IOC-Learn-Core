using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PowerAutoAdd : MonoBehaviour , IAutoAdd<GameEntity>
{
    public ConvertToPower powerComponent;
    public void AddComponent(ref GameEntity e)
    {
        e.AddPower(powerComponent.value);
    }
}

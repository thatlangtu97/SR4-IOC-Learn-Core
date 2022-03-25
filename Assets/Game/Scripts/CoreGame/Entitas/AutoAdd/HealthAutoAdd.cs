using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class HealthAutoAdd : MonoBehaviour, IAutoAdd<GameEntity>
{
    public HealthComponent healthComponent;
    public void AddComponent(ref GameEntity e)
    {
        e.AddHealth(healthComponent.health,healthComponent.maxHealth);
    }
}

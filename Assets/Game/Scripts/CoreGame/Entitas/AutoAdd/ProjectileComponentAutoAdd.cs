using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileComponentAutoAdd : MonoBehaviour, IAutoAdd<GameEntity>
{
    public ConvertToProjectileContainer component;
    public void AddComponent(ref GameEntity e)
    {
        e.AddProjectileContainer(component.value);
    }
}

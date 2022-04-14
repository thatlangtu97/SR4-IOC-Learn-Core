using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileComponentAutoAdd : AutoAddComponent
{
    //public ConvertToProjectileContainer component;
    public ProjectileComponent value;
    public override void AddComponent(ref GameEntity e)
    {
        e.AddProjectileContainer(value);
    }
}

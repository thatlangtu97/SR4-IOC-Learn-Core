﻿using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class HealthAutoAdd : AutoAddComponent
{
    //public ConvertToHealth healthComponent;
    
    public int health;
    public int maxHealth;
    public override void AddComponent(ref GameEntity e)
    {
        e.AddHealth(health,maxHealth);
    }
}

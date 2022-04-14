using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PowerAutoAdd : AutoAddComponent
{
    //public ConvertToPower powerComponent;
    public int value;
    public override void AddComponent(ref GameEntity e)
    {
        e.AddPower(value);
    }
}

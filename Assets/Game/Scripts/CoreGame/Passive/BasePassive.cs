using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
public interface BasePassive 
{
    string name{ get; set; }
    
    List<IGamePassiveCondition> conditions { get; set; }

    void OnStart();

    void OnUpdate();

    void OnExit();

    bool PassCondition();
}

public class IncreaseStat : BasePassive
{
    [ShowInInspector]
    string _name;
    [ListDrawerSettings(ListElementLabelName = "GetName")]
    [HideReferenceObjectPicker]
    [ShowInInspector]
    List<IGamePassiveCondition> _conditions;

    public string name {
        get { return _name; }
        set { _name = value; } 
    }
    public List<IGamePassiveCondition> conditions

    {
        get { return _conditions; }
        set { _conditions = value; }
    }

    public void OnStart()
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public bool PassCondition()
    {
        foreach (var VARIABLE in conditions)
        {
            if (!VARIABLE.PassCondition())
            {
                return false;
            }
        }
        return true;
    }
}


using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
public interface BasePassive 
{
    string name{ get; set; }

    //public BaseSkillType type;
    [ListDrawerSettings(ListElementLabelName = "GetName")]
    List<IGamePassiveCondition> conditions { get; set; }

    void OnStart();

    void OnUpdate();

    void OnExit();
//    public BasePassive( List<IGamePassiveCondition> conditions)
//    {
//        this.conditions = new List<IGamePassiveCondition>(conditions);
//    }
}

public class IncreaseStat : BasePassive
{

    public string _name;
    public string name {
        get { return _name; }
        set { _name = value; } 
    }
    [HideReferenceObjectPicker]
    public List<IGamePassiveCondition> _conditions;
    
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
}


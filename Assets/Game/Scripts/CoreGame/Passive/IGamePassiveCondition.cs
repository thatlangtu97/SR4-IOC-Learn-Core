using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGamePassiveCondition
{
    void OnStart(GameEntity entity);
    void OnUpdate();
    void OnExit();
    
    string GetName();
}

public class ApplyOnStart : IGamePassiveCondition
{

    public void OnStart(GameEntity entity)
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

    public string GetName()
    {
        return "Apply On Start";
    }
}

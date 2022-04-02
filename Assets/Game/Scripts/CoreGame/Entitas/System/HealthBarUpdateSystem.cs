using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
public class HealthBarUpdateSystem : IExecuteSystem
{
    public readonly Contexts context;
    readonly IGroup<GameEntity> entities;
    public HealthBarUpdateSystem(Contexts _contexts)
    {
        context = _contexts;
        entities = context.game.GetGroup(GameMatcher.AllOf(GameMatcher.HealthBarUI));
    }
    public void Execute()
    {
        foreach (var e in entities.GetEntities())
        {
            e.healthBarUI.hpBarUI.Setvalue(e.health.health,e.health.maxHealth);
        }
    }

}

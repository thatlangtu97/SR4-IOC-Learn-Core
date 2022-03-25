using BehaviorDesigner.Runtime;
using UnityEngine;
using Entitas;

[Game]    
[System.Serializable]
public class PlayerFlagComponent : IComponent
{
    public bool isPlayer = true;
}
    
[Game]
[System.Serializable]
public class ProjectileContainerComponent : IComponent
{
    public ProjectileComponent projectileComponent;
}
    
[Game]
[System.Serializable]
public class StateMachineContainerComponent : IComponent
{
    public StateMachineController stateMachine;
}
    
[Game]
[System.Serializable]
public class DamageTextComponent  : IComponent
{
    public DamageTextType damageTextType;
    public string value;
    public Vector3 position;
}
public enum DamageTextType
{
    Normal,
    Block,
}

[Game]
[System.Serializable]
public class HealthComponent : IComponent
{
    public int health;
    public int maxHealth;
}
    
[Game]
[System.Serializable]
public class PowerComponent : IComponent
{
    public int power;
}
[Game]
[System.Serializable]
public class TakeDamageComponent : IComponent
{
    public GameEntity myEntity;
    public GameEntity targetEnemy;
    public DamageInfoSend damageInfoSend;
    public TakeDamageComponent(){}
    public TakeDamageComponent(GameEntity myEntity, GameEntity targetEnemy,DamageInfoSend damageInfoSend)
    {
        this.myEntity = myEntity;
        this.targetEnemy = targetEnemy;
        this.damageInfoSend = damageInfoSend;
    }
}
[Game]
[System.Serializable]
public class BehaviourTreeComponent : IComponent
{
    public BehaviorManager.BehaviorTree behaviorTree;
}
public partial class GameContext
{
    public GameEntity playerFlagEntity { get { return GetGroup(GameMatcher.PlayerFlag).GetSingleEntity(); } }
}




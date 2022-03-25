using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

    [Game]
    public class PlayerFlagComponent : IComponent
    {
    }
    
    [Game]
    public class ProjectileContainerComponent : IComponent
    {
        public ProjectileComponent projectileComponent;
    }
    
    [Game]
    public class StateMachineContainerComponent : IComponent
    {
        public StateMachineController stateMachine;
    }
    
    [Game]
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
    public class HealthComponent : IComponent
    {
        public int health;
        public int maxHealth;
    }
    
    [Game]
    public class PowerComponent : IComponent
    {
        public int power;
    }
    [Game]
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
public partial class GameContext
{
    public GameEntity playerFlagEntity { get { return GetGroup(GameMatcher.PlayerFlag).GetSingleEntity(); } }
}




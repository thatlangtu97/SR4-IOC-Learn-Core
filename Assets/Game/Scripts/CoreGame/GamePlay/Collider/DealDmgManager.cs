using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDmgManager
{
    public static Contexts context;
    public static List<GameEntity> listEntity;
    public static void DealDamage(Collider2D target, GameEntity myEntity, DamageInfoSend damageInfoSend)
    {
        
        ComponentManager enemyComponent = target.GetComponent<ComponentManager>();      
        GameEntity targetEntity = enemyComponent.entity;
        AddReactiveComponent(myEntity, targetEntity, damageInfoSend);
    }
    public static void DealDamage(GameEntity targetEntity, GameEntity myEntity, DamageInfoSend damageInfoSend)
    {
        AddReactiveComponent(myEntity, targetEntity, damageInfoSend);
    }
    static void AddReactiveComponent( GameEntity myEntity, GameEntity targetEntity, DamageInfoSend damageInfoSend)
    {
        //GameEntity takeDamageComponent = context.game.CreateEntity();
        GameEntity takeDamageComponent = ObjectPool.instance.SpawnEntity();
        takeDamageComponent.AddTakeDamage(myEntity, targetEntity, damageInfoSend);
    }

}

public class DamageTextManager
{
    public static Contexts context;
    public static string hexColorNormal="#9c9c9c9c";
    public static List<GameEntity> listEntity;
    public static void AddReactiveComponent( DamageTextType newDamageTextType,string newValue, Vector3 newPosition)
    {
        //GameEntity damageText = context.game.CreateEntity();
        GameEntity damageText = ObjectPool.instance.SpawnEntity();
        damageText.AddDamageText(newDamageTextType, newValue, newPosition);
    }

    public static Color GetColor(DamageTextType newDamageTextType)
    {
        switch (newDamageTextType)
        {
            case DamageTextType.Normal:
                return Color.white;
            default: 
                return Color.black;
        }
    }
}
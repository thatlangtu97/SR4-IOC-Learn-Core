﻿using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using DG.Tweening;
using UnityEngine;
using Entitas;
using TMPro;


public class DamageTextSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    GameEntity targetEnemy;
    private DamageTextView textprefab;
    
    public DamageTextSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
        textprefab = Resources.Load<DamageTextView>("DamageTextPrefab");
    }
    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDamageText;
    }
    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.DamageText);
    }
    protected override void Execute(List<GameEntity> entities)
    {
        Debug.Log(entities.Count);
        foreach (GameEntity entity in entities)
        {
            //DamageTextView damageTextView = ObjectPool.instance.SpawnDamageText();
            //DamageTextView damageTextView = ObjectPool.SpawnNotDeactive(textprefab);
            DamageTextView damageTextView = PoolManager.Spawn(textprefab,null,PoolManager.TypeSpawn.NotDeactive);
            damageTextView.text = entity.damageText.value;
            damageTextView.color = DamageTextManager.GetColor(entity.damageText.damageTextType);
            damageTextView.transform.position = entity.damageText.position;
            damageTextView.transform.DOMove(damageTextView.transform.position + new Vector3(0f,.3f,0f),.4f);
            damageTextView.PlayAnim();
            //ObjectPool.instance.RecycleNotDeactive(damageTextView.gameObject, .5f);
            PoolManager.Recycle(damageTextView.gameObject,.5f,PoolManager.TypeSpawn.NotDeactive);
            PoolManager.RecycleEntity(entity);
        }
    }

}

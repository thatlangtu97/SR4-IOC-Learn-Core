using System.Collections;
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
        foreach (GameEntity myEntity in entities)
        {
            //DamageTextView damageTextView = ObjectPool.Spawn(textprefab);
            DamageTextView damageTextView = ObjectPool.instance.SpawnDamageText();
            damageTextView.text = myEntity.damageText.value;
            damageTextView.color = DamageTextManager.GetColor(myEntity.damageText.damageTextType);
            damageTextView.transform.position = myEntity.damageText.position;
            damageTextView.transform.DOMove(damageTextView.transform.position + new Vector3(0f,.3f,0f),.4f);
            damageTextView.PlayAnim();
            //ObjectPool.instance.RecycleDamageText(damageTextView,.5f);
            ObjectPool.instance.RecycleDamageText(damageTextView);
            ObjectPool.instance.RecycleEntity(myEntity);
        }
    }

}

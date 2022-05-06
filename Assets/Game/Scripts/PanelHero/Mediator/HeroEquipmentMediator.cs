using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

public class HeroEquipmentMediator : Mediator
{
    [Inject] public  HeroEquipmentView View { get; set; }
    [Inject] public OnViewHeroSignal OnViewHeroSignal { get; set; }
    
    [Inject] public LevelUpGearSuccessSignal LevelUpGearSuccessSignal { get; set; }
    
    
    public override void OnRegister()
    {
        OnViewHeroSignal.AddListener(View.Show);
        LevelUpGearSuccessSignal.AddListener(View.ReShow);
    }

    public override void OnRemove()
    {
        OnViewHeroSignal.RemoveListener(View.Show);
        LevelUpGearSuccessSignal.RemoveListener(View.ReShow);
    }

    private void OnDestroy()
    {
        OnRemove();
    }
}

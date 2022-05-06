using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

public class EquipmentDetailMediator : Mediator
{
    [Inject] public EquipmentDetailView View { get; set; }
    [Inject] public LevelUpGearSuccessSignal LevelUpGearSuccessSignal { get; set; }
    public override void OnRegister()
    {
        LevelUpGearSuccessSignal.AddListener(View.ReShow);
    }

    public override void OnRemove()
    {
        LevelUpGearSuccessSignal.RemoveListener(View.ReShow);
    }

    private void OnDestroy()
    {
        OnRemove();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevivePopup : AbsPopupView
{
    public Button reviveBtn;
    public Button closeBtn;
    protected override void Awake()
    {
        base.Awake();
        reviveBtn.onClick.AddListener(Revive);
        closeBtn.onClick.AddListener(HidePopup);
    }

    public override void ShowPopupByCmd()
    {
        base.ShowPopupByCmd();
        //this.gameObject.SetActive(true);
    }

    public void Revive()
    {
        Contexts.sharedInstance.game.playerFlagEntity.stateMachineContainer.value.OnInputRevive();
        HidePopup();
    }
    
}

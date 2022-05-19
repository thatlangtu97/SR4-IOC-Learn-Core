using System.CodeDom;
using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSetupHomeSceneCmd : Command
{
    [Inject]
    public PopupManager popupManager { get; set; }
    [Inject]
    public ShowPanelHomeSignal showPanelHomeSignal { get; set; }
    [Inject]
    public ShowPanelHeroSignal showPanelHeroSignal { get; set; }
    [Inject]
    public ShowPanelShopSignal showPanelShopSignal { get; set; }
    [Inject]
    public ShowPanelCraftSignal showPanelCraftSignal { get; set; }

    public override void Execute()
    {
        string panelKey = popupManager.GetPanelAfterLoadHomeScene();
        switch (panelKey)
        {
            case "PanelHomeView":
                showPanelHomeSignal.Dispatch();
                break;
            case "PanelHeroView":
                showPanelHeroSignal.Dispatch();
                break;
            case "PanelCraftView":
                showPanelCraftSignal.Dispatch();
                break;
            case "PanelShopView":
                showPanelShopSignal.Dispatch();
                break;
        }
        //popupManager.ResetPanelShowAfterLoadHomeScene();
    }
}

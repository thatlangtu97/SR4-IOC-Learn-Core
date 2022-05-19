using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShowPopupStaminaCmd : AbsShowPopupCmd
{
    public override void Execute()
    {
        PopupStaminaView popupStaminaView = GetInstance<PopupStaminaView>();
        popupStaminaView.ShowPopupByCmd();
        popupStaminaView.ShowPopup();
    }

    public override string GetResourcePath()
    {
        return GameResourcePath.POPUP_STAMINA;
    }
}

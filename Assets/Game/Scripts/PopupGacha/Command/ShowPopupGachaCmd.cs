using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopupGachaCmd : AbsShowPopupCmd
{
    [Inject] public GlobalData global { get; set; }
    [Inject] public DataGachaOpened dataGachaOpened { get; set; }
    public override void Execute()
    {
        PopupGachaView popupGachaView = GetInstance<PopupGachaView>();
        popupGachaView.dataGachaOpened = dataGachaOpened;
        popupGachaView.ShowPopupByCmd();
        popupGachaView.ShowPopup(new ParameterPopupGacha());
    }

    public override string GetResourcePath()
    {
        return GameResourcePath.POPUP_GACHA;
    }
}

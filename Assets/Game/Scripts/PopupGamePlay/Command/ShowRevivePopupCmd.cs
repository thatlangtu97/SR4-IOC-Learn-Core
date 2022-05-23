﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRevivePopupCmd : AbsShowPopupCmd
{
    public override void Execute()
    {
        base.Execute();
        RevivePopup revivePopup = GetInstance<RevivePopup>();
        revivePopup.ShowPopup(new ParameterPopup());
    }
    public override string GetResourcePath()
    {
        return GameResourcePath.POPUP_REVIVE;
    }
}

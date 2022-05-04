﻿using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class ShowTooltipPopupCmd : AbsShowPopupCmd
{
    [Inject] public ToolTipPopupParameter Parameter { get; set; }

    public override void Execute()
    {
        base.Execute();
        popupKey = PopupKey.TooltipPopup;
        ToolTipPopup toolTipPopup = GetInstance<ToolTipPopup>();
        toolTipPopup.SetParameter(Parameter);
        toolTipPopup.ShowPopupByCmd();
    }

    public override string GetResourcePath()
    {
        return GameResourcePath.TOOLTIP_POPUP;
    }
}

public class ToolTipPopupParameter : ParameterPopup
{
    public AbsRewardLogic rewardLogic;
    public Vector3 position;

    public ToolTipPopupParameter()
    {
        
    }

    public ToolTipPopupParameter(AbsRewardLogic rewardLogic, Vector3 position)
    {
        this.rewardLogic = rewardLogic;
        this.position = position;
    }
}

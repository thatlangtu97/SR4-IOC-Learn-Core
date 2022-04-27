using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class ShowPopupRewardCmd : Command
{
    [Inject] public List<AbsRewardLogic> listRewardLogics { get; set; }
    [Inject] public Action action { get; set; }

    public override void Execute()
    {
        
    }
}

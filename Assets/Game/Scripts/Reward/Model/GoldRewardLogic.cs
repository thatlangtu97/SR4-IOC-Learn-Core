using System.Collections;
using System.Collections.Generic;
using strange.extensions.injector.api;
using UnityEngine;

public class GoldRewardLogic : AbsRewardLogic
{
    public int value;
    
    public GoldRewardLogic(){}
    
    public GoldRewardLogic(int value)
    {
        this.value = value;
    }
    
    public override AbsRewardLogic AddReward(IInjectionBinder injectionBinder)
    {
        injectionBinder.GetInstance<AddGoldSignal>().Dispatch(value);
        return this;
    }
}

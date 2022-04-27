using System.Collections;
using System.Collections.Generic;
using strange.extensions.injector.api;
using UnityEngine;

public class GemRewardLogic : AbsRewardLogic
{
    public int value;
    
    public GemRewardLogic(){}
    
    public GemRewardLogic(int value)
    {
        this.value = value;
    }
    
    public override AbsRewardLogic AddReward(IInjectionBinder injectionBinder)
    {
        injectionBinder.GetInstance<AddGemSignal>().Dispatch(value);
        return this;
    }
}
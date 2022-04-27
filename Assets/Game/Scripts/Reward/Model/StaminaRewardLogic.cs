using System.Collections;
using System.Collections.Generic;
using strange.extensions.injector.api;
using UnityEngine;


public class StaminaRewardLogic : AbsRewardLogic
{
    public int value;
    
    public StaminaRewardLogic(){}
    
    public StaminaRewardLogic(int value)
    {
        this.value = value;
    }
    
    public override AbsRewardLogic AddReward(IInjectionBinder injectionBinder)
    {
        injectionBinder.GetInstance<AddStaminaSignal>().Dispatch(value);
        return this;
    }
}
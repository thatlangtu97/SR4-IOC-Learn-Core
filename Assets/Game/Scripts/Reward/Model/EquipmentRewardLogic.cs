using System.Collections;
using System.Collections.Generic;
using strange.extensions.injector.api;
using UnityEngine;

public class EquipmentRewardLogic : AbsRewardLogic
{
    public List<EquipmentData> equipmentData;

    public EquipmentRewardLogic(EquipmentData value)
    {
        equipmentData = new List<EquipmentData>();
        equipmentData.Add(value);
    }
    public EquipmentRewardLogic(List<EquipmentData> value)
    {
        equipmentData = value;

    }
    public override AbsRewardLogic AddReward(IInjectionBinder injectionBinder)
    {
        injectionBinder.GetInstance<AddEquipmentSignal>().Dispatch(new DataEquipmentRewardParameter(equipmentData));
        return this;
    }
}

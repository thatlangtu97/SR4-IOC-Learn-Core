using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class SellGearCmd : Command
{
    [Inject] public List<EquipmentData> EquipmentDatas { get; set; }

    [Inject] public SellGearSuccessSignal SellGearSuccessSignal { get; set; }

    public override void Execute()
    {
        EquipmentLogic.SellEquipment(EquipmentDatas);
        SellGearSuccessSignal.Dispatch(EquipmentDatas);
    }
}

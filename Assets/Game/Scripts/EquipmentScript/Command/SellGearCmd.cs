using System.Collections;
using System.Collections.Generic;
using System.Linq;
using strange.extensions.command.impl;
using UnityEngine;

public class SellGearCmd : Command
{
    [Inject] public DataSellGear dataSellGear { get; set; }
    [Inject] public SellGearSuccessSignal SellGearSuccessSignal { get; set; }
    [Inject] public UnequipGearSignal UnequipGearSignal { get; set; }

    public override void Execute()
    {
        GearSlot[] arraySlot = new[] {GearSlot.weapon, GearSlot.armor, GearSlot.ring, GearSlot.charm};
        Dictionary<GearSlot, int> gearEquiped = DataManager.Instance.HeroDataManager.GetGearEquiped(dataSellGear.gearOfHero);
        foreach (var equipment in dataSellGear.datas)
        {
            if(gearEquiped.Values.Contains(equipment.id))
            {
                UnequipGearSignal.Dispatch(equipment);
            }
        }
        EquipmentLogic.SellEquipment(dataSellGear.datas);
        SellGearSuccessSignal.Dispatch(dataSellGear.datas);
    }
}

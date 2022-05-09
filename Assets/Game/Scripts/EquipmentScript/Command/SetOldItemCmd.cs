using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class SetOldItemCmd : Command
{
    [Inject] public EquipmentData equipmentData { get; set; }
    
    public override void Execute()
    {
        DataManager.Instance.InventoryDataManager.SetOldItem(equipmentData);
    }
}

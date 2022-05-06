using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentDetailView : AbsPopupView
{

    [Inject] public EquipGearSignal equipGearSignal { get; set; }
    [Inject] public UnequipGearSignal unEquipGearSignal { get; set; }
    [Inject] public NotificationPanelHeroSignal NotificationPanelHeroSignal { get; set; }
    [Inject] public NotificationPanelCraftSignal NotificationPanelCraftSignal { get; set; }
    
    [Inject] public LevelUpGearSignal LevelUpGearSignal { get; set; }
    public EquipmentItemView EquipmentView;
    public Text nameEquipment;
    public Text rarityEquipment;
    public Text mainStatType,mainStatValue;
    public Text textLevel;
    EquipmentData equipmentData;
    EquipmentConfig equipmentConfig;

    protected override void Awake()
    {
        base.Awake();
    }
    public void SetupData(EquipmentData equipmentData, EquipmentConfig equipmentConfig)
    {
        this.equipmentData = equipmentData;
        this.equipmentConfig = equipmentConfig;
        //EquipmentView.Show(equipmentData, equipmentConfig);
        EquipmentLogic.ShowEquipmentView(equipmentData,EquipmentView);
        nameEquipment.text = equipmentConfig.gearName;
        nameEquipment.color = EquipmentLogic.GetColorByRarity(equipmentData.rarity);
        rarityEquipment.text = equipmentData.rarity.ToString();
        rarityEquipment.color = EquipmentLogic.GetColorByRarity(equipmentData.rarity);
        mainStatType.text = EquipmentLogic.StatTypeToString(equipmentData.mainStatData.statType);
        mainStatValue.text = EquipmentLogic.StatValueToString(equipmentData.mainStatData.statType,equipmentData.mainStatData.baseValue);
        textLevel.text = $"Lv.{equipmentData.level}";
    }

    public void ReShow(EquipmentData equipmentData)
    {
        if (this.equipmentData.id == equipmentData.id)
        {
            this.equipmentData = equipmentData;
            textLevel.text = $"Lv.{equipmentData.level}";
        }
    }
    public void EquipGear()
    {
        equipGearSignal.Dispatch(equipmentData);
        NotificationPanelHeroSignal.Dispatch();
        HidePopup();

    }
    public void UnEquipGear()
    {
        unEquipGearSignal.Dispatch(equipmentData);
        NotificationPanelHeroSignal.Dispatch();
        HidePopup();
    }
    public void SelectGearCraft()
    {
        EquipmentLogic.AddEquipmentToCraft(equipmentData);
        NotificationPanelCraftSignal.Dispatch();
        HidePopup();
    }
    public void UnSelectGearCraft()
    {
        EquipmentLogic.RemoveEquipmentToCraft(equipmentData);
        NotificationPanelCraftSignal.Dispatch();
        HidePopup();
    }

    public void LevelUpGear()
    {
        LevelUpGearSignal.Dispatch(equipmentData);
    }
    

}

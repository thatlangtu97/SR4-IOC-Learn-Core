﻿using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

public class CraftEquipmentView : View
{
    [Inject] public GlobalData global { get; set; }
    [Inject] public CraftEquipmentSignal CraftEquipmentSignal { get; set; }
    [Inject] public ShowEquipmentDetailSignal showEquipmentDetailSignal { get; set; }
    [SerializeField]
    List<EquipmentToCraftView> listEquipmentOfHeroView = new List<EquipmentToCraftView>();
    List<EquipmentData> currentEquipment = new List<EquipmentData>();
    public PopupKey popupKeyDetail;
    protected override void Awake()
    {
        base.Awake();
        base.CopyStart();
        InitItem();
    }
    protected override void Start()
    {
        base.Start();
        Show();

    }
    protected override void OnEnable()
    {
        base.OnEnable();
        Show();

    }
    private void InitItem()
    {
        foreach(EquipmentToCraftView temp in listEquipmentOfHeroView)
        {
            if (temp.container.childCount == 0)
            {
                GameObject prefab = PrefabUtils.LoadPrefab(GameResourcePath.ITEM_EQUIPMENT_VIEW);
                EquipmentItemView itemview = Instantiate(prefab, temp.container).GetComponent<EquipmentItemView>();
                itemview.transform.localPosition=Vector3.zero;
                itemview.SetupAction( ()=>ShowDetail(itemview) );
                temp.view = itemview;
            }
        }

    }
    public void ShowDetail(EquipmentItemView tempEquipment)
    {
        ParameterEquipmentDetail temp = new ParameterEquipmentDetail();
        temp.equipmentData = tempEquipment.data;
        temp.equipmentConfig = tempEquipment.config;
        temp.popupkey = popupKeyDetail;
        showEquipmentDetailSignal.Dispatch(temp);
    }
    public void Show()
    {
        currentEquipment = EquipmentLogic.GetEquipmentOfCraft();
        foreach (EquipmentToCraftView equipmentOfCraft in listEquipmentOfHeroView)
        {
            equipmentOfCraft.view.gameObject.SetActive(false);
            equipmentOfCraft.backItem.SetActive(true);
        }
        int index = 0;
        foreach (EquipmentData data in currentEquipment)
        {
            if (index > listEquipmentOfHeroView.Count) continue;
            EquipmentConfig config = EquipmentLogic.GetEquipmentConfigById(data.idConfig);
            listEquipmentOfHeroView[index].backItem.SetActive(false);
//            listEquipmentOfHeroView[index].view.gameObject.SetActive(true);
//            listEquipmentOfHeroView[index].view.Show(data, config);
            EquipmentLogic.ShowEquipmentView(data,listEquipmentOfHeroView[index].view);
            listEquipmentOfHeroView[index].view.transform.localPosition=Vector3.zero;
            index += 1;
        }
    }
    public void CraftItem()
    {
        if (EquipmentLogic.CanCraft())
        {
            CraftEquipmentSignal.Dispatch();
        }

    }
    [System.Serializable]
    public class EquipmentToCraftView
    {
        public EquipmentItemView view;
        public GameObject backItem;
        public Transform container;
    }
}


using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopAds : View
{
    [Inject] public GlobalData globalData { get; set; }
    public CurrencyType currencyType;
    public int value;
    public Text valueText;
    public Button btnBuy;
    protected override void Awake()
    {
        base.Awake();
        base.CopyStart();
        valueText.text = $"{value}";
        if(btnBuy)
            btnBuy.onClick.AddListener(BuyItem);
    }
    public void BuyItem()
    {
        switch (currencyType)
        {
            case CurrencyType.gold:
                DataManager.Instance.CurrencyDataManager.UpGold(value, false);
                break;
            case CurrencyType.gem:
                DataManager.Instance.CurrencyDataManager.UpGem(value, false);
                break;
            case CurrencyType.stamina:
                DataManager.Instance.CurrencyDataManager.UpStamina(value, false);
                break;
        }
        globalData.UpdateDataAllCurrencyView();
    }
}

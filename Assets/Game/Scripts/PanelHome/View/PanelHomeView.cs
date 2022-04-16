using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelHomeView : AbsPanelView
{
    [Inject] public ShowPopupStaminaSignal showPopupStaminaSignal { get; set; }
    [Inject] public ShowPanelHeroSignal showPanelHeroSignal { get; set; }
    [Inject] public ShowPanelCraftSignal showPanelCraftSignal { get; set; }
    [Inject] public ShowPanelShopSignal showPanelShopSignal { get; set; }
    public Button StaminaBtn;
    public Button ShopBtn;
    public Button HeroBtn;
    public Button CraftBtn;

    public Button StartGameBtn;
    //public Button ShopGoldBtn, ShopGemBtn;
    public Doozy.Engine.UI.UIButton UIBtnShop, UIBtnHero , UIBtnCraft;
    protected override void Start()
    {
        base.Start();
        HeroBtn.onClick.AddListener(ShowPanelHero);
        CraftBtn.onClick.AddListener(ShowPanelCraft);

        ShopBtn.onClick.AddListener(ShowPanelShopGold);
        StartGameBtn.onClick.AddListener(StartGame);
        /*
        StaminaBtn.onClick.AddListener( ()=>{ showPopupStaminaSignal.Dispatch(); });
        ShopGoldBtn.onClick.AddListener(() => { showPanelShopSignal.Dispatch(); });
        ShopGoldBtn.onClick.AddListener(() => { popupManager.popupKey = PopupKey.ShopGoldPopup; });
        ShopGemBtn.onClick.AddListener(() => { showPanelShopSignal.Dispatch(); });
        ShopGemBtn.onClick.AddListener(() => { popupManager.popupKey = PopupKey.ShopGemPopup; });
        */

//        UIBtnHero.OnClick.OnTrigger.Event.AddListener(ShowPanelHero);
//        UIBtnShop.OnClick.OnTrigger.Event.AddListener(ShowPanelShopGold);
//        UIBtnCraft.OnClick.OnTrigger.Event.AddListener(ShowPanelCraft);

    }

    public override void ShowPanelByCmd()
    {
        base.ShowPanelByCmd();
        popupManager.SetFirstSelect(ShopBtn.gameObject);
    }

    public void ShowPanelHero()
    {
        showPanelHeroSignal.Dispatch();
    }
    public void ShowPanelCraft()
    {
        showPanelCraftSignal.Dispatch();
    }
    public void ShowPanelShopGold()
    {
        popupManager.popupKey = PopupKey.ShopGoldPopup;
        showPanelShopSignal.Dispatch();
    }
    public void LoadScene(string name)
    {
        if (PlayFlashScene.instance)
            PlayFlashScene.instance.Loading(name, 1.2f);
        else
            SceneManager.LoadScene(name);
    }

    public void StartGame()
    {

        //SceneManager.LoadScene("TestBt");
        LoadScene("TestBt");
    }
}

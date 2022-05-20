using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using strange.extensions.signal.impl;
using UnityEngine.EventSystems;

public class PopupManager 
{
    public string panelKey { get; set; }
    public string popupKey { get; set; }

    public string sceneKey { get; set; }
    
    public string BasePabelKey;
    
    public Dictionary<string, List<string>> backPanelData = new Dictionary<string, List<string>>();
    public Dictionary<UILayer, Transform> UIDic = new Dictionary<UILayer, Transform>();
    public Dictionary<string, AbsPopupView> PanelDic = new Dictionary<string, AbsPopupView>();
    public Dictionary<string, AbsPopupView> PopupDic = new Dictionary<string, AbsPopupView>();

    public Dictionary<string, IEnumerator> listActionDelay = new Dictionary<string, IEnumerator>();
    //public Dictionary<PanelKey, List<GameObject>> ListPopupOfPanel = new Dictionary<PanelKey, List<GameObject>>();
    public EventSystem eventSystem { get; set; }
    public List<AbsPopupView> ListPopupShow = new List<AbsPopupView>();
    [Inject] public ShowPanelHeroSignal showPanelHeroSignal { get; set; }
    [Inject] public ShowPanelHomeSignal showPanelHomeSignal { get; set; }
    [Inject] public ShowPanelShopSignal showPanelShopSignal { get; set; }
    
    
    // 
    public List<Type> fullScreenPopup= new List<Type>();
    private List<AbsPopupView> listPopupShow = new List<AbsPopupView>();
    private Type curPopup = null;
    
    
    public PopupManager()
    {
    }

    void SetupFullScreen()
    {
        fullScreenPopup.Add(typeof(PanelHomeView));
        fullScreenPopup.Add(typeof(PanelHeroView));
        fullScreenPopup.Add(typeof(PanelShopView));
        fullScreenPopup.Add(typeof(PanelCraftView));
    }
    
    
    
    
    
    public void SetFirstSelect(GameObject gameObjectFirst)
    {
        if (eventSystem)
        {
            eventSystem.firstSelectedGameObject = gameObjectFirst;
        }
    }
    public void AddUILayer(UILayer layer, Transform transform)
    {

        if (UIDic.ContainsKey(layer))
        {
            UIDic[layer] = transform;
        }
        else
        {
            UIDic.Add(layer, transform);

        }
    }
    
    public Transform GetUILayer(UILayer layer)
    {
        if (UIDic.ContainsKey(layer))
        {
            return UIDic[layer];
        }
        else
        {
            return null;
        }

    }

    #region PANEL
    public bool CheckContainPanel(string key)
    {
        if (PanelDic.ContainsKey(key))
        {
            return true;
        }
        return false;
    }
    public AbsPopupView GetPanelByPanelKey(string key)
    {
        if (PanelDic.ContainsKey(key))
        {
            return PanelDic[key];
        }
        
        return null;
    }
    public void AddPanel(AbsPopupView panel)
    {
        string key = panel.GetType().ToString();
        if (PanelDic.ContainsKey(key))
        {
            PanelDic[key] = panel;
        }
        else
        {
            PanelDic.Add(key, panel);
        }

        if (!backPanelData.ContainsKey(sceneKey))
        {
            backPanelData.Add(sceneKey, new List<string>(){key});
        }
        if (!backPanelData[sceneKey].Contains(key))
        {
            backPanelData[sceneKey].Add(key);
        }
    }

    
    public void ResetPanelAfterLoadHomeScene()
    {
        panelKey = typeof(PanelHomeView).ToString();
        BasePabelKey = typeof(PanelHomeView).ToString();
    }
    public string GetPanelAfterLoadHomeScene()
    {
        return panelKey;
    }
    public void ResetPanelShowAfterLoadHomeScene()
    {
        //panelKey = PanelKey.PanelHome;
    }

    public void ShowPanel(AbsPanelView panel)
    {
        //panelKey = key;

        string key = panel.GetType().ToString();
        foreach (string temp in PanelDic.Keys)
        {
            if (temp != key)
            {
                if(PanelDic[temp]!=null)
                    PanelDic[temp].HidePopup();
            }
//            else
//            {
//                if (PanelDic[temp] != null)
//                    PanelDic[temp].ShowPanel();
//            }
        }
//
//        PanelDic[key].ShowPopup();
    }
    public void BackPanel()
    {
        //Disable popup
        AbsPopupView lastPopup = null;
        foreach (AbsPopupView temp in PopupDic.Values)
        {
            if (temp != null)
            {
                if (temp.gameObject.activeInHierarchy == true)
                {
                    lastPopup = temp;
                }
            }
        }
        if (lastPopup != null)
        {
            lastPopup.HidePopup();
            return;
        }
        //disable Panel
        foreach (string temp in PanelDic.Keys)
        {
            if (temp != BasePabelKey)
            {
                if (PanelDic[temp] != null)
                {
                    if (PanelDic[temp].gameObject.activeInHierarchy == true)
                    {
                        PanelDic[temp].GetComponent<AbsPanelView>().HidePanel();
                    }
                }
            }
        }

        if (sceneKey == "Home")
        {
            if (BasePabelKey == typeof(PanelHomeView).ToString())
            {
                showPanelHomeSignal.Dispatch(new ParameterPanelHome());
                panelKey = typeof(PanelHomeView).ToString();
            }
        }
    }
    #endregion

    #region POPUP
    public bool CheckContainPopup(AbsPopupView popup)
    {
        string key = popup.GetType().ToString();
        if (PopupDic.ContainsKey(key))
        {
            return true;
        }

        if (PanelDic.ContainsKey(key))
        {
            return true;
        }
        return false;
    }
    public AbsPopupView GetPopupByPopupKey(AbsPopupView popup)
    {
        string key = popup.GetType().ToString();
        if (PanelDic.ContainsKey(key))
        {
            return PanelDic[key];
        }
        if (PopupDic.ContainsKey(key))
        {
            return PopupDic[key];
        }

        return null;
    }
    public void AddPopup(AbsPopupView panel)
    {
        string key = panel.GetType().ToString();
        if (PopupDic.ContainsKey(key))
        {
            PopupDic[key] = panel;
        }
        else
        {
            PopupDic.Add(key, panel);
        }
    }
    public void ResetPopup()
    {
        //popupKey = PopupKey.Node;
    }
//    public void ShowPopup(AbsPopupView popup)
//    {
//        string key = popup.GetType().ToString();
//        if (!PopupDic.ContainsKey(key))
//            return;
//        if (PopupDic[key] != null)
//            PopupDic[key]/*.GetComponent<AbsPopupView>()*/.ShowPopup();
//    }
    public void ShowPopup(AbsPopupView absPopup,bool addToListShow=true)
    {
        if (fullScreenPopup.Contains(absPopup.GetType()))
        {
//            string key = absPopup.GetType().ToString();
//            if (!listTypeHasSetup.Contains(absPopup.GetType()))
//            {
//                SetupBackForPopup(absPopup);
//            }
//            if (backData.ContainsKey(key))
//            {
//                currentBackData.Add(absPopup.GetType(), key);
//            }

            curPopup = absPopup.GetType();
            
            string key = absPopup.GetType().ToString();
            foreach (string temp in PanelDic.Keys)
            {
                if (temp != key)
                {
                    if(PanelDic[temp]!=null)
                        PanelDic[temp].HidePopup();
                }
            }
    
            for (int i = PopupDic.Count - 1; i >= 0; i--)
            {
                if (listPopupShow[i] != null)
                    listPopupShow[i].HidePopup();
            }

            listPopupShow.Clear();
        }
        if (addToListShow)
            listPopupShow.Add(absPopup);
    }
    #endregion
    
    
    
    public T GetPopupShow<T>() where T : AbsPopupView {
        for (int i = 0; i < ListPopupShow.Count; i++)
        {
            if (ListPopupShow[i].GetType() == typeof(T))
            {
                return (T)ListPopupShow[i];
            }
        }

        return null;
    }
    public bool GetPopupShow<T>(out AbsPopupView popup) where T : AbsPopupView
    {
        if (IsShowPopup<T>())
        {
            popup = GetPopupShow<T>();
            return true;
        }

        popup = null;
        return false;
    }
    public bool IsShowPopup<T>() where T : AbsPopupView
    {
        for (int i = 0; i < ListPopupShow.Count; i++)
        {
            if (ListPopupShow[i].GetType() == typeof(T))
            {
                return true;
            }
        }

        return false;
    }

}
public enum PanelKey
{
    PanelHome,
    PanelHero,
    PanelCraft,
    PanelShop,
}
public enum PopupKey
{
    Node=0,
    StaminaPopup = 1,
    EquipmentHeroDetailLeft = 2,
    EquipmentHeroDetailRight = 3,
    EquipmentCraftDetailLeft = 4,
    EquipmentCraftDetailRight = 5,
    ShopGoldPopup = 6,
    ShopGemPopup =7,
    ShopGachaPopup =8,
    GachaPopup =9,
    CraftPopup =10,
    GachaInfoPopup =11,
    RevivePopup = 12,
    RewardGameplayPopup = 13,
    RewardPopup=14,
    TooltipPopup=15,
    
    
    
}

public enum SceneKey
{
    Home,
    GamePlay,
}
public enum UILayer
{
    UI1,
    UI2,
    NODE,
    UI3,
    UI4,
}

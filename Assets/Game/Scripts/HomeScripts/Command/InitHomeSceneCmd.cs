using strange.extensions.command.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitHomeSceneCmd : Command
{
	[Inject] public PopupManager popupManager { get; set; }

    public override void Execute()
    {
	    popupManager.sceneKey = SceneKey.Home.ToString();
        //	popupManager.listActionDelay.Add(GameResourcePath.PANEL_HERO, delayPlayAction(
        //		() =>
        //		{
        //			GetResourcePath = GameResourcePath.PANEL_HERO;
        //			PanelHeroView panelHeroView = GetInstance<PanelHeroView>();
        //		}
        //		));
        //	popupManager.listActionDelay.Add(GameResourcePath.PANEL_CRAFT, delayPlayAction(
        //		() =>
        //		{
        //			GetResourcePath = GameResourcePath.PANEL_CRAFT;
        //			PanelCraftView panelCraftView = GetInstance<PanelCraftView>();
        //		}
        //		));
        //	popupManager.listActionDelay.Add(GameResourcePath.PANEL_SHOP, delayPlayAction(
        //		() =>
        //{
        //	GetResourcePath = GameResourcePath.PANEL_SHOP;
        //	PanelShopView panelShopView = GetInstance<PanelShopView>();
        //}
        //		));
        //	popupManager.listActionDelay.Add(GameResourcePath.PANEL_HOME, delayPlayAction(
        //		() =>
        //{
        //	GetResourcePath = GameResourcePath.PANEL_HOME;
        //	PanelHomeView panelHomeView = GetInstance<PanelHomeView>();
        //}
        //		));

//        GetResourcePath = GameResourcePath.PANEL_HERO;
//        PanelHeroView panelHeroView = GetInstance<PanelHeroView>();
//        GetResourcePath = GameResourcePath.PANEL_CRAFT;
//        PanelCraftView panelCraftView = GetInstance<PanelCraftView>();
//        GetResourcePath = GameResourcePath.PANEL_SHOP;
//        PanelShopView panelShopView = GetInstance<PanelShopView>();
        GetResourcePath = GameResourcePath.PANEL_HOME;
        PanelHomeView panelHomeView = GetInstance<PanelHomeView>();
    }
	public T GetInstance<T>() where T : Component
	{
		bool isInit = injectionBinder.GetBinding<T>(GetInjectName()) == null ||
					  injectionBinder.GetInstance<T>(GetInjectName()) == null;
		if (isInit)
		{
			GameObject q = Instantiate();
			//q.transform.localScale = new Vector3(1, 1, 1);
			if (injectionBinder.GetBinding<T>(GetInjectName()) != null)
			{
				injectionBinder.Unbind<T>(GetInjectName());
			}
			injectionBinder.Bind<T>()
				.ToValue(q.GetComponent<T>())
				.ToName(GetInjectName());
		}

		return injectionBinder.GetInstance<T>(GetInjectName());
	}

	public virtual string GetInjectName()
	{
		return "";
	}
	public GameObject Instantiate()
	{
		GameObject o = PrefabUtils.LoadPrefab(GetResourcePath);
		GameObject spawned = null;
		PanelKey panelKey = o.GetComponent<AbsPanelView>().panelKey;
		UILayer uILayer = o.GetComponent<AbsPanelView>().uILayer;
		AbsPanelView typePanel = o.GetComponent<AbsPanelView>();
		if (!popupManager.CheckContainPanel(typePanel.GetType().ToString()))
		{
			spawned = GameObject.Instantiate(o/*, popupManager.GetUILayer(uiLayer)*/) as GameObject;
			
			popupManager.AddPanel(spawned.GetComponent<AbsPanelView>());

		}
		else
		{
			if (popupManager.GetPanelByPanelKey(typePanel.GetType().ToString()) == null)
			{
				spawned = GameObject.Instantiate(o) as GameObject;
				popupManager.AddPanel(spawned.GetComponent<AbsPanelView>());
			}
			else
			{
				spawned = popupManager.GetPanelByPanelKey(typePanel.GetType().ToString()).gameObject;
			}
		}
		spawned.transform.parent = popupManager.GetUILayer(uILayer);
		spawned.transform.localScale = Vector3.one;
		return spawned;
	}

	public string GetResourcePath { get; set; }

	IEnumerator delayPlayAction(Action action)
    {
		yield return new WaitForSecondsRealtime(0.3f);
		action.Invoke();


	}
}

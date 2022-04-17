using System;
using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using EntrySystem;
using UnityEngine;

public class HomeSceneContextView : ContextView
{
    private void Awake()
    {
        if (EntryContextView.Instance != null)
        {
            //EntryContextView.Instance.loadFlashScene = false;
        }
        
        InitUI();
    }
    public void InitUI()
    {
        GameObject UI1 = Resources.Load<GameObject>(GameResourcePath.UI1);
        GameObject UI2 = Resources.Load<GameObject>(GameResourcePath.UI2);
        Instantiate(UI1);
        Instantiate(UI2);
    }

    void Start()
    {
        context = new HomeSceneContext(this);
        context.Start();
        if (PlayFlashScene.instance != null)
        {
            PlayFlashScene.instance.HideLoading();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbsTabView<T> : View where T : struct, IComparable, IFormattable, IConvertible
{
    public Image backGroundTab;
    public Text[] textTab;
    public T tabType;
    public Action<T> onSelect = delegate (T action) { };
    public bool isSelect = false;
    public bool isLock = false;
    public void Init(T tabType, Action<T> onSelect) {
        this.tabType = tabType;
        this.onSelect += onSelect;
        OnInit();
        GetComponent<Button>().onClick.AddListener(delegate
        {
            Debug.Log("Select Tab"+tabType);
            Select();
        } );
        textTab = GetComponentsInChildren<Text>();
    }
    public void OnChangeTab(T tabType)
    {
        Debug.Log("OnChangeTab"+tabType);
        isSelect = this.tabType.GetHashCode() == tabType.GetHashCode();
        if (isSelect)
        {
            onSelect.Invoke(tabType);
            
        }
        onChange(isSelect);
    }
    protected virtual void Select() {
        OnChangeTab(tabType);
    }
    public void Lock(bool isLock)
    {
        this.isLock = isLock;
    }
    protected abstract void OnMapValue();
    protected virtual void OnInit(){}
    public virtual void OnShow(){}
    
    public virtual void onChange(bool isSelect) {}
    
    public virtual void ShowHighlight(T tabtype) { }
}

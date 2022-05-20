using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbsTabView<T> : View where T : struct, IComparable, IFormattable, IConvertible
{
    public T tabType;
    public Action<T> onSelect = delegate (T inventoryType) { };
    public void Init(T tabType, Action<T> onSelect) {
        this.tabType = tabType;
        this.onSelect += onSelect;
        OnInit();
        GetComponent<Button>().onClick.AddListener(delegate
        {
            this.onSelect.Invoke(tabType);
            Debug.Log("Select Tab");
        } );
    }
    public void OnChangeTab(T tabType) {
//        toggle.value = (this.tabType.GetHashCode() == tabType.GetHashCode());
//        if (toggle.value) {
//            onSelect.Invoke(tabType);
//        }

        if (this.tabType.GetHashCode() == tabType.GetHashCode())
        {
            onSelect.Invoke(tabType);
        }
        
    }
    protected abstract void OnMapValue();
    protected virtual void OnInit(){}
    public virtual void OnShow(){}
}

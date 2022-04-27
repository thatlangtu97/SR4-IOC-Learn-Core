using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupRewardView : AbsPopupView
{
    List<AbsRewardLogic> listReward = new List<AbsRewardLogic>();
    public List<ItemView> listItemView= new List<ItemView>();
    protected override void Awake()
    {
        base.Awake();
        InitItemView();
    }

    public override void ShowPopupByCmd()
    {
        base.ShowPopupByCmd();
        listReward = (parameterPopup as ShowPopupRewardParameter).listRewardLogics;
        Show();
    }

    public void InitItemView()
    {
        
    }

    public void Show()
    {
        for (int i = 0; i < listItemView.Count; i++)
        {
            if(i<listReward.Count)
                listItemView[i].Show(listReward[i]);
            else
            {
                listItemView[i].Hide();
            }
        }
    }
}
